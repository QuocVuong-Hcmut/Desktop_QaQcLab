using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    public class Tester
    {
        
            public string employeeId { get; set; }
        public string testerID { get { return employeeId; }  }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public override string ToString()
        {
            return firstName + " " + lastName;
            
        }
        public  string ToToggleString ( )
        {
            return lastName+" "+firstName;

        }

    }
}
