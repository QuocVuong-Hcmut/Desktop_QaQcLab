using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_
{
    public struct SoftCloseMachineRawData
        {
            public int TimeCloseSP;
            public int TimeClosePV;
            public int TimeOpenSP;
            public int TimeOpenPV;
            public float TimeClosingSmooth;
            public float TimeClosingSmoothPlinth;
            public int NumberClosingSP; 
            public int NumberClosingPV; 
        }
}
