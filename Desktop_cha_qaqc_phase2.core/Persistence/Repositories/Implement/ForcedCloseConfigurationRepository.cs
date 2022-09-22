
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class ForcedCloseConfigurationRepository : IForcedCloseConfigurationepository
    {
        private readonly ApplicationDbContext _context;
        public ForcedCloseConfigurationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task InsertAsync(ForcedCloseTest entry)
        {
            await _context.ForcedCloseTests.AddAsync(entry);
        }
        public async Task ClearAsync()
        {
            try
            {
                var list = await _context.ForcedCloseTests.ToListAsync();
                foreach (var item in list)
                {
                    _context.ForcedCloseTests.Remove(item);
                }

            }
            finally
            {
            }
        }
        public async Task<ForcedCloseTest> LoadConfigurationAsync()
        {

            try
            {

                return  await _context.ForcedCloseTests.FirstOrDefaultAsync();
               
            }
            finally
            {
            }
        }
        public async Task UpdateConfiguration(ForcedCloseTest config)
        {
            var pre = await (from p in _context.ForcedCloseTests
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
