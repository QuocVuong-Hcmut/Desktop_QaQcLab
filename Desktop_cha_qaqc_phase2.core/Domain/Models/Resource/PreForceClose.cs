using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource
{
    public class PreForceClose
    {
        public int Id { get; set; }
        public int Number { set; get; }
        public int TimeClosing1 { set; get; }
        public int TimeClosing2 { set; get; }
        public bool IsReport { get; set; } = false;
    }
}
