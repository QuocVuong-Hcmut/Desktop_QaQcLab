using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces
{
    public interface IForcedCloseReportRepository
    {
        Task InsertAsync(ForcedCloseTestSample entryreliability);
        Task<IList<ForcedCloseTestSample>?> LoadAsync();
        Task ClearDefomationSheet();
        Task UpdateReport(IEnumerable<ForcedCloseTestSample> records);
        Task<IList<PreForceClose>> LoadPreReport ( );
        Task<ServiceResponse> ClearPreReportAsync (PreForceClose preForceClose = null);
        Task InsertPreReportAsync (PreForceClose entry);
        void UpdateAsync (PreForceClose preForceClose);
    }
}
