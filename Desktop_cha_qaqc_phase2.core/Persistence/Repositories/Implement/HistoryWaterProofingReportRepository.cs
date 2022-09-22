using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class HistoryWaterProofingReportRepository: IHistoryWaterProofingReportRepository
    {
        private readonly ApplicationDbContext _context;
        public HistoryWaterProofingReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task InsertAsync(HistoryWaterProofingReport entry)
        {
            await _context.HistoryWaterProofingReports.AddAsync(entry);
        }
        public IList<HistoryWaterProofingReport> Load(DateTime timestart,DateTime timestop)
        {
            var _timestart = timestart.AddHours(0).AddMinutes(0).AddSeconds(0);
            var _timestop = timestop.AddHours(23).AddMinutes(59).AddSeconds(59);
            var data= _context.HistoryWaterProofingReports
                .Where(f => f.Timestamp >= _timestart && f.Timestamp <= _timestop)
                .ToList();
            return data;
        }
    }
}
