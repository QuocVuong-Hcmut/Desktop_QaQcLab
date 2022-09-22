using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    #region GET
    public class CurlingForceTestGET : TestGET
    {
        public List<CurlingForceTestSampleGET> Samples { get; set; }
    }
    public class CurlingForceTestSampleGET : SampleGET
    {
        public double Load { get; set; }
        public int Duration { get; set; }
        public double DeformationDegree { get; set; }
    }
    #endregion
    #region POST
    public class CurlingForceTestPOST : TestPOST
    {
        public List<CurlingForceTestSamplePOST> Samples { get; set; }
    }
    public class CurlingForceTestSamplePOST : SamplePOST
    {
        public double Load { get; set; }
        public int Duration { get; set; }
        public double DeformationDegree { get; set; }
    }
    #endregion
}
