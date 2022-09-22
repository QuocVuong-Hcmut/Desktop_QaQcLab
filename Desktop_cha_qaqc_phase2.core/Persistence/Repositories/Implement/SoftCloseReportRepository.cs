
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class SoftCloseReportRepository : ISoftCloseReportRepository
    {
        private readonly ApplicationDbContext _context;
        public SoftCloseReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task InsertAsync(SoftCloseTestSample sheet)
        {
            await _context.SoftCloseTestSamples.AddAsync(sheet);
        }
        public async Task<IEnumerable<SoftCloseTestSample>> LoadAsync()
        {
            try
            {
                return await _context.SoftCloseTestSamples.ToListAsync();
            }
            catch
            {
                return null;

            }
        }
        public async Task ClearAsync()
        {
            try
            {
                _context.Database.ExecuteSqlRawAsync("DELETE FROM [SoftCloseTestSamples]");
            }
            catch
            {
            }
        }
        public async Task RemoveByNumClosing(List<int> listsamples)
        {
            try
            {
                foreach (int item in listsamples)
                {
                    var Sheets = _context.SoftCloseTestSamples.Where(p => p.NumberOfClosing == item).ToList();
                    if (Sheets.Count == 0) continue;
                    foreach (var sheet in Sheets)
                    {
                        _context.Remove(sheet);
                    }
                }
            }
            finally
            {
            }
        }
        public async Task<IList<SoftCloseTestSample>> GetAllByNumClosing(List<int> listSamples)
        {
            IList<SoftCloseTestSample> temp = new List<SoftCloseTestSample>();
            foreach (int item in listSamples)
            {
                var testSheets = _context.SoftCloseTestSamples.Where(p => p.NumberOfClosing == item).FirstOrDefault();
                temp.Add(testSheets);
            }
            return temp;
        }
        public async Task UpdateByNumClosing(IList<SoftCloseTestSample> listsamples)
        {
            foreach (var sample in listsamples)
            {
                var unmodifiedSample = (from sheet in _context.SoftCloseTestSamples
                                        where sheet.NumberOfClosing == sample.NumberOfClosing
                                        select sheet).FirstOrDefault();
                if (unmodifiedSample != null)
                {

                    unmodifiedSample.IsBumperLidIntact = sample.IsBumperLidIntact;
                    unmodifiedSample.IsBumperLidUnleaked = sample.IsBumperLidUnleaked;
                    unmodifiedSample.IsBumperRingIntact = sample.IsBumperRingIntact;
                    unmodifiedSample.IsBumperRingUnleaked = sample.IsBumperRingUnleaked;
                    unmodifiedSample.IsLidPassed = sample.IsBumperLidUnleaked;
                    unmodifiedSample.IsRingPassed = sample.IsBumperLidUnleaked;
                    unmodifiedSample.Note = sample.Note;
                    unmodifiedSample.NumberOfError = sample.NumberOfError;
                    unmodifiedSample.Tester = sample.Tester;
                }
            }
        }
    }
}
