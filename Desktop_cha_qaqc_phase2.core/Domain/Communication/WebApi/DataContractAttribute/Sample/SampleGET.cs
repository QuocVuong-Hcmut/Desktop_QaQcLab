using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    public class SampleGET
    {
        public int NumberOfError { get; set; }
        public string Note { get; set; }
        public Tester Tester { get; set; }
    }
}
