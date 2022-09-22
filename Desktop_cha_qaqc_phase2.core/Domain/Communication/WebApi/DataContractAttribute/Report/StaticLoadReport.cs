using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    #region GET
    public class StaticLoadTestGET : TestGET
    {
        public List<StaticLoadTestSampleGET> Samples { get; set; }
    }
    public class StaticLoadTestSampleGET : SampleGET
    {
        public string Status { get; set; }

    }
    #endregion
    #region POST
    public class StaticLoadTestPOST : TestPOST
    {
        public List<StaticLoadTestSamplePOST> Samples { get; set; }
    }
    public class StaticLoadTestSamplePOST : SamplePOST
    {
        public string Status { get; set; }

    }
    #endregion
}
