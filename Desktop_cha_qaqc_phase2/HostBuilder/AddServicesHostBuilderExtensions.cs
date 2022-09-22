using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Neleus.DependencyInjection.Extensions;
using System;
using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;

namespace Desktop_cha_qaqc_phase2.HostBuilder
{
    public static class AddServicesHostBuilderExtensions
    {
       
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>

            {
                
                #region SignalRService
                services.AddSingleton<ISignalRService, SignalRService>();
                #endregion
                #region LOGO_PLCService
                services.AddSingleton<ILogoSoftCloseService,LogoSoftClose>((s) =>
                {
                    return new LogoSoftClose("10.84.60.19", 0);
                });
                services.AddSingleton<ILogoForceCloseService,LogoForcedClose>((s) =>
                {
                    return new LogoForcedClose("10.84.60.21", 0);
                });
                services.AddSingleton<ILogoSoftCloseMachineService, LogoSoftCloseMachineService>();
                services.AddSingleton<ILogoForceCloseMachineService, LogoForcedCloseMachineService>();
                services.AddSingleton<S71200Endurance>((IServiceProvider serviceProvider) =>
                {
                    return new S71200Endurance(0, "10.84.60.17", 52);
                });
                services.AddSingleton<S71200WaterProofing>((IServiceProvider serviceProvider) =>
                {
                    return new S71200WaterProofing(0, "10.84.60.23", 17);
                });
                services.AddSingleton<ControlPlcService>();
                services.AddSingleton< S71200EnduranceMachineService>();
                services.AddSingleton< S71200WaterProofingMachineService>();
                #endregion
                #region DatabaseService
                services.AddTransient<IDatabaseService, DatabaseService>();
                services.AddSingleton<IUnitOfWork, UnitOfWork>();
                services.AddSingleton<ICurlingForceReportRepository, CurlingForceReportRepository>();
                services.AddSingleton<IHistoryForcedCloseReportRepository, HistoryForcedCloseReportRepository>();
                services.AddSingleton<IForcedCloseConfigurationepository, ForcedCloseConfigurationRepository>();
                services.AddSingleton<IForcedCloseReportRepository, ForcedCloseReportRepository>();
                services.AddSingleton<IEnduranceSettingParameterRepository, EnduranceSettingParameterRepository>();
                services.AddSingleton<IHistoryEnduranceReportRepository, HistoryEnduranceReportRepository>();
                services.AddSingleton<IHistorySoftCloseReportHistoryRepository, HistorySoftCloseReportRepository>();
                services.AddSingleton<ISoftCloseConfigurationRepository, SoftCloseConfigurationRepository>();
                services.AddSingleton<ISoftCloseReportRepository, SoftCloseReportRepository>();
                services.AddSingleton<IRockTestReportRepository, RockTestReportRepository>();
                services.AddSingleton<IStaticLoadReportRepository, StaticLoadReportRepository>();
                #endregion
                #region ExcelService
                services.AddScoped<IExcelExportService, ExcelExportService>();
                #endregion
                #region RegularExpressionService
                //services.AddSingleton<IRegularExpressionService,RegularExpressionService>();
                #endregion
                #region ChartService
                services.AddTransient<ILiveChartService, LiveChartService>();
                #endregion
                #region DialogService
                services.AddScoped<IDialogService, DialogService>();
                #endregion
                #region apiservice
                services.AddSingleton<IApiService, ApiService>();
                #endregion
            });
            return host;
        }
    }
}
