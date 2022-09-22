using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class RockTestReportRepository : IRockTestReportRepository
    {
        private readonly ApplicationDbContext _context;
        public RockTestReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async void UpdateAsync (PreReportEndurance preReportEndurance)
        {

            _context.PreReportEndurances.Update(preReportEndurance);
            await _context.SaveChangesAsync( );
        }
        public async Task InsertAsync(RockTestSample reports)
        {
            await _context.AddAsync(reports);
        }
        public async Task<IList<RockTestSample>> LoadAsync()
        {
            try
            {
                var result = await _context.RockTestSamples.ToListAsync();
                return result;
            }
            catch
            {
                return null;

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
        public async Task UpdateAsync(IEnumerable<RockTestSample> testsamples)
        {
            foreach (var testsample in testsamples)
            {
                var unmodifiedSample = await (from p in _context.RockTestSamples
                                              where p.Id == testsample.Id
                                              select p).FirstOrDefaultAsync();
                if (unmodifiedSample != null)
                {
                    unmodifiedSample.Load = testsample.Load;
                    unmodifiedSample.TestedTimes = testsample.TestedTimes;
                    unmodifiedSample.Passed = testsample.Passed;
                    unmodifiedSample.NumberOfError = testsample.NumberOfError;
                    unmodifiedSample.Note = testsample.Note;
                    unmodifiedSample.Tester = testsample.Tester;
                }

            }
        }
    }
}
