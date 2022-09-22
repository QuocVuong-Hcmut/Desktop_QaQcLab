using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    #region GET
    public class WaterProofingTestGET :TestGET
    {
        public List<WaterProofingSampleGET> Samples { get; set; }
    }
    public class WaterProofingSampleGET : SampleGET
    {
        public double Temperature { get; set; }
        public int Duration { get; set; }
        public bool Passed { get; set; }
    }
    #endregion
    #region POST
    public class WaterProofingTestPOST : TestPOST
    {
        public List<WaterProofingSamplePOST> Samples { get; set; }
    }
    public class WaterProofingSamplePOST : SamplePOST
    {
        public double Temperature { get; set; }
        public int Duration { get; set; }
        public bool Passed { get; set; }

    }
    #endregion
}
