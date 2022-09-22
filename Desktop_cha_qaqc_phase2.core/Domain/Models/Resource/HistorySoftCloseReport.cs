using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource
{
    public class HistorySoftCloseReport
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string NameTest { get; set; }
    }
}
