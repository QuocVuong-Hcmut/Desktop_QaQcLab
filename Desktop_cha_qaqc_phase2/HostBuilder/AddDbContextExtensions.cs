using Desktop_cha_qaqc_phase2.Core.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.HostBuilder
{
    public static class AddDbContextExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddDbContextFactory<ApplicationDbContext>();
            });
            return host;
        }
    }
}
