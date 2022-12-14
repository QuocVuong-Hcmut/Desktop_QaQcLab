using Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Service.Interfaces
{
    public interface ILogoForceCloseService
    {
        void ControlActive(string s);
        void SendData2Byte(int offset, Int16 data);
        Task ReadData();
        void SendData4Byte(int offset, Int32 data);

        event Action<ForcedCloseMachineRawData, byte[], byte[], byte[]> DataReceived;
        event Action PlcNotConnected;

    }
}
