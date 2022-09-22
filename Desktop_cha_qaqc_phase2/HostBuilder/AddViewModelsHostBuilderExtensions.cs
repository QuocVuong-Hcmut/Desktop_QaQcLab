using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;

using Desktop_cha_qaqc_phase2.Core.ViewModel;
using Desktop_cha_qaqc_phase2.ValueConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop_cha_qaqc_phase2.Core.Services;
using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SettingViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel.EnduranceReportViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.WarningViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.HelpViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.HistoryViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.core.ViewModel;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;

namespace Desktop_cha_qaqc_phase2.HostBuilder
{

    public static class AddViewModelsHostBuilderExtensions
    {

        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                #region Login
                services.AddSingleton<LoginViewModel>();
                #endregion
                #region Setting
                services.AddSingleton<EnduranceSettingsViewModel>();
                services.AddSingleton<SoftCloseSettingsViewModel>();
                services.AddSingleton<ForcedCloseSettingsViewModel>();
                services.AddSingleton<WaterProofingSettingsViewModel>();
                services.AddSingleton<MainSettingsViewModel>((IServiceProvider serviceprovider) =>
                {
                    var settingstore = serviceprovider.GetRequiredService<NavigationStore>();
                    settingstore.CurrentViewModel = serviceprovider.GetRequiredService<SoftCloseSettingsViewModel>();
                    return new MainSettingsViewModel(
                        settingstore,
                        CreateReliabilitySettingNavigationService(serviceprovider, settingstore),
                        CreateEnduranceSettingNavigationService(serviceprovider, settingstore),
                        CreateDeformationSettingNavigationService(serviceprovider, settingstore),
                        CreateWaterProofingSettingNavigationService(serviceprovider, settingstore)
                        );
                });
                #endregion
                #region Supervisor
                services.AddSingleton<WaterProofingSupervisorViewModel>();
                services.AddSingleton<EnduranceParaViewModel>((IServiceProvider ServiceProvider) =>
                {
                    var s71200model = ServiceProvider.GetRequiredService<S71200EnduranceMachineService>();
                    return new EnduranceParaViewModel(s71200model);
                });
                services.AddSingleton<EnduranceMonitorViewModel>((IServiceProvider ServiceProvider) =>
                {
                    var s71200model = ServiceProvider.GetRequiredService<S71200EnduranceMachineService>();
                    return new EnduranceMonitorViewModel(s71200model);
                });
                services.AddSingleton<EnduranceSupervisorViewModel>((IServiceProvider serviceprovider) =>
                {
                    var endurancestore = serviceprovider.GetRequiredService<NavigationStore>();
                    endurancestore.CurrentViewModel = serviceprovider.GetRequiredService<EnduranceMonitorViewModel>();
                    var s71200ModellingMachineService = serviceprovider.GetRequiredService<S71200EnduranceMachineService>();
                    var database = serviceprovider.GetRequiredService<IDatabaseService>( );
                    var signalR = serviceprovider.GetRequiredService<ISignalRService>();

                    var confirmSettingVM = serviceprovider.GetRequiredService<ConfirmSettingViewModel>();
                    return new EnduranceSupervisorViewModel(
                        endurancestore,
                        CreateEnduranceParaNavigationService(serviceprovider, endurancestore),
                        CreateEnduranceMonitorNavigationService(serviceprovider, endurancestore),
                        s71200ModellingMachineService,
                        confirmSettingVM,signalR,database);
                });
                services.AddSingleton<SoftCloseSupervisorViewModel>();
                services.AddSingleton<ForcedCloseSupervisorViewModel>();
                services.AddSingleton<MainSupervisorViewModel>((IServiceProvider serviceprovider) =>
                {
                    //var signalR1 = serviceprovider.GetRequiredService<ISignalRService>();
                    //signalR1.SetDependency(1);
                    //var signalR2 = serviceprovider.GetRequiredService<ISignalRService>();
                    //signalR2.SetDependency(2);
                    var supervisorstore = serviceprovider.GetRequiredService<NavigationStore>();
                    supervisorstore.CurrentViewModel = serviceprovider.GetRequiredService<SoftCloseSupervisorViewModel>();
                    return new MainSupervisorViewModel(/*signalR1,signalR2,*/
                        supervisorstore,
                        CreateReliabilitySupervisorNavigationService(serviceprovider, supervisorstore),
                        CreateEnduranceSupervisorNavigationService(serviceprovider, supervisorstore),
                        CreateDeformationSupervisorNavigationService(serviceprovider, supervisorstore),
                        CreateWaterProofingSupervisorNavigationService(serviceprovider, supervisorstore)
                        );
                });
                #endregion
                #region Report
                //services.AddSingleton<EnduranceReportViewModel>();
                services.AddSingleton<WaterProofingReportViewModel>();
                services.AddSingleton<SoftCloseReportViewModel>();
                services.AddSingleton<EnduranceStaticLoadTestReportViewModel>();
                services.AddSingleton<EnduranceCurlingForceTestReportViewModel>();
                services.AddSingleton<EnduranceRockTestReportViewModel>();
                services.AddSingleton<ForcedCloseReportViewModel>();
                services.AddSingleton<DataConfigDialogViewModel>();
                services.AddTransient<ListExportedReportViewModel>();
                services.AddSingleton<MainReportViewModel>((IServiceProvider serviceprovider) =>
                {
                    //var signalR1 = serviceprovider.GetRequiredService<ISignalRService>();
                    //signalR1.SetDependency(1);
                    //var signalR2 = serviceprovider.GetRequiredService<ISignalRService>();
                    //signalR2.SetDependency(2);
                    var reportstore = serviceprovider.GetRequiredService<NavigationStore>();
                    reportstore.CurrentViewModel = serviceprovider.GetRequiredService<SoftCloseReportViewModel>();
                    return new MainReportViewModel(/*signalR1,signalR2,*/
                        reportstore,
                        CreateReliabilityReportNavigationService(serviceprovider, reportstore),
                        CreateDeformationReportNavigationService(serviceprovider, reportstore),
                        CreateEnduranceStaticLoadTestNavigationService(serviceprovider, reportstore),
                        CreateEnduranceCurlingForceNavigationService(serviceprovider, reportstore),
                        CreateEnduranceRockTestNavigationService(serviceprovider, reportstore),
                        CreateWaterProofingNavigationService(serviceprovider, reportstore)
                        );
                });
                #endregion
                #region Warning
                services.AddSingleton<EnduranceWarningViewModel>();
                services.AddSingleton<SoftCloseWarningViewModel>();
                services.AddSingleton<ForcedCloseWarningViewModel>();
                services.AddSingleton<WaterProofingWarningViewModel>();
                services.AddSingleton<MainWarningViewModel>((IServiceProvider serviceprovider) =>
                {
                    //var signalR1 = serviceprovider.GetRequiredService<ISignalRService>();
                    //signalR1.SetDependency(1);
                    //var signalR2 = serviceprovider.GetRequiredService<ISignalRService>();
                    //signalR2.SetDependency(2);
                    var warningstore = serviceprovider.GetRequiredService<NavigationStore>();
                    warningstore.CurrentViewModel = serviceprovider.GetRequiredService<SoftCloseWarningViewModel>();

                    return new MainWarningViewModel(/*signalR1,signalR2,*/
                        warningstore,
                        CreateReliabilityWarningNavigationService(serviceprovider, warningstore),
                        CreateEnduranceWarningNavigationService(serviceprovider, warningstore),
                         CreateDeformationWarningNavigationService(serviceprovider, warningstore),
                         CreateWaterProofingWarningNavigationService(serviceprovider, warningstore)
                        );
                });
                #endregion
                #region History
                services.AddSingleton<EnduranceHistoryViewModel>();
                services.AddSingleton<SoftCloseHistoryViewModel>();
                services.AddSingleton<ForcedCloseHistoryViewModel>();
                services.AddSingleton<WaterProofingHistoryViewModel>();
                services.AddSingleton<MainHistoryViewModel>((IServiceProvider serviceprovider) =>
                {
                    //var signalR1 = serviceprovider.GetRequiredService<ISignalRService>();
                    //signalR1.SetDependency(1);
                    //var signalR2 = serviceprovider.GetRequiredService<ISignalRService>();
                    //signalR2.SetDependency(2);
                    var historystore = serviceprovider.GetRequiredService<NavigationStore>();
                    historystore.CurrentViewModel = serviceprovider.GetRequiredService<SoftCloseHistoryViewModel>();
                    return new MainHistoryViewModel(/*signalR1,signalR2,*/
                        historystore,
                        CreateReliabilityHistoryNavigationService(serviceprovider, historystore),
                        CreateEnduranceHistoryNavigationService(serviceprovider, historystore),
                         CreateDeformationHistoryNavigationService(serviceprovider, historystore),
                         CreateWaterProofingHistoryNavigationService(serviceprovider, historystore)
                        );
                });
                #endregion
                #region Help
                services.AddSingleton<MainHelpViewModel>();
                #endregion
                #region MainViewModel
                services.AddSingleton<MainViewModel>((IServiceProvider serviceprovider) =>
                {
                    //var signalR1 = serviceprovider.GetRequiredService<ISignalRService>();
                    //signalR1.SetDependency(1);
                    //var signalR2 = serviceprovider.GetRequiredService<ISignalRService>();
                    //signalR2.SetDependency(2);
                    var Mainstore = serviceprovider.GetRequiredService<NavigationStore>();
                    var dialogService = serviceprovider.GetRequiredService<IDialogService>();
                    Mainstore.CurrentViewModel = serviceprovider.GetRequiredService<LoginViewModel>();

                    return new MainViewModel(/*signalR1,signalR2,*/
                        Mainstore,
                        CreateLoginNavigationService(serviceprovider, Mainstore),
                        CreateSettingNavigationService(serviceprovider, Mainstore),
                        CreateSupervisorNavigationService(serviceprovider, Mainstore),
                        CreateReportNavigationService(serviceprovider, Mainstore),
                        CreateWarningNavigationService(serviceprovider, Mainstore),
                        CreateHistoryNavigationService(serviceprovider, Mainstore),
                        CreateHelpNavigationService(serviceprovider, Mainstore),
                        dialogService);
                });
                #endregion
                services.AddTransient<ConfirmSettingViewModel>();

            });
            return host;
        }


        //MainViewModel
        private static INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<LoginViewModel>(
                store,
                () => serviceProvider.GetRequiredService<LoginViewModel>());
        }
        private static INavigationService CreateSettingNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<MainSettingsViewModel>(
                store,
                () => serviceProvider.GetRequiredService<MainSettingsViewModel>());
        }
        private static INavigationService CreateSupervisorNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<MainSupervisorViewModel>(
                store,
                () => serviceProvider.GetRequiredService<MainSupervisorViewModel>());
        }
        private static INavigationService CreateReportNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<MainReportViewModel>(
                store,
                () => serviceProvider.GetRequiredService<MainReportViewModel>());
        }
        private static INavigationService CreateWarningNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<MainWarningViewModel>(
                store,
                () => serviceProvider.GetRequiredService<MainWarningViewModel>());
        }
        private static INavigationService CreateHistoryNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<MainHistoryViewModel>(
                store,
                () => serviceProvider.GetRequiredService<MainHistoryViewModel>());
        }
        private static INavigationService CreateHelpNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<MainHelpViewModel>(
                store,
                () => serviceProvider.GetRequiredService<MainHelpViewModel>());
        }
        //Setting
        private static INavigationService CreateDeformationSettingNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<ForcedCloseSettingsViewModel>(
               store,
               () => serviceProvider.GetRequiredService<ForcedCloseSettingsViewModel>());
        }

        private static INavigationService CreateReliabilitySettingNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<SoftCloseSettingsViewModel>(
               store,
               () => serviceProvider.GetRequiredService<SoftCloseSettingsViewModel>());
        }

        private static INavigationService CreateEnduranceSettingNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<EnduranceSettingsViewModel>(
               store,
               () => serviceProvider.GetRequiredService<EnduranceSettingsViewModel>());
        }

        private static INavigationService CreateWaterProofingSettingNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<WaterProofingSettingsViewModel>(
               store,
               () => serviceProvider.GetRequiredService<WaterProofingSettingsViewModel>());
        }

        //Supervisor
        private static INavigationService CreateDeformationSupervisorNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<ForcedCloseSupervisorViewModel>(
               store,
               () => serviceProvider.GetRequiredService<ForcedCloseSupervisorViewModel>());
        }
        private static INavigationService CreateReliabilitySupervisorNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<SoftCloseSupervisorViewModel>(
               store,
               () => serviceProvider.GetRequiredService<SoftCloseSupervisorViewModel>());
        }
        private static INavigationService CreateEnduranceSupervisorNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<EnduranceSupervisorViewModel>(
               store,
               () => serviceProvider.GetRequiredService<EnduranceSupervisorViewModel>());
        }
        private static INavigationService CreateEnduranceParaNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<EnduranceParaViewModel>(
               store,
               () => serviceProvider.GetRequiredService<EnduranceParaViewModel>());
        }
        private static INavigationService CreateEnduranceMonitorNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<EnduranceMonitorViewModel>(
               store,
               () => serviceProvider.GetRequiredService<EnduranceMonitorViewModel>());
        }
        private static INavigationService CreateWaterProofingSupervisorNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<WaterProofingSupervisorViewModel>(
            store,
            () => serviceProvider.GetRequiredService<WaterProofingSupervisorViewModel>());
        }

        //Report
        private static INavigationService CreateDeformationReportNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<ForcedCloseReportViewModel>(
               store,
               () => serviceProvider.GetRequiredService<ForcedCloseReportViewModel>());
        }

        private static INavigationService CreateReliabilityReportNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<SoftCloseReportViewModel>(
               store,
               () => serviceProvider.GetRequiredService<SoftCloseReportViewModel>());
        }
        private static INavigationService CreateEnduranceStaticLoadTestNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<EnduranceStaticLoadTestReportViewModel>(
               store,
               () => serviceProvider.GetRequiredService<EnduranceStaticLoadTestReportViewModel>());
        }
        private static INavigationService CreateEnduranceCurlingForceNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<EnduranceCurlingForceTestReportViewModel>(
               store,
               () => serviceProvider.GetRequiredService<EnduranceCurlingForceTestReportViewModel>());
        }
        private static INavigationService CreateEnduranceRockTestNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<EnduranceRockTestReportViewModel>(
               store,
               () => serviceProvider.GetRequiredService<EnduranceRockTestReportViewModel>());
        }
        private static INavigationService CreateWaterProofingNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<WaterProofingReportViewModel>(
               store,
               () => serviceProvider.GetRequiredService<WaterProofingReportViewModel>());
        }

        //Warning

        private static INavigationService CreateDeformationWarningNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<ForcedCloseWarningViewModel>(
               store,
               () => serviceProvider.GetRequiredService<ForcedCloseWarningViewModel>());
        }
        private static INavigationService CreateWaterProofingWarningNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<WaterProofingWarningViewModel>(
               store,
               () => serviceProvider.GetRequiredService<WaterProofingWarningViewModel>());
        }
        private static INavigationService CreateReliabilityWarningNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<SoftCloseWarningViewModel>(
               store,
               () => serviceProvider.GetRequiredService<SoftCloseWarningViewModel>());
        }
        private static INavigationService CreateEnduranceWarningNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<EnduranceWarningViewModel>(
               store,
               () => serviceProvider.GetRequiredService<EnduranceWarningViewModel>());
        }
        //History

        private static INavigationService CreateDeformationHistoryNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<ForcedCloseHistoryViewModel>(
               store,
               () => serviceProvider.GetRequiredService<ForcedCloseHistoryViewModel>());
        }
        private static INavigationService CreateWaterProofingHistoryNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<WaterProofingHistoryViewModel>(
               store,
               () => serviceProvider.GetRequiredService<WaterProofingHistoryViewModel>());
        }
        private static INavigationService CreateReliabilityHistoryNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<SoftCloseHistoryViewModel>(
               store,
               () => serviceProvider.GetRequiredService<SoftCloseHistoryViewModel>());
        }
        private static INavigationService CreateEnduranceHistoryNavigationService(IServiceProvider serviceProvider, NavigationStore store)
        {
            return new NavigationService<EnduranceHistoryViewModel>(
               store,
               () => serviceProvider.GetRequiredService<EnduranceHistoryViewModel>());
        }
    }
}
