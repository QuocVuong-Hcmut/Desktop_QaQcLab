using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces
{
    public interface IRockTestReportRepository
    {
        Task InsertAsync(RockTestSample entry);
        Task<IList<RockTestSample>> LoadAsync();
        Task ClearAsync (PreReportEndurance preReportEndurance);
        Task UpdateAsync(IEnumerable<RockTestSample> entry);
        void UpdateAsync (PreReportEndurance preReportEndurance);

    }
}
