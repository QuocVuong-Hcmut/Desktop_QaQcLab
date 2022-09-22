using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    public class EnduranceMonitorData
    {
        public int NumberOfTestPv1 { get; set; }
        public int NumberOfTestPv2 { get; set; }
        public int NumberOfTestPv3 { get; set; }
        public int NumberOfTestSp12 { get; set; }
        public int NumberOfTestSp3 { get; set; }
        public int ErrorCode { get; set; }
        public int ModeStatus { get; set; }
        public double ForceCylinderSp12 { get; set; }
        public double ForceCylinderSp3 { get; set; }
        public double TimeHoldSp12 { get; set; }
        public double TimeHoldSp3 { get; set; }
        public bool Seclect1 { get; set; }
        public bool Seclect2 { get; set; }
        public bool RedStatus { get; set; }
        public bool GreenStatus { get; set; }
        public bool ErrorStatus { get; set; }
    }
}
