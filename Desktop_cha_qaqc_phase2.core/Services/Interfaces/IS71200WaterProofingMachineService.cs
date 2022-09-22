using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Services.Interfaces
{
    public interface IS71200WaterProofingMachineService
    {
        
        public event Action<WaterProofingMachineMonitoringData> DataUpdated;
        void SendStatus(string s);

    }

}
