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
    public class ForcedCloseSettingsViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {

        private short preTimeStop; 
        private short preTimeStart;
        private int preTimeCount;

        public bool EnableSetting { get; set; } = false; 


        private readonly ILogoForceCloseMachineService _modelingMachineService;
        private ConfirmSettingViewModel _confirmSettingViewModel;
        public ICommand ConfirmSettingCommand { get; set; }
        public ConfirmSettingViewModel ConfirmSettingViewModel
        {
            get => _confirmSettingViewModel;
        }

        public short TimeStop { get; set; }
        public short TimeStart { get; set; }
        public int TimeCount { get; set; }
        private  IDatabaseService _database;

        public ForcedCloseSettingsViewModel(ILogoForceCloseMachineService logoModelingMachine1, ConfirmSettingViewModel confimSettingViewModel,IDatabaseService databaseService)
        {
            // _modelingMachine.ConnectAlarm.Add(Alert);
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
            
        }

        private void CancelConfirm(object? sender, EventArgs e)
        {
            _confirmSettingViewModel.ConfirmAction -= ConfirmSetting;
        }

        /// <summary>
        /// Send configuration to logo2 
        /// </summary>
        private void ConfirmSetting(object sender, EventArgs e)
        {
            _modelingMachineService.Send2Bytes(TimeStop, 0);
            _modelingMachineService.Send2Bytes(TimeStart, 4);
            _modelingMachineService.Send4byte(TimeCount, 14);
            preTimeCount = TimeCount;
            preTimeStart= TimeStart;
            preTimeStop= TimeStop;
            foreach ( var item in _database.LoadPreReportForcedClose( ).Result )
            {
                if ( item.IsReport )
                {
                    _database.DeletePreReportForcedClose(item);
                }
            }

            _confirmSettingViewModel.ConfirmAction -= ConfirmSetting;
        }
        private void Update(ForcedCloseMachineMonitoringData monitoringData)
        {
            if ((preTimeStop != monitoringData.TimeCloseSP) || 
                (preTimeStart != monitoringData.TimeOpenSP) || 
                (preTimeCount != monitoringData.NumberOfClosingSP) || 
                (monitoringData.Run == true))
            {
                TimeStop = (short)monitoringData.TimeCloseSP;
                TimeStart = (short)monitoringData.TimeOpenSP; 
                TimeCount = monitoringData.NumberOfClosingSP;
                preTimeStop = (short)monitoringData.TimeCloseSP;
                preTimeStart = (short)monitoringData.TimeOpenSP;
                preTimeCount = monitoringData.NumberOfClosingSP;
                EnableSetting = false;
            }
            else
            {
                EnableSetting = true;
            }
        }
    }

}
