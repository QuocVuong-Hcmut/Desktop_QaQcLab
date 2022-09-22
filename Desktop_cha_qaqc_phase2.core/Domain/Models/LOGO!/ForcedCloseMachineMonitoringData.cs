using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_
{

    public struct ForcedCloseMachineMonitoringData
    {
     
        public int TimeOpenPV;
        public int TimeClosePV;
        public float ClosingSmoothTime;
        public int NumberOfClosingPV;
        public int TimeCloseSP;
        public int TimeOpenSP;
        public int NumberOfClosingSP;
        public int ForceCloseWarningCode;
        public bool Mode;
        public bool Run;
        public bool Greenlight;
        public bool Warn;
   
    }
}
