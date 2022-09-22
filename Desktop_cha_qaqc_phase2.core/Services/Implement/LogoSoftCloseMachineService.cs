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
    public class LogoSoftCloseMachineService: ILogoSoftCloseMachineService
    {
        private readonly ControlPlcService _controlPlcService;
        public string LogoAddress;
        public int preNumberOfClosingPV;
        public event Action<SoftCloseMachineMonitoringData> DataUpdated;
        public IDatabaseService _databaseService;
        public event Action ResetStatus;
        public LogoSoftCloseMachineService(ControlPlcService controlPlcService, IDatabaseService databaseService)
        {
            _controlPlcService = controlPlcService;
            _databaseService = databaseService;
            _controlPlcService._softclose.DataReceived += DataRecivedHandler;
            _controlPlcService._softclose.PlcNotConnected += PlcNotConnectedHandler;
            Iniliazation();
        }

        private async void Iniliazation()
        {
            var data = await _databaseService.LoadSoftCloseTestReport();
            if((data != null)&& (data.Count() != 0 ))
            {
                preNumberOfClosingPV = data.ElementAt(data.Count() - 1).NumberOfClosing;
            }
            else
            {
                preNumberOfClosingPV = 0;
            }

        }

        public void SendStatus(string s)
        {
            _controlPlcService._softclose.ControlActive(s.ToLower());
        }
        public void Send2Bytes(short data, int offset)
        {
            _controlPlcService._softclose.SendData2Byte(offset, data);
        }
        
        public void Send4byte(int data, int offset)
        {
            _controlPlcService._softclose.SendData4Byte(offset, data);
        }
        private void DataRecivedHandler(SoftCloseMachineRawData componentResult, byte[] bufferM, byte[] bufferQ, byte[] bufferI)
        {
            SoftCloseMachineMonitoringData monitoringData = new SoftCloseMachineMonitoringData();
            monitoringData.TimeClosePV = componentResult.TimeClosePV;
            monitoringData.TimeOpenPV = componentResult.TimeOpenPV;
            monitoringData.SmoothTimeClosing = (float)Math.Round(componentResult.TimeClosingSmooth,3);
            monitoringData.SmoothTimeClosingPlinth = (float)Math.Round(componentResult.TimeClosingSmoothPlinth, 3);
            //reset
            if (componentResult.NumberClosingPV == 0 || preNumberOfClosingPV > componentResult.NumberClosingPV)
            {
                ResetStatus?.Invoke();
            }
            monitoringData.NumberOfClosingPV = componentResult.NumberClosingPV;
            preNumberOfClosingPV = componentResult.NumberClosingPV;
            monitoringData.TimeCloseSP = componentResult.TimeCloseSP;
            monitoringData.TimeOpenSP = componentResult.TimeOpenSP;
            monitoringData.NumberOfClosingSP = componentResult.NumberClosingSP;
            monitoringData.Run = Sharp7.S7.GetBitAt(bufferQ, 0, 6);
            monitoringData.Warn = Sharp7.S7.GetBitAt(bufferQ, 0, 2);
            monitoringData.Mode = Sharp7.S7.GetBitAt(bufferI, 0, 0);
            
            if (monitoringData.Warn)
            {
                if( Sharp7.S7.GetBitAt(bufferM, 0, 0))
                {
                    monitoringData.SoftCloseWarningCode = 101;
                }
                if (Sharp7.S7.GetBitAt(bufferM, 0, 1))
                {
                    monitoringData.SoftCloseWarningCode = 102;
                }
                if (Sharp7.S7.GetBitAt(bufferM, 0, 6))
                {
                    monitoringData.SoftCloseWarningCode = 103;
                }
                if (Sharp7.S7.GetBitAt(bufferQ, 1, 0))
                {
                    monitoringData.SoftCloseWarningCode = 104;
                }
                if (Sharp7.S7.GetBitAt(bufferQ, 1, 1))
                {
                    monitoringData.SoftCloseWarningCode = 105;
                }

                if (Sharp7.S7.GetBitAt(bufferQ, 1, 5) )
                {
                    monitoringData.SoftCloseWarningCode = 106;
                }
            } 
            DataUpdated?.Invoke(monitoringData);
        }
        private void PlcNotConnectedHandler ()
        {
            MessageBox.Show("Logo SoftClose not connected", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
