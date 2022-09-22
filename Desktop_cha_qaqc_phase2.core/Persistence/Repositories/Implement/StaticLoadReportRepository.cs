using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using ProductVertificationDesktopApp.Core.Domain;
using System.Collections.ObjectModel;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class StaticLoadReportRepository : IStaticLoadReportRepository
    {
        private readonly ApplicationDbContext _context;
        public StaticLoadReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async void UpdateAsync (PreReportEndurance preReportEndurance)
        {

            _context.PreReportEndurances.Update(preReportEndurance);
            await _context.SaveChangesAsync( );
        }
        public async Task InsertAsync(StaticLoadTestSample entry)
        {
           await  _context.StaticLoadTestSamples.AddAsync(entry);
        }
        public async Task<IList<StaticLoadTestSample>> LoadAsync()
        {
            try
            {
                return await _context.StaticLoadTestSamples.ToListAsync();
            }
            catch
            {
                return null;

            }
        }
        public async Task UpdateAsync(IEnumerable<StaticLoadTestSample> sheets)
        {
            try
            {
                foreach (var sheet in sheets)
                {
                    var unmodifiedSheet = await (from p in _context.StaticLoadTestSamples
                                                 where p.Id == sheet.Id
                                                 select p).FirstOrDefaultAsync();
                    if (unmodifiedSheet != null)
                    {

                        unmodifiedSheet.Note = sheet.Note;
                        unmodifiedSheet.NumberOfError = sheet.NumberOfError;
                        unmodifiedSheet.Status = sheet.Status;
                        unmodifiedSheet.Tester = sheet.Tester;
                    }
                }

            }
            catch
            {
                //Error error = new Error("Lỗi db");
                //return ServiceResponse.Failed();

            }
        }
        public async Task ClearAsync(PreReportEndurance preReportEndurance =null)
        {
            try
            {
                if(preReportEndurance !=null )
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

    }
}
