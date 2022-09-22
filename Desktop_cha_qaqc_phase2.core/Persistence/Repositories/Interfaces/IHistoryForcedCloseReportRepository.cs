using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces
{
    public interface IHistoryForcedCloseReportRepository
    {
        Task InsertAsync(HistoryForcedCloseReport entry);
        IList<HistoryForcedCloseReport> LoadAsync(DateTime timestart, DateTime timestop);
    }
}
