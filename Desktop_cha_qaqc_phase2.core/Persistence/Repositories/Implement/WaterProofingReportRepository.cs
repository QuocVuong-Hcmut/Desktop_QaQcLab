
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
using ProductVertificationDesktopApp.Core.Domain.Communication;
using ProductVertificationDesktopApp.Core.Domain;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class WaterProofingReportRepository : IWaterProofingReportRepository
    {
        private readonly ApplicationDbContext _context;
        public WaterProofingReportRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async void UpdateAsync (PreReportWaterProofing preReportWaterProofings)
        {

            _context.PreReportWaterProofings.Update(preReportWaterProofings);
            await _context.SaveChangesAsync( );
        }

        public async Task UpdateReport(IList<WaterProofingTestSample> reports)
        {
            foreach (var report in reports)
            {
                var unmodifiedReport = await (from p in _context.WaterProofingTestSamples
                                              where p.Id == report.Id
                                              select p).FirstOrDefaultAsync();
                if (unmodifiedReport != null)
                {
                    unmodifiedReport.Note = report.Note;
                    unmodifiedReport.Duration = report.Duration;
                    unmodifiedReport.NumberOfError = report.NumberOfError;
                    unmodifiedReport.Passed = report.Passed;
                    unmodifiedReport.Temperature = report.Temperature;
                    unmodifiedReport.Tester = report.Tester;
                }
            }
        }
        public async Task<IEnumerable<WaterProofingTestSample>> LoadReportAsync()
        {
            try
            {
                var result = await _context.WaterProofingTestSamples.ToListAsync();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ServiceResponse> ClearReportAsync()
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("DELETE FROM [WaterProofingTestSamples]");
                return ServiceResponse.Successful();
            }
            catch
            {
                Error error = new Error("Database.DELETE", "Lỗi db");
                return ServiceResponse.Failed(error);
            }
        }
        public async Task<ServiceResponse> ClearPreReportAsync (PreReportWaterProofing preReportWaterProofing = null)
        {
            try
            {
                if ( preReportWaterProofing==null )
                {
                    await _context.Database.ExecuteSqlRawAsync("DELETE FROM [PreReportWaterProofings]");
                }
                else
                {
                     _context.PreReportWaterProofings.Remove(preReportWaterProofing);
                }
                
                return ServiceResponse.Successful( );
            }
            catch
            {
                Error error = new Error("Database.DELETE","Lỗi db");
                return ServiceResponse.Failed(error);
            }
        }

        public async Task InsertPreReportAsync (PreReportWaterProofing entry)
        {
            await _context.PreReportWaterProofings.AddAsync(entry);
        }

        public async Task<IList<PreReportWaterProofing>> LoadPreReport ( )
        {
          return await _context.PreReportWaterProofings.ToListAsync( );
        }
    }
}