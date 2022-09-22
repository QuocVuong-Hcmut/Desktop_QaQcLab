using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel.EnduranceReportViewModel;

using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel
{
    public class MainReportViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly INavigationService _staticLoadTestnavigationService;
        private readonly INavigationService _rockTestnavigationService;
        private readonly INavigationService _curlingForceTestnavigationService;
        private string _isPopupOpen = "False";
        public string IsPopupOpen
        {
            get =>
                _isPopupOpen;
            set
            {
                _isPopupOpen = value;
                OnPropertyChanged();
            }
        }
        public BaseViewModels.BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand ReliabilityCommand { get; set; }
        public ICommand CurlingForceCommand { get; set; }
        public ICommand StaticLoadCommand { get; set; }
        public ICommand RockTestCommand { get; set; }
        public ICommand DeformationCommand { get; set; }
        public ICommand EnduranceCommand { get; set; }
        public ICommand WaterProofingCommand { get; set; }
        public bool IsSoftCloseSelected { get; set; } = true;
        public bool IsForcedCloseSelected { get; set; }
        public bool IsEnduranceSelected { get; set; }
        public bool IsWaterProofingSelected { get; set; }
        public MainReportViewModel(
           NavigationStore navigationStore,
           INavigationService _ReliabilitynavigationService,
           INavigationService _DeformationnavigationService,
           INavigationService _StaticLoadTestnavigationService,
           INavigationService _CurlingForcenavigationService,
           INavigationService _RockTestnavigationService,
           INavigationService _WaterProofingnavigationService)
        {
            _navigationStore = navigationStore;
            _staticLoadTestnavigationService = _StaticLoadTestnavigationService;
            _rockTestnavigationService = _RockTestnavigationService;
            _curlingForceTestnavigationService = _CurlingForcenavigationService;
           
            ReliabilityCommand = new NavigateCommand(_ReliabilitynavigationService);
            DeformationCommand = new NavigateCommand(_DeformationnavigationService);
            WaterProofingCommand = new NavigateCommand(_WaterProofingnavigationService);
            CurlingForceCommand = new RelayCommand(BendingForceTestRelayCommand);
            StaticLoadCommand = new RelayCommand(ImpactTestRelayCommand);
            EnduranceCommand = new RelayCommand(OpenPopup);
            RockTestCommand = new RelayCommand(RockTestRelayCommand);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
       }


        public void OpenPopup()
        {
            IsPopupOpen = "True";
        }
        public void RockTestRelayCommand()
        {
            IsPopupOpen = "False";
            var command = new NavigateCommand(_rockTestnavigationService);
            command.Execute(null);
        }
        public void ImpactTestRelayCommand()
        {
            IsPopupOpen = "False";
            var command = new NavigateCommand(_staticLoadTestnavigationService);
            command.Execute(null);
        }
        public void BendingForceTestRelayCommand()
        {
            IsPopupOpen = "False";
            var command = new NavigateCommand(_curlingForceTestnavigationService);
            command.Execute(null);
        }
        private void OnCurrentViewModelChanged()
        {
            IsSoftCloseSelected = false;
            IsForcedCloseSelected = false;
            IsEnduranceSelected = false;
            IsWaterProofingSelected = false;
            if (CurrentViewModel is SoftCloseReportViewModel) IsSoftCloseSelected = true;
            if (CurrentViewModel is ForcedCloseReportViewModel) IsForcedCloseSelected = true;
            if (CurrentViewModel is EnduranceCurlingForceTestReportViewModel) IsEnduranceSelected = true;
            if (CurrentViewModel is EnduranceRockTestReportViewModel) IsEnduranceSelected = true;
            if (CurrentViewModel is EnduranceStaticLoadTestReportViewModel) IsEnduranceSelected = true;
            if (CurrentViewModel is WaterProofingReportViewModel) IsWaterProofingSelected = true;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
