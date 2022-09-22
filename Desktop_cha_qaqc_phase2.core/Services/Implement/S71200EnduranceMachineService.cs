
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
    public class S71200EnduranceMachineService : IS71200EnduranceMachineService
    {
       
        private readonly ControlPlcService _controlPlcService;

        public S71200EnduranceMachineService(ControlPlcService controlPlcService)
        {
            _controlPlcService = controlPlcService;
            _controlPlcService._endurance.DataReceived += DataReceivedHandler;
            _controlPlcService._endurance.PlcNotConnected += PLCNotConnectedHandler;
        }

        private void PLCNotConnectedHandler()
        {
            MessageBox.Show("Plc Endurance not connected", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        public event Action<EnduranceMachineMonitoringData> DataUpdated;
    
        private void DataReceivedHandler(EnduranceMachineRawData testingStruct)
        {
            EnduranceMachineMonitoringData monitorData = new EnduranceMachineMonitoringData();
            monitorData.Cylinder1ForceProcessValue = (int)testingStruct.PV_Force_Cylinder_1;
            monitorData.Cylinder2ForceProcessValue = (int)testingStruct.PV_Force_Cylinder_2;
            monitorData.Cylinder3ForceProcessValue = (int)testingStruct.PV_Force_Cylinder_3;
            monitorData.NumberOfPresses1ProcessValue = (int)testingStruct.PV_No_Press_1;
            monitorData.NumberOfPresses2ProcessValue = (int)testingStruct.PV_No_Press_2;
            monitorData.NumberOfPresses3ProcessValue = (int)testingStruct.PV_No_Press_3;
            monitorData.Cylinder12ForceSP = (int)testingStruct.SP_Force_Cylinder_12;
            monitorData.Cylinder3ForceSP = (int)testingStruct.SP_Force_Cylinder_3;
            monitorData.NumberOfPresses12SP = (int)testingStruct.SP_No_Press_12;
            monitorData.NumberOfPresses3SP = (int)testingStruct.SP_No_Press_3;
            monitorData.HoldingTime12SP = (int)testingStruct.SP_Time_Hold_12 / 1000;
            monitorData.HoldingTime3SP = (int)testingStruct.SP_Time_Hold_3 / 1000;
            monitorData.HoldingTime1ProcessValue = (int)testingStruct.PV_Time_Hold_1 / 1000;
            monitorData.HoldingTime2ProcessValue = (int)testingStruct.PV_Time_Hold_2 / 1000;
            monitorData.HoldingTime3ProcessValue = (int)testingStruct.PV_Time_Hold_3 / 1000;
            monitorData.ErrorCode = (int)testingStruct.Error_Code;
            monitorData.Start    = testingStruct.Green_App;
            monitorData.Warning = testingStruct.Red_App;
            monitorData.SelectSystem1 = testingStruct.Seclec_1_App;
            monitorData.SelectSystem2 = testingStruct.Seclec_2_App;
            monitorData.ErrorStatus = testingStruct.Error_App;
            monitorData.Mode = testingStruct.Mode;
            monitorData.NumberReport=testingStruct.Number_of_report;

            DataUpdated?.Invoke(monitorData);
        }
        public void SendStatus(string s)
        {
            _controlPlcService._endurance.SetMemoryBit(s.ToLower());
        }
        public void SendataUint(Int16 data, int offset)
        {
            _controlPlcService._endurance.SendDataUint(offset, data);
        }
        public void SendataReal(Single data, int offset)
        {
            _controlPlcService._endurance.SendDataReal(offset, data);
        }
        public void SendTime(int data, int offset)
        {
            _controlPlcService._endurance.SendTime(offset, data);
        }
        public void SetMemmoryBit(int offset, int bit, bool status, int StatusSelect)
        {
            _controlPlcService._endurance.SetMemoryBitData(offset, bit, status, StatusSelect);
        }
        public void SetMemmoryStatus(int offset, int bit, bool status)
        {
            _controlPlcService._endurance.SetMemMoryBitStatus(offset, bit, status);
        }
        
    }

}
