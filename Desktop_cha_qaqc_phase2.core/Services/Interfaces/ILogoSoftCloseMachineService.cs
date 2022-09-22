using Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Service.Interfaces
{
    public interface ILogoSoftCloseMachineService
    {
       
        void Send2Bytes(Int16 data, int offset);
        void Send4byte(Int32 data, int offset);

        void SendStatus(string s);
        event Action<SoftCloseMachineMonitoringData> DataUpdated;
        event Action ResetStatus; 
    }
}
