using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{

    public class ForcedCloseTestGET : TestGET
    {
        public List<ForcedCloseTestSampleGET> samples { get; set; }
    }
    public class ForcedCloseTestSampleGET : SampleGET
    {
        public ForcedCloseTestSampleResultAPI SeatLidResult { get; set; }
        public ForcedCloseTestSampleResultAPI SeatRingResult { get; set; }
        public int NumberOfClosing { get; set; }

    }
    public class ForcedCloseTestSampleResultAPI
    {
        public int FallTime { get; set; }
        public bool IsIntact { get; set; }
        public bool IsUnleaked { get; set; }
        public bool Passed { get; set; }

    }
    public class ForcedCloseTestPOST 
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TestPurpose { get; set; }
        public string ProductId { get; set; }
        public string EmployeeId { get; set; }
        public string Note { get; set; }
        public List<ForcedCloseTestSamplePOST> samples { get; set; }
    }
    public class ForcedCloseTestSamplePOST
    {
        public ForcedCloseTestSampleResultAPI SeatLidResult { get; set; }
        public ForcedCloseTestSampleResultAPI SeatRingResult { get; set; }
        public int NumberOfClosing { get; set; }
        public int NumberOfError { get; set; }
        public string Note { get; set; }

    }
}


