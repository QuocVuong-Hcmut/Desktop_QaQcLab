
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;
using System.Threading;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SaveChangeAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {}
            finally
            {}
        }
       
    }
}
