using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    #region GET
    public class SoftCloseTestGET : TestGET

    {
        public List<SoftCloseTestSampleGET> Samples { get; set; }
    }
    public class SoftCloseTestSampleGET : SampleGET
    {
        public SoftCloseTestSampleResultAPI SeatLidResult { get; set; }
        public SoftCloseTestSampleResultAPI SeatRingResult { get; set; }
        public int NumberOfClosing { get; set; }
    }
    #endregion
    #region POST
    public class SoftCloseTestPOST : TestPOST
    {
        public List<SoftCloseTestSamplePOST> Samples { get; set; }
    }
    public class SoftCloseTestSamplePOST : SamplePOST
    {
        public SoftCloseTestSampleResultAPI SeatLidResult { get; set; }
        public SoftCloseTestSampleResultAPI SeatRingResult { get; set; }
        public int NumberOfClosing { get; set; }

    }
    #endregion
    public class SoftCloseTestSampleResultAPI
    {
        public int FallTime { get; set; }
        public bool IsBumperIntact { get; set; }
        public bool IsUnleaked { get; set; }
        public bool Passed { get; set; }

    }

    
}
