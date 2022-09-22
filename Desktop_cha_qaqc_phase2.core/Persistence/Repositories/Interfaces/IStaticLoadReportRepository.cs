using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces
{
    public interface IStaticLoadReportRepository
    {
        Task InsertAsync(StaticLoadTestSample entry);
        Task<IList<StaticLoadTestSample>> LoadAsync();
        Task ClearAsync(PreReportEndurance preReportEndurance);
        Task UpdateAsync(IEnumerable<StaticLoadTestSample> sheets);
        void UpdateAsync (PreReportEndurance preReportEndurance);
    }
}
