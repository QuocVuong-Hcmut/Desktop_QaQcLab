using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource
{
    public  class PreReportEndurance
    {
        public DateTime DateTime { get; set; }
        public int Id { set; get; }
        public int Time1 { set; get; }
        public int Time2 { set; get; }
        public int Force1 { set; get; }
        public int Force2 { set; get; }
        public int Number1 { set; get; }
        public int Number2 { set; get; }
        public bool IsReport { get; set; } = false;

    }
}
