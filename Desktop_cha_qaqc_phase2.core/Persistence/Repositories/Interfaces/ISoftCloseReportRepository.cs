
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces
{
    public interface ISoftCloseReportRepository
    {
        Task InsertAsync(SoftCloseTestSample entryreliability);
        Task<IEnumerable<SoftCloseTestSample>> LoadAsync();
        Task<IList<SoftCloseTestSample>> GetAllByNumClosing(List<int> listsamples);
        Task RemoveByNumClosing(List<int> listsamples);
        Task UpdateByNumClosing(IList<SoftCloseTestSample> listsamples);
        Task ClearAsync();
    }
}
