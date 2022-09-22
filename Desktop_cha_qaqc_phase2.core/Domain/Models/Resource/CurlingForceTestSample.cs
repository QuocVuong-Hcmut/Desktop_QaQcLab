using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource
{
    public class CurlingForceTestSample : Sample
    {
        public double? Load { get; set; }
        public int? Duration { get; set; }
        public double? DeformationDegree { get; set; }
    }
}
