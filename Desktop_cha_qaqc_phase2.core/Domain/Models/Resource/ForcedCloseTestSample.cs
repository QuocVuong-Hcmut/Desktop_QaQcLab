using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource
{
    public class ForcedCloseTestSample :Sample
    {
        public int NumberOfClosing { get; set; }
        public double FallTimeLid { get; set; }
        public bool IsLidIntact { get; set; }
        public bool IsLidUnleaked { get; set; }
        public bool IsLidPassed { get; set; }
        public double FallTimeRing { get; set; }
        public bool IsRingIntact { get; set; }
        public bool IsRingUnleaked { get; set; }
        public bool IsRingPassed { get; set; }
    }
}
