using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;
using System.Threading;
using ProductVertificationDesktopApp.Core.Domain.Communication;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class ForcedCloseReportRepository : IForcedCloseReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ForcedCloseReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async void UpdateAsync (PreForceClose preForceClose)
        {

            _context.PreForceCloses.Update(preForceClose);
            await _context.SaveChangesAsync( );
        }
        public async Task InsertAsync(ForcedCloseTestSample entryreliability)
        {
            try
            {
                await _context.ForcedCloseTestSamples.AddAsync(entryreliability);
            }
            finally
            {
            }


        }
        public async Task<IList<ForcedCloseTestSample>?> LoadAsync()
        {
            try
            {
                return await _context.ForcedCloseTestSamples.ToListAsync();
                
            }
            finally
            {
            }
        }
        public async Task ClearDefomationSheet()
        {
            try
            {
                var list = await _context.ForcedCloseTestSamples.ToListAsync();
                foreach (var item in list)
                {
                    _context.ForcedCloseTestSamples.Remove(item);
                }
            }
            finally
            {
            }
        }
        public async Task UpdateReport(IEnumerable<ForcedCloseTestSample> reports)
        {
            foreach (var report in reports)
            {
                var unmodifiedReport = await (from p in _context.ForcedCloseTestSamples
                                              where p.Id == report.Id
                                              select p).FirstOrDefaultAsync();
                if (unmodifiedReport != null)
                {
                    unmodifiedReport.NumberOfClosing = report.NumberOfClosing;
                    unmodifiedReport.FallTimeLid = report.FallTimeLid;
                    unmodifiedReport.IsLidIntact = report.IsLidIntact;
                    unmodifiedReport.IsLidUnleaked = report.IsLidUnleaked;
                    unmodifiedReport.IsLidPassed = report.IsLidPassed;
                    unmodifiedReport.FallTimeRing = report.FallTimeRing;
                    unmodifiedReport.IsRingIntact = report.IsRingIntact;
                    unmodifiedReport.IsRingUnleaked= report.IsRingUnleaked;
                    unmodifiedReport.IsRingPassed = report.IsRingPassed;
                    unmodifiedReport.NumberOfError = report.NumberOfError;
                    unmodifiedReport.Note = report.Note;
                    unmodifiedReport.Tester = report.Tester;
                }
            }
        }
        public async Task<ServiceResponse> ClearPreReportAsync ( PreForceClose preForceClose=null )
        {
            try
            {
                if ( preForceClose!=null )
                {
                    _context.PreForceCloses.Remove(preForceClose);
                }
                else
                {
                    await _context.Database.ExecuteSqlRawAsync("DELETE FROM [PreForceCloses]");
                }
                return ServiceResponse.Successful( );
            }
            catch
            {
                Error error = new Error("Database.DELETE","Lỗi db");
                return ServiceResponse.Failed(error);
            }
        }

        public async Task InsertPreReportAsync (PreForceClose entry)
        {
            await _context.PreForceCloses.AddAsync(entry);
        }

        public async Task<IList<PreForceClose>> LoadPreReport ( )
        {
            return await _context.PreForceCloses.ToListAsync( );
        }
    }
}
