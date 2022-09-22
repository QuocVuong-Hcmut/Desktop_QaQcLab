using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Services.Interfaces
{
    public interface IS71200WaterProofingService
    {
        public event Action<WaterProofingMachineRawData> DataReceived;
        public event Action PlcNotConnected;
        public void SendTime(int offset, int data);
        Task ReadData();
        public void SetMemoryBit(string s);
        void SendDataReal(int offset, float data);
        void SendData2Byte (int offset,int data);
    }

}
