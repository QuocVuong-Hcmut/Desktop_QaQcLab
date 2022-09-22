using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Desktop_cha_qaqc_phase2.Core.Services.Interfaces
{
    public interface IS71200EnduranceService
    {
        void SetMemoryBit(string s);
        void SetMemMoryBitStatus(int offset, int bit, bool status);
        void SetMemoryBitData(int offset, int bit, bool status, int StatusSelect);
        void SendDataUint(int offset, Int16 data);
        void SendDataReal(int offset, Single data);
        void SendTime(int offset, int data);
        
        Task ReadData();
        event Action<EnduranceMachineRawData> DataReceived;
        event Action PlcNotConnected;
    }

}
