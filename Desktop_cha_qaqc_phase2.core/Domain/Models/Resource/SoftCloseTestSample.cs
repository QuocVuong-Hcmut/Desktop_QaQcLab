using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource
{
    public class SoftCloseTestSample : Sample
    {
        public int NumberOfClosing { get; set; }
        public double FallTimeLid { get; set; }
        public bool IsBumperLidIntact { get; set; }
        public bool IsBumperLidUnleaked { get; set; }
        public bool IsLidPassed { get; set; }
        public double FallTimeRing { get; set; }
        public bool IsBumperRingIntact { get; set; }
        public bool IsBumperRingUnleaked { get; set; }
        public bool  IsRingPassed { get; set; }
    }
}
