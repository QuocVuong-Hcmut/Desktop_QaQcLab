using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute
{
    public class WaterProofingMonitorData
    {
        public bool Status { get; set; }
        public bool Alarm { get; set; }
        public double TemperatureSp { get; set; }
        public double HourSp { get; set; }
        public double MinuteSp { get; set; }
        public double TemperaturePv { get; set; }
        public double HourPv { get; set; }
        public double MinutePv { get; set; }
        public double SecondPv { get; set; }
    }
}
