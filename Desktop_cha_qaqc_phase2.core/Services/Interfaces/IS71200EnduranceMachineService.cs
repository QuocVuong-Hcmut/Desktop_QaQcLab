using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Services.Interfaces
{
    public interface IS71200EnduranceMachineService
    {
        
        event Action<EnduranceMachineMonitoringData> DataUpdated;
        void SendStatus(string s);
        void SendataUint(Int16 data, int offset);
        void SendataReal(Single data, int offset);
        void SendTime(Int32 data, Int32 offset);
        void SetMemmoryBit(int offset, int bit, bool status, int StatusSelect);
        void SetMemmoryStatus(int offset, int bit, bool status);
    }

}
