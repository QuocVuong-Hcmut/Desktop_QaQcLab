using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using Desktop_cha_qaqc_phase2.Core.ViewModel.HelpViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.HistoryViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SettingViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.WarningViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.core.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public int SelectButton { get; set; }

        private readonly NavigationStore _navigationStore;
        private readonly IDialogService _dialogService;
        public IDialogService DialogService { get { return _dialogService; } }
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand LoggingCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        public ICommand ReportCommand { get; set; }
        public ICommand SupervisorCommand { get; set; }
        public ICommand WarningCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        public ICommand HelpCommand { get; set; }
        public bool isEnable { get; set; } = true;
        public bool isLoginSelected { get; private set; }
        public bool isSupervisorSelected { get; private set; }
        public bool isSettingSelected { get; private set; }
        public bool isReportSelected { get; private set; }
        public bool isHistorySelected { get; private set; }
        public bool isWarningSelected { get; private set; }
        public bool isHelpSelected { get; private set; }
        public MainViewModel(
            NavigationStore navigationStore,
            INavigationService _LogingnavigationService,
            INavigationService _SettingnavigationService,
            INavigationService _SupervisornavigationService,
            INavigationService _ReportnavigationService,
            INavigationService _WarningavigationService,
            INavigationService _HistorynavigationService,
            INavigationService _HelpnavigationService,
            IDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationStore = navigationStore;
            LoggingCommand = new NavigateCommand(_LogingnavigationService);
            SettingCommand = new NavigateCommand(_SettingnavigationService);
            ReportCommand = new NavigateCommand(_ReportnavigationService);
            SupervisorCommand = new NavigateCommand(_SupervisornavigationService);
            WarningCommand = new NavigateCommand(_WarningavigationService);
            HistoryCommand = new NavigateCommand(_HistorynavigationService);
            HelpCommand = new NavigateCommand(_HelpnavigationService);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            //
            isLoginSelected = true;
        }
        private void OnCurrentViewModelChanged()
        {
            isLoginSelected = false;
            isSettingSelected= false;
            isSupervisorSelected = false;
            isReportSelected = false;
            isHistorySelected = false;
            isWarningSelected = false;
            isHelpSelected = false;
            if (CurrentViewModel is LoginViewModel) isLoginSelected = true;
            if (CurrentViewModel is MainSettingsViewModel) isSettingSelected = true;
            if (CurrentViewModel is MainSupervisorViewModel) isSupervisorSelected = true;
            if (CurrentViewModel is MainReportViewModel) isReportSelected = true;
            if (CurrentViewModel is MainHistoryViewModel) isHistorySelected = true;
            if (CurrentViewModel is MainWarningViewModel) isWarningSelected = true;
            if (CurrentViewModel is MainHelpViewModel) isHelpSelected = true;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
