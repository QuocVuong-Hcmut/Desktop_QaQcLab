using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neleus.DependencyInjection.Extensions;
using Desktop_cha_qaqc_phase2.core.Services;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop_cha_qaqc_phase2.Views;
using Desktop_cha_qaqc_phase2.Views.Report;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Views.Supervisor;

namespace Desktop_cha_qaqc_phase2.HostBuilder
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)     
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));       
            });

            return host;
        }
    }
}
