
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
using ProductVertificationDesktopApp.Core.Domain;
using ProductVertificationDesktopApp.Core.Domain.Communication;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class CurlingForceConfigurationRepository : ICurlingForceConfigurationRepository
    {
        private readonly ApplicationDbContext _context;
        public CurlingForceConfigurationRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task ClearConfigurationAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM [CurlingForceTests]");
        }
        public Task InsertConfiguration(CurlingForceTest entry)
        {
            throw new NotImplementedException();
        }
        public async Task<CurlingForceTest> LoadConfiguration()
        {

            var configs = await _context.CurlingForceTests.ToArrayAsync();
            return configs[0];
        }
        public async Task UpdateConfiguration(CurlingForceTest config)
        {
            var pre = await (from p in _context.CurlingForceTests
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