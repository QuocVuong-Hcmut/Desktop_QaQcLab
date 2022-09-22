
using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop_cha_qaqc_phase2.Core.Services.Implement
{
    public class S71200WaterProofingMachineService : IS71200WaterProofingMachineService
    {

        private readonly ControlPlcService _controlPlcService;

        public S71200WaterProofingMachineService(ControlPlcService controlPlcService)
        {
            _controlPlcService = controlPlcService;
            _controlPlcService._waterproofing.DataReceived += DataReceivedHandler;
            _controlPlcService._waterproofing.PlcNotConnected += PlcNotConnectedHandler;
        }
        public event Action<WaterProofingMachineMonitoringData> DataUpdated;
        private void DataReceivedHandler(WaterProofingMachineRawData dataStruct)
        {
            WaterProofingMachineMonitoringData monitoringData = new WaterProofingMachineMonitoringData();
            monitoringData.HourSP = (int)dataStruct.Hour_SP ;
            monitoringData.MinuteSP = (int)dataStruct.Minute_SP ;
            monitoringData.TemperatureSP = (int)dataStruct.Temperature_SP;
            monitoringData.HourPV = (int)dataStruct.Hour_PV;
            monitoringData.MinutePV = (int)dataStruct.Minute_PV;
            monitoringData.SecondPV = (int)dataStruct.Second_PV;
            monitoringData.TemperaturePV = (float)dataStruct.Temperature_PV;
            monitoringData.Complete_System = dataStruct.Complete_System;
            monitoringData.HeatingRegister = dataStruct.Heating_Register;
            monitoringData.GreenLight = dataStruct.Green_Light;
            monitoringData.RedLight = dataStruct.Red_Light;
            monitoringData.WarningSystem = dataStruct.Warn_System;
            monitoringData.ErrorSystem = dataStruct.Error_System;
            monitoringData.ErrorCode = (int)dataStruct.Error_Code;
            monitoringData.TemperatureDelay = (int)dataStruct.Temperature_Delay;
            monitoringData.NumberPreReport=(int)dataStruct.Number_of_Report;
            DataUpdated?.Invoke(monitoringData);
        }
        private void PlcNotConnectedHandler()
        {
            MessageBox.Show("Plc WaterProofing not connected", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void SendTime(int data, int offset)
        {
            _controlPlcService._waterproofing.SendTime(offset, data);
        }
        public void SendDataReal(float data, int offset)
        {
            _controlPlcService._waterproofing.SendDataReal(offset, data);
        }
        public void SendData2Byte (int data,int offset)
        {
            _controlPlcService._waterproofing.SendData2Byte(offset,data);
        }
        public void SendStatus(string s)
        {
            _controlPlcService._waterproofing.SetMemoryBit(s.ToLower());
        }

    }
}
