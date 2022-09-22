using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource
{
    public class PreReportWaterProofing
    {
        public int Id { set; get; }
        public DateTime DateTime { get; set; }
        public int Temperature { get; set; }
        public Double Time { get; set; }
        public bool IsReport { get; set; } = false;
    }
}
