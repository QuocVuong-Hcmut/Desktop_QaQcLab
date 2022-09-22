using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    public class TestPOST
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TestPurpose { get; set; }
        public string ProductId { get; set; }
        public string EmployeeId { get; set; }
        public string Note { get; set; }
    }
}
