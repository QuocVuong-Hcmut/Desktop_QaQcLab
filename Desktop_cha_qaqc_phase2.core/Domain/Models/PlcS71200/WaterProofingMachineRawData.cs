using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200
{
    public struct WaterProofingMachineRawData
    {
        public Int32 Hour_SP;
        public Int32 Minute_SP;
        public Int32 Hour_PV;
        public Int32 Minute_PV;
        public Int32 Second_PV;
        public float Temperature_SP;
        public float Temperature_PV;
        public bool Heating_Register;
        public bool Green_Light;
        public bool Red_Light;
        public bool Error_System;
        public bool Warn_System;
        public bool Complete_System;
        public Int16 Error_Code;
        public Int16 Temperature_Delay;
        public Int16 Number_of_Report;
    }
}
