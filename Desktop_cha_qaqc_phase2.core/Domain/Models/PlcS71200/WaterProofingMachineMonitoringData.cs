using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200
{
    public struct WaterProofingMachineMonitoringData
    {
        public int HourSP;
        public int MinuteSP;
        public int TemperatureSP;
        public int HourPV;
        public int MinutePV;
        public int SecondPV;
        public float TemperaturePV;
        public bool Complete_System;
        public bool HeatingRegister;
        public bool GreenLight;
        public bool RedLight;
        public bool ErrorSystem;
        public bool WarningSystem;
        public int ErrorCode;
        public int TemperatureDelay;
        public int NumberPreReport;
    }
}
