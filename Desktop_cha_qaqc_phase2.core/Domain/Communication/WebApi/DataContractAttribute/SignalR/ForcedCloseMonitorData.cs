using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    public class ForcedCloseMonitorData
    {
        public bool Status { get; set; }
        public bool Alarm { get; set; }
        public int NumberClosingSp { get; set; }
        public int NumberClosingPv { get; set; }
        public int TimeLidClose { get; set; }
        public int TimeLidOpen { get; set; }
    }
}
