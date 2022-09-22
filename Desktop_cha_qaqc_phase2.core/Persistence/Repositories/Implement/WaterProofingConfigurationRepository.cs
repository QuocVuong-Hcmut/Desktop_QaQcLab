
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
    public class WaterProofingConfigurationRepository : IWaterProofingConfigurationRepository
    {
        private readonly ApplicationDbContext _context;
        public WaterProofingConfigurationRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task ClearConfigurationAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM [WaterProofingTests]");
        }
        public Task InsertConfiguration(WaterProofingTest entry)
        {
            throw new NotImplementedException();
        }
        public async Task<WaterProofingTest> LoadConfiguration()
        {
            return await _context.WaterProofingTests.FirstOrDefaultAsync();
        }
        public async Task UpdateConfiguration(WaterProofingTest config)
        {

            var pre = await (from p in _context.WaterProofingTests
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