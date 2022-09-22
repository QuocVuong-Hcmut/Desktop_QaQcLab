using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication
{
    public class ServerError
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}
