using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces
{
    public interface IHistoryEnduranceReportRepository
    {
        Task InsertAsync(HistoryEnduranceReport entry);
        IList<HistoryEnduranceReport> Load(DateTime timestart, DateTime timestop);
    }
}
