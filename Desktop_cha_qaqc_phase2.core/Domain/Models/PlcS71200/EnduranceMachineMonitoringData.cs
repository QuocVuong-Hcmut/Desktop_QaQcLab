using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200
{
    public struct EnduranceMachineMonitoringData
    {
        public int Cylinder12ForceSP { get; set; }
        public int Cylinder3ForceSP { get; set; }
        public int Cylinder1ForceProcessValue { get; set; }
        public int Cylinder2ForceProcessValue { get; set; }
        public int Cylinder3ForceProcessValue { get; set; }
        public int HoldingTime12SP { get; set; }
        public int HoldingTime3SP { get; set; }
        public int HoldingTime1ProcessValue { get; set; }
        public int HoldingTime2ProcessValue { get; set; }
        public int HoldingTime3ProcessValue { get; set; }
        public int NumberOfPresses12SP { get; set; }
        public int NumberOfPresses3SP { get; set; }
        public int NumberOfPresses1ProcessValue { get; set; }
        public int NumberOfPresses2ProcessValue { get; set; }
        public int NumberOfPresses3ProcessValue { get; set; }
        public int ErrorCode { get; set; }
        public bool SelectSystem1 { get; set; }
        public bool SelectSystem2 { get; set; }
        public bool Start { get; set; }
        public int Mode { get; set; }
        public bool Warning { get; set; }
        public bool ErrorStatus { get; set; }
        public bool GreenApp { get; set; }
        public bool RedApp { get; set; }
        public int NumberReport { get; set; }
    }
}
