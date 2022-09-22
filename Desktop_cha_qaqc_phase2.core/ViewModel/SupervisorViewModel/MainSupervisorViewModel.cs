using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel
{
    public class MainSupervisorViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public BaseViewModels.BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand ReliabilityCommand { get; set; }
        public ICommand EnduranceCommand { get; set; }
        public ICommand DeformationCommand { get; set; }
        public ICommand WaterProofingCommand { get; set; }
        public bool IsSoftCloseSelected { get; set; } = true;
        public bool IsForcedCloseSelected { get; set; }
        public bool IsEnduranceSelected { get; set; }
        public bool IsWaterProofingSelected { get; set; }
        public MainSupervisorViewModel(NavigationStore navigationStore,
           INavigationService _ReliabilitynavigationService,
           INavigationService _EndurancenavigationService,
           INavigationService _DeformationnavigationService,
           INavigationService _WarterProofingnavigationService)
        {
            _navigationStore = navigationStore;
            ReliabilityCommand = new NavigateCommand(_ReliabilitynavigationService);
            EnduranceCommand = new NavigateCommand(_EndurancenavigationService);
            DeformationCommand = new NavigateCommand(_DeformationnavigationService);
            WaterProofingCommand = new NavigateCommand(_WarterProofingnavigationService);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        private void OnCurrentViewModelChanged()
        {
            IsSoftCloseSelected = false;
            IsForcedCloseSelected = false;
            IsEnduranceSelected = false;
            IsWaterProofingSelected = false;
            if (CurrentViewModel is SoftCloseSupervisorViewModel) IsSoftCloseSelected = true;
            if (CurrentViewModel is ForcedCloseSupervisorViewModel) IsForcedCloseSelected = true;
            if (CurrentViewModel is EnduranceSupervisorViewModel) IsEnduranceSelected = true;
            if (CurrentViewModel is WaterProofingSupervisorViewModel) IsWaterProofingSelected = true;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
