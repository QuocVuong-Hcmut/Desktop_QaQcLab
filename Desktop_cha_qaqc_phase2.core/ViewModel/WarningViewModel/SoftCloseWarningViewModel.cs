using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using Desktop_cha_qaqc_phase2.Core.Domain.Models;
using System;
using Desktop_cha_qaqc_phase2.Core.Dialog;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.WarningViewModel
{
    public class SoftCloseWarningViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly ILogoSoftCloseMachineService _supervisorService;
        private readonly IApiService _apiService;
        private IDialogService _dialogService;
        private int preWarningCode;
        private ObservableCollection<ListEvent> listEvents = new ObservableCollection<ListEvent>();
        public ObservableCollection<ListEvent> ListEvents
        {
            get { return listEvents; }
            set { listEvents = value; }
        }
        public SoftCloseWarningViewModel(ILogoSoftCloseMachineService supervisor,
            IApiService apiService,
            IDialogService dialogService)
        {
            _supervisorService = supervisor;
            _apiService = apiService;
            _dialogService = dialogService;
            _supervisorService.DataUpdated += Update;
        }
        private async void Update(SoftCloseMachineMonitoringData monitoringData)
        {
            if (monitoringData.Warn == true)
            {
                if (preWarningCode != monitoringData.SoftCloseWarningCode)
                {
                    preWarningCode = monitoringData.SoftCloseWarningCode;
                    var ErrorCode = new ErrorCode();
                    string message;
                    ErrorCode.SoftCloseWarningCode.TryGetValue(monitoringData.SoftCloseWarningCode, out message);
                    if (message != "")
                    {

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            ListEvents.Add(new ListEvent(DateTime.Now, message));
                        });

                        if (monitoringData.SoftCloseWarningCode == 103)
                        {
                            _dialogService.ShowDialog(message, 1);
                        }
                        else
                        {
                            _dialogService.ShowDialog(message, 2);

                        }
                        #region API
                        //API
                        AlarmCode apiwarning = new AlarmCode();
                        if (message != null)
                        {
                            apiwarning.Timestamp = DateTime.Now;
                            apiwarning.ErrorCode = monitoringData.SoftCloseWarningCode;
                            apiwarning.Message = message;
                        }
                        else
                        {
                            apiwarning.Timestamp = DateTime.Now;
                            apiwarning.ErrorCode = monitoringData.SoftCloseWarningCode;
                            apiwarning.Message = "Undifined";
                        }
                        var result = await _apiService.PostSoftCloseWarning(apiwarning);

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
            //reset pre warning code
            else preWarningCode = 0;
        }
    }
}
