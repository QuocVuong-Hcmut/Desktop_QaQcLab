using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_
{
    public struct SoftCloseMachineMonitoringData
    {
        public bool Mode { get; set; }
        public bool Run { get; set; }
        public bool Warn { get; set; }
        public int TimeOpenPV { get; set; }
        public int TimeClosePV { get; set; }
        public float SmoothTimeClosing { get; set; }
        public float SmoothTimeClosingPlinth { get; set; }
        public int NumberOfClosingPV { get; set; }
        public int TimeCloseSP { get; set; }
        public int TimeOpenSP { get; set; }
        public int NumberOfClosingSP { get; set; }
        public int SoftCloseWarningCode;

    }
}
