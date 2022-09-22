using Desktop_cha_qaqc_phase2.Core.Dialog;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.WarningViewModel
{
    public class WaterProofingWarningViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly IS71200WaterProofingMachineService _supervisorService;
        private readonly IApiService _apiService;
        private IDialogService _dialogService;
        private int preWarningCode;
        private ObservableCollection<ListEvent> listEvents = new ObservableCollection<ListEvent>();
        public ObservableCollection<ListEvent> ListEvents
        {
            get { return listEvents; }
            set { listEvents = value; }
        }
        public WaterProofingWarningViewModel(
            S71200WaterProofingMachineService supervisor,
            IApiService apiService,
            IDialogService dialogService)
        {
            _supervisorService = supervisor;
            _apiService = apiService;
            _dialogService = dialogService;
            _supervisorService.DataUpdated += Update;
        }
        private async void Update(WaterProofingMachineMonitoringData monitoringData)
        {

            if ((monitoringData.ErrorSystem == true))
            {
                if (preWarningCode != monitoringData.ErrorCode)
                {
                    preWarningCode = monitoringData.ErrorCode;
                    var ErrorCode = new ErrorCode();
                    string message;
                    ErrorCode.WaterProofingWarningCode.TryGetValue(monitoringData.ErrorCode, out message);
                    // Chuyển Dispatcher thread 
                    if (message != "")
                    {

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            ListEvents.Add(new ListEvent(DateTime.Now, message));
                        });
                        if (monitoringData.ErrorCode == 100)
                        {
                            _dialogService.ShowDialog(message, 1);

                        }
                        else
                        {
                            _dialogService.ShowDialog(message, 2);

                        }
                        #region API

                        AlarmCode apiwarning = new AlarmCode();
                        if (message != null)
                        {
                            apiwarning.Timestamp = DateTime.Now;
                            apiwarning.ErrorCode = monitoringData.ErrorCode;
                            apiwarning.Message = message;

                        }
                        else
                        {
                            apiwarning.Timestamp = DateTime.Now;
                            apiwarning.ErrorCode = monitoringData.ErrorCode;
                            apiwarning.Message = "Undifined";
                        }
                        var result = await _apiService.PostWaterProofingWarning(apiwarning);
                        if (!result.Success)
                        {
                            if (!string.IsNullOrEmpty(result.Error.Message))
                            {
                                _dialogService.ShowDialog(result.Error.Message, 2);

                            }
                        }
                        #endregion
                    }

                }
            }
            else preWarningCode = 0;
        }
    }
}
