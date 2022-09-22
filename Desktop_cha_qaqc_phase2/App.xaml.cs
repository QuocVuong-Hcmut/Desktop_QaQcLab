using Desktop_cha_qaqc_phase2.Core.Domain.Stores;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel.EnduranceReportViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SettingViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.WarningViewModel;
using Desktop_cha_qaqc_phase2.HostBuilder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop_cha_qaqc_phase2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                        .AddServices()
                        .AddViewModels()
                        .AddDbContext()
                        .AddDataStore()
                        .AddViews();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            #region Control PLC 
            _host.Services.GetRequiredService(typeof(ControlPlcService));
            #endregion
            #region Report
            _host.Services.GetRequiredService(typeof(ForcedCloseReportViewModel));
            _host.Services.GetRequiredService(typeof(SoftCloseReportViewModel));
            _host.Services.GetRequiredService(typeof(WaterProofingReportViewModel));
            _host.Services.GetRequiredService(typeof(EnduranceCurlingForceTestReportViewModel));
            _host.Services.GetRequiredService(typeof(EnduranceRockTestReportViewModel));
            _host.Services.GetRequiredService(typeof(EnduranceStaticLoadTestReportViewModel));
            #endregion
            #region Setting
            _host.Services.GetRequiredService(typeof(SoftCloseSettingsViewModel));
            _host.Services.GetRequiredService(typeof(ForcedCloseSettingsViewModel));
            _host.Services.GetRequiredService(typeof(WaterProofingSettingsViewModel));
            _host.Services.GetRequiredService(typeof(EnduranceSettingsViewModel));
            #endregion
            #region Supervisor
            _host.Services.GetRequiredService(typeof(SoftCloseSupervisorViewModel));
            _host.Services.GetRequiredService(typeof(ForcedCloseSupervisorViewModel));
            _host.Services.GetRequiredService(typeof(EnduranceSupervisorViewModel));
            _host.Services.GetRequiredService(typeof(EnduranceMonitorViewModel));
            _host.Services.GetRequiredService(typeof(WaterProofingSupervisorViewModel));

            #endregion
            #region Warning 
            _host.Services.GetRequiredService(typeof(SoftCloseWarningViewModel));
            _host.Services.GetRequiredService(typeof(ForcedCloseWarningViewModel));
            _host.Services.GetRequiredService(typeof(EnduranceWarningViewModel));
            _host.Services.GetRequiredService(typeof(WaterProofingWarningViewModel));

            #endregion
            #region Store
            _host.Services.GetRequiredService<ProductStore>();
            _host.Services.GetRequiredService<TesterStore>();
            #endregion
            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();
            base.OnStartup(e);
        }
    
        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
