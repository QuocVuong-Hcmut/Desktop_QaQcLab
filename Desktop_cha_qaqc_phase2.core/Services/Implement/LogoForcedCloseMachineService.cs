using Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop_cha_qaqc_phase2.Core.Services.Implement
{
    public class LogoForcedCloseMachineService : ILogoForceCloseMachineService
    {
        private readonly ControlPlcService _controlPlcService;
        public string LogoAddress;
        public event Action<ForcedCloseMachineMonitoringData> DataUpdated;
        public IDatabaseService _databaseService;
        public event EventHandler ResetStatus;
        public LogoForcedCloseMachineService(ControlPlcService controlPlcService, IDatabaseService database)
        {   
            _databaseService = database;
            _controlPlcService = controlPlcService;
            _controlPlcService._forcedclose.DataReceived += DataRecivedHandler;
            _controlPlcService._forcedclose.PlcNotConnected += PlcNotConnectedHandler;
        }
        public void SendStatus(string s)
        {
            _controlPlcService._forcedclose.ControlActive(s.ToLower());
        }
        public void Send2Bytes(short data, int offset)
        {
            _controlPlcService._forcedclose.SendData2Byte(offset, data);
        }

        public void Send4byte(int data, int offset)
        {
            _controlPlcService._forcedclose.SendData4Byte(offset, data);
        }
        private void DataRecivedHandler(ForcedCloseMachineRawData componentResult, byte[] bufferM, byte[] bufferQ, byte[] bufferI) 
        {
            ForcedCloseMachineMonitoringData monitoringData = new ForcedCloseMachineMonitoringData();
            monitoringData.TimeClosePV = componentResult.TimeClosePV;
            monitoringData.TimeOpenPV = componentResult.TimeOpenPV;
            monitoringData.ClosingSmoothTime = componentResult.ClosingSmoothTime;
            monitoringData.NumberOfClosingPV = componentResult.NumberOfClosingPV;
            monitoringData.TimeCloseSP = componentResult.TimeCloseSP;
            monitoringData.TimeOpenSP = componentResult.TimeOpenSP;
            monitoringData.NumberOfClosingSP = componentResult.NumberOfClosingSP;
            monitoringData.Mode = Sharp7.S7.GetBitAt(bufferI, 0, 0);
            monitoringData.Run = Sharp7.S7.GetBitAt(bufferQ, 0, 6);
            //monitoringData.Greenlight = Sharp7.S7.GetBitAt(bufferQ,,)
            monitoringData.Warn = Sharp7.S7.GetBitAt(bufferQ, 0, 2);
            if (monitoringData.Warn)
            {
                if (Sharp7.S7.GetBitAt(bufferM, 0, 3) == true)
                {
                    monitoringData.ForceCloseWarningCode = 201;
                }
                if (Sharp7.S7.GetBitAt(bufferM, 0, 4) == true)
                {
                    monitoringData.ForceCloseWarningCode = 202;
                }
                if (Sharp7.S7.GetBitAt(bufferM, 0, 5) == true)
                {
                    monitoringData.ForceCloseWarningCode = 203;
                }
                if (Sharp7.S7.GetBitAt(bufferQ, 1, 0) == true)
                {
                    monitoringData.ForceCloseWarningCode = 204;
                }
                if (Sharp7.S7.GetBitAt(bufferQ, 1, 1) == true)
                {
                    monitoringData.ForceCloseWarningCode = 205;
                }
            } 
            DataUpdated?.Invoke(monitoringData);
        }
        private void PlcNotConnectedHandler ()
        {
            MessageBox.Show("Plc ForcedClose not connected", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
