using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Services.Interfaces
{
    public interface ISignalRService
    {
        Task<ServiceResponse> SoftCloseMonitoringData(SoftCloseMonitorData monitoringData);
        Task<ServiceResponse> ForcedCloseMonitoringData(ForcedCloseMonitorData monitoringData);
        Task<ServiceResponse> WaterProofingMonitorData(WaterProofingMonitorData monitoringData);
        Task<ServiceResponse> EnduranceMonitoringData(EnduranceMonitorData monitoringData);
    }
}
