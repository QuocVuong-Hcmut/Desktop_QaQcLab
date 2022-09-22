
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;


namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class SoftCloseConfigurationRepository : ISoftCloseConfigurationRepository
    {
        private readonly ApplicationDbContext _context;

        public SoftCloseConfigurationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(SoftCloseTest entry)
        {
            await _context.SoftCloseTests.AddAsync(entry);
        }     
        public async Task ClearAsync()
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("DELETE FROM [SoftCloseTests]");
            }
            catch
            {
            }
        }
        public async Task<SoftCloseTest> LoadConfigurationsAsync()
        {
            try
            {

                return await _context.SoftCloseTests.FirstOrDefaultAsync();
            }
            catch
            {
                return null;

            }
        }
        public async Task UpdateConfiguration(SoftCloseTest config)
        {
            var pre = await (from p in _context.SoftCloseTests
                       where p.Id == config.Id
                       select p).FirstOrDefaultAsync();
            if (pre != null)
            {
                pre.StartDate = config.StartDate;
                pre.EndDate = config.EndDate;
                pre.Note = config.Note;
                pre.TestPurpose = config.TestPurpose;
                pre.ProductName = config.ProductName;
            }
        }
    }
}
