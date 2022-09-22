using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    public class AlarmCode
    {
        public DateTime Timestamp { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
