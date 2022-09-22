using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200
{
    public struct EnduranceMachineRawData
    {
        public Single PV_Force_Cylinder_1;
        public Single PV_Force_Cylinder_2;
        public Single PV_Force_Cylinder_3;
        public Int16 PV_No_Press_1;
        public Int16 PV_No_Press_2;
        public Int16 PV_No_Press_3;
        public Int32 PV_Time_Hold_1;
        public Int32 PV_Time_Hold_2;
        public Int32 PV_Time_Hold_3;
        public Single SP_Force_Cylinder_12;
        public Single SP_Force_Cylinder_3;
        public Int16 SP_No_Press_12;
        public Int16 SP_No_Press_3;
        public Int32 SP_Time_Hold_12;
        public Int32 SP_Time_Hold_3;
        public bool Seclec_1_App;
        public bool Seclec_2_App;
        public bool Mode_App;
        public bool Green_App;
        public bool Red_App;
        public bool Error_App;
        public bool Empty_mode1;
        public bool Empty_mode2;
        public Byte Empty;
        public Int16 Error_Code;
        public Int16 Mode;
        public Int16 Number_of_report;
    }
}
