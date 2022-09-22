using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    public class TestGET
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Product Product { get; set; }
        public int TestPurpose { get; set; }
        public string Note { get; set; }
    }
}
