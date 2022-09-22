using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.SettingViewModel
{
    public class WaterProofingSettingsViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        public bool EnableSetting { get; set; }
        private int preTemperature_SP;
        private int preHour_SP;
        private int preMinute_SP;
        public int Temperature_SP { get; set; }
        public int Temperature_Delay_SP { get; set; }
        public int Hour_SP { get; set;}
        public int Minute_SP { get; set; }
        public ConfirmSettingViewModel ConfirmSettingViewModel { get => _confirmSettingViewModel; }
        private S71200WaterProofingMachineService _waterProofing;
        private ConfirmSettingViewModel _confirmSettingViewModel;
        private IDatabaseService _databaseService;
        public ICommand ConfirmSettingCommand { get; set; }
        public WaterProofingSettingsViewModel(S71200WaterProofingMachineService waterProofing,
            ConfirmSettingViewModel confirmSettingViewModel,
            IDatabaseService databaseService
            )
        {
            _databaseService=databaseService;
            _waterProofing = waterProofing;
            _confirmSettingViewModel = confirmSettingViewModel;
            ConfirmSettingCommand = new RelayCommand(ConfirmSettingPara);
            _waterProofing.DataUpdated += Update;
            WaterProofingSupervisorViewModel.UpdateDatabase += UpdateReport;
        }

        private void Update(WaterProofingMachineMonitoringData monitoringData)
        {
            if ((monitoringData.TemperatureSP != preTemperature_SP) ||
               (monitoringData.HourSP != preHour_SP) ||
               (monitoringData.MinuteSP != preMinute_SP))
            {
                EnableSetting = false;
                Temperature_SP = monitoringData.TemperatureSP;
                Hour_SP = monitoringData.HourSP;
                Minute_SP = monitoringData.MinuteSP;
                preTemperature_SP = monitoringData.TemperatureSP;
                preHour_SP = monitoringData.HourSP;
                preMinute_SP = monitoringData.MinuteSP;
                Temperature_Delay_SP=monitoringData.TemperatureDelay;
            }
            else
            {
                EnableSetting = true;
            }
        }
        public static event Action<WaterProofingTestSample> UpdatePreReport;
        private void ConfirmSettingPara()
        {
      //      _databaseService.InsertPreReportWaterProofing(new PreReportWaterProofing( ) { DateTime=DateTime.Now,Temperature=Temperature_SP,Time=Hour_SP+Minute_SP/60 });
     //      var a = _databaseService.LoadPreReportWaterProofing();
            preHour_SP = Hour_SP;
            preMinute_SP = Minute_SP;
            preTemperature_SP = Temperature_SP;
            _waterProofing.SendTime(Hour_SP, 2);
            _waterProofing.SendTime(Minute_SP, 6);
            _waterProofing.SendDataReal(Temperature_SP, 10);
            _waterProofing.SendData2Byte(Temperature_Delay_SP,14);
            foreach(var item in _databaseService.LoadPreReportWaterProofing().Result )
            {
                if ( item.IsReport )
                {
                    _databaseService.DeletePreReportWaterProofing(item);
                }
            }
            //   UpdatePreReport?.Invoke(new WaterProofingTestSample( ) { Duration=Hour_SP+Minute_SP/60,Temperature=Temperature_SP });
            _confirmSettingViewModel.IsOpen = true;
            _confirmSettingViewModel.ConfirmAction += ConfirmSetting;
        }
        public void UpdateReport()
        {
            _databaseService.InsertPreReportWaterProofing(new PreReportWaterProofing() { DateTime = DateTime.Now, Temperature = Temperature_SP, Time = Hour_SP + Minute_SP / 60 });
        }
        private void ConfirmSetting(object sender, EventArgs args)
        {
            _waterProofing.SendStatus("confirm");
        }
    }
}
