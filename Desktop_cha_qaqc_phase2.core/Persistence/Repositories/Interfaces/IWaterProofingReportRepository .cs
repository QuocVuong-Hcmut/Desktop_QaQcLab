using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces
{
    public interface IWaterProofingReportRepository
    {   
        Task UpdateReport(IList<WaterProofingTestSample> entry);
        Task<IEnumerable<WaterProofingTestSample>> LoadReportAsync();
        Task<ServiceResponse> ClearReportAsync();
        Task<IList<PreReportWaterProofing>> LoadPreReport ( );
        Task<ServiceResponse> ClearPreReportAsync (PreReportWaterProofing PreReportWaterProofing = null);
        Task InsertPreReportAsync (PreReportWaterProofing entry);
        void UpdateAsync (PreReportWaterProofing preReportWaterProofings);

    }
}
