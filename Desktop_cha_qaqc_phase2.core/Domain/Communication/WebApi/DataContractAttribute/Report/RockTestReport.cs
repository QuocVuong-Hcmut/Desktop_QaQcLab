using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    #region GET
    public class RockTestGET : TestGET
    {
        public List<RockTestSampleGET> Samples { get; set; }
    }
    public class RockTestSampleGET : SampleGET
    {
        public double Load { get; set; }
        public int TestedTimes { get; set; }
        public bool Passed { get; set; }
    }
    #endregion
    #region POST
    public class RockTestPOST : TestPOST
    {
        public List<RockTestSamplePOST> Samples { get; set; }
    }
    public class RockTestSamplePOST : SamplePOST
    {
        public double Load { get; set; }
        public int TestedTimes { get; set; }
        public bool Passed { get; set; }
    }
    #endregion
}
