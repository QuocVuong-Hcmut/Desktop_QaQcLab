using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource
{
    public class Sample
    {
        public int Id { get; set; }
        public int IdPreReport { get; set; }
        public bool IsReport { get; set; }
        public int NumberOfError { get; set; } = 0;
        public string Note { get; set; } = "";
        public string Tester { get; set; } = "";
    }
}
