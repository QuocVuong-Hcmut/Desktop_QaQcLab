
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using System.Collections.ObjectModel;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class CurlingForceReportRepository : ICurlingForceReportRepository
    {
        private readonly ApplicationDbContext _context;
        public CurlingForceReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async void UpdateAsync (PreReportEndurance preReportEndurance)
        {

            _context.PreReportEndurances.Update(preReportEndurance);
            await _context.SaveChangesAsync( );
        }
        public async Task InsertAsync(ObservableCollection<CurlingForceTestSample> reports)
        {
            foreach (var report in reports)
            {
                var unmodifiedReport = (from p in _context.CurlingForceTestSamples
                                        where p.Id == report.Id
                                        select p).FirstOrDefault();
                if (unmodifiedReport != null)
                {
                    unmodifiedReport.Load = report.Load;
                    unmodifiedReport.Duration = report.Duration;
                    unmodifiedReport.DeformationDegree = report.DeformationDegree;
                    unmodifiedReport.NumberOfError = report.NumberOfError;
                    unmodifiedReport.Note = report.Note;
                    unmodifiedReport.Tester = report.Tester;
                }
            }
        }
        public async Task ClearAsync (PreReportEndurance preReportEndurance = null)
        {
            try
            {
                if ( preReportEndurance!=null )
                {
                    _context.PreReportEndurances.Remove(preReportEndurance);
                }
                else
                {
                    await _context.Database.ExecuteSqlRawAsync("DELETE FROM [StaticLoadTestSheets]");
                }
            }
            catch
            {
            }
        }
        public async Task<IList<CurlingForceTestSample>> LoadAsync()
        {
            try
            {
                try
                {
                    var result = await _context.CurlingForceTestSamples.ToListAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    IList<CurlingForceTestSample> curlingForceTestSheetResource = new List<CurlingForceTestSample>();
                    return curlingForceTestSheetResource;
                }
            }
            finally
            {
            }
        }
        public async Task UpdateAsync(IEnumerable<CurlingForceTestSample> testsamples)
        {
            foreach (var testsample in testsamples)
            {
                var unmodifiedSample = await (from p in _context.CurlingForceTestSamples
                                              where p.Id == testsample.Id
                                              select p).FirstOrDefaultAsync();
                if (unmodifiedSample != null)
                {
                    unmodifiedSample.Load = testsample.Load;
                    unmodifiedSample.Duration = testsample.Duration;
                    unmodifiedSample.DeformationDegree = testsample.DeformationDegree;
                    unmodifiedSample.NumberOfError = testsample.NumberOfError;
                    unmodifiedSample.Note = testsample.Note;
                    unmodifiedSample.Tester = testsample.Tester;
                }

            }
        }
    }
}