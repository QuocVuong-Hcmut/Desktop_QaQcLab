using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_
{
    public struct ForcedCloseMachineRawData
    {
        public int TimeCloseSP;
        public int TimeClosePV;
        public int TimeOpenSP;
        public int TimeOpenPV;
        public float ClosingSmoothTime;
        public int NumberOfClosingSP;
        public int NumberOfClosingPV;
    }

}
