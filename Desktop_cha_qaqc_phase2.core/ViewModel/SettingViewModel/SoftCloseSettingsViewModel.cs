
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.SettingViewModel
{
    public class SoftCloseSettingsViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly ILogoSoftCloseMachineService _modelingMachineService;
        private ConfirmSettingViewModel _confirmSettingViewModel;
        #region Command
        public ICommand ConfirmSettingCommand { get; set; }
        #endregion
        #region Public Property

        public ConfirmSettingViewModel ConfirmSettingViewModel
        {
            get => _confirmSettingViewModel;
        }

        public bool EnableSetting { get; set; } = false;

        private short preTimeStop;
        public short TimeStop { get; set; }

        private short preTimeStart;
        public short TimeStart { get; set; }

        private int preTimeCount;
        public int TimeCount { get; set; }
        #endregion
        public SoftCloseSettingsViewModel(ILogoSoftCloseMachineService logoModelingMachine1, ConfirmSettingViewModel confimSettingViewModel)
        {
            _confirmSettingViewModel = confimSettingViewModel;
            _confirmSettingViewModel.CancelAction += CancelConfirm;
            ConfirmSettingCommand = new RelayCommand(
                () =>
                {
                    _confirmSettingViewModel.IsOpen = true;
                    _confirmSettingViewModel.ConfirmAction += ConfirmSetting;
                });

            _modelingMachineService = logoModelingMachine1;
            _modelingMachineService.DataUpdated += Update;

            // _modelingMachine.ConnectAlarm.Add(Alert);
        }

        private void CancelConfirm(object? sender, EventArgs e)
        {
            _confirmSettingViewModel.ConfirmAction -= ConfirmSetting;
        }

        /// <summary>
        /// Send Configuration to Logo1 
        /// (a Product of HCMUT PDA)
        /// </summary>
        private void ConfirmSetting(object sender, EventArgs e)
        {
            _modelingMachineService.Send2Bytes(TimeStop, 0);
            _modelingMachineService.Send2Bytes(TimeStart, 4);
            _modelingMachineService.Send4byte(TimeCount, 14);
            preTimeCount = TimeCount;
            preTimeStart = TimeStart;
            preTimeStop = TimeStop;

            _confirmSettingViewModel.ConfirmAction -= ConfirmSetting;
        }
        private void Update(SoftCloseMachineMonitoringData monitoringData)
        {
            if ((preTimeStop != monitoringData.TimeCloseSP) ||
                (preTimeStart != monitoringData.TimeOpenSP) ||
                (preTimeCount != monitoringData.NumberOfClosingSP) ||
                (monitoringData.Run == true))
            {
                TimeStop = (short)monitoringData.TimeCloseSP;
                TimeStart = (short)monitoringData.TimeOpenSP;
                TimeCount = monitoringData.NumberOfClosingSP;
                preTimeStop = TimeStop = (short)monitoringData.TimeCloseSP;
                preTimeStart = TimeStart = (short)monitoringData.TimeOpenSP;
                preTimeCount = TimeCount = monitoringData.NumberOfClosingSP;
                EnableSetting = false;
            }
            else
            {
                EnableSetting = true;
            }
        }
    }
}
