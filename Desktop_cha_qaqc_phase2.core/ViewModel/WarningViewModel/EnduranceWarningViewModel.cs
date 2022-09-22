using Desktop_cha_qaqc_phase2.Core.Dialog;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
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
using System.Timers;
using System.Windows;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.WarningViewModel
{
    public class EnduranceWarningViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly IS71200EnduranceMachineService _is71200ModellingMachineService;
        private readonly IApiService _apiService;
        private readonly IDialogService _dialogService;
        private int preErrorCode;
        private ObservableCollection<ListEvent> listEvents = new ObservableCollection<ListEvent>();

        public ObservableCollection<ListEvent> ListEvents
        {
            get { return listEvents; }
            set { listEvents = value; }
        }
        public EnduranceWarningViewModel(S71200EnduranceMachineService is71200ModellingMachine, IApiService apiService,IDialogService dialogService)
        {
            _is71200ModellingMachineService = is71200ModellingMachine;
            _dialogService = dialogService;
            _apiService = apiService;
            _is71200ModellingMachineService.DataUpdated += Update;

        }
        private async void Update(EnduranceMachineMonitoringData monitorData)
        {
            if (monitorData.ErrorStatus)
            {
                if (preErrorCode != monitorData.ErrorCode)
                {

                    preErrorCode = monitorData.ErrorCode;
                    var error = new ErrorCode();
                    string message;
                    error.EnduranceWarningCode.TryGetValue(monitorData.ErrorCode, out message);
                    if (message != "")
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            ListEvents.Add(new ListEvent(DateTime.Now, message));

                        });
                        if (monitorData.ErrorCode == 100)
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
                            apiwarning.ErrorCode = monitorData.ErrorCode;
                            apiwarning.Message = message;

                        }
                        else
                        {
                            apiwarning.Timestamp = DateTime.Now;
                            apiwarning.ErrorCode = monitorData.ErrorCode;
                            apiwarning.Message = "Undifined";
                        }
                        var result = await _apiService.PostEnduranceWarning(apiwarning);
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
            else
            {
                preErrorCode = 0;
            }
        }

    }
}
    