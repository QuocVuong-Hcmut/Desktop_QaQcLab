using Desktop_cha_qaqc_phase2.Core.Dialog;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.WarningViewModel
{
    public class ForcedCloseWarningViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly ILogoForceCloseMachineService _supervisorService;
        private readonly IDialogService _dialogService;
        private readonly IApiService _apiService;
        private int preErrorCode;
        private ObservableCollection<ListEvent> listEvents = new ObservableCollection<ListEvent>();

        public ObservableCollection<ListEvent> ListEvents
        {
            get { return listEvents; }
            set { listEvents = value; }
        }

        public ForcedCloseWarningViewModel(ILogoForceCloseMachineService supervisor, IApiService apiService, IDialogService dialogService)
        {
            _supervisorService = supervisor;
            _dialogService = dialogService;
            _apiService = apiService;
            _supervisorService.DataUpdated += Update;
        }
        private async void Update(ForcedCloseMachineMonitoringData monitoringData)
        {

            if (monitoringData.Warn == true)
            {
                if (preErrorCode != monitoringData.ForceCloseWarningCode)
                {
                    preErrorCode = monitoringData.ForceCloseWarningCode;
                    var ErrorCode = new ErrorCode();
                    string message;
                    ErrorCode.ForcedCloseWarningCode.TryGetValue(monitoringData.ForceCloseWarningCode, out message);
                    if (message != "")
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                        ListEvents.Add(new ListEvent(DateTime.Now, message));
                        });
                        if (monitoringData.ForceCloseWarningCode == 203)
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
                            apiwarning.ErrorCode = monitoringData.ForceCloseWarningCode;
                            apiwarning.Message = message;

                        }
                        else
                        {
                            apiwarning.Timestamp = DateTime.Now;
                            apiwarning.ErrorCode = monitoringData.ForceCloseWarningCode;
                            apiwarning.Message = "Undifined";
                        }
                        var result = await _apiService.PostForcedCloseWarning(apiwarning);
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
            else preErrorCode = 0;


        }

    }


}

