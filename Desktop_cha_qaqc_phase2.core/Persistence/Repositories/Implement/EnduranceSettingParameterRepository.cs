
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using Microsoft.EntityFrameworkCore;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class EnduranceSettingParameterRepository : IEnduranceSettingParameterRepository
    {
        private readonly ApplicationDbContext _context;
        public EnduranceSettingParameterRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async void UpdateAsync(EnduranceSettingParameter entry)
        {
            try
            {
                var product = await _context.EnduranceSettingParameters.FindAsync(entry.Id);
                if (product != null && entry != null)
                {
                    product.Clone(entry);
                }
            }
            catch
            {
            }
        }
        public EnduranceSettingParameter Load(int ID)
        {
            var result = (from p in _context.EnduranceSettingParameters
                          where (p.Id == ID)
                          select p
                         )
                        .ToList();
            var item = result.FirstOrDefault();
            return item;
        }
        public async Task<ServiceResponse> ClearPreReportAsync ( )
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("DELETE FROM [PreReportEndurances]");
                return ServiceResponse.Successful( );
            }
            catch
            {
                Error error = new Error("Database.DELETE","Lỗi db");
                return ServiceResponse.Failed(error);
            }
        }

        public async Task InsertPreReportAsync (PreReportEndurance entry)
        {
            await _context.PreReportEndurances.AddAsync(entry);
        }

        public async Task<IList<PreReportEndurance>> LoadPreReport ( )
        {
            return await _context.PreReportEndurances.ToListAsync( );
        }
    }
}
