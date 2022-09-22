using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.WarningViewModel
{
    public class MainWarningViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly NavigationStore _navigationStore;

        public Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand ReliabilityCommand { get; set; }
        public ICommand EnduranceCommand { get; set; }
        public ICommand DeformationCommand { get; set; }
        public ICommand WaterProofingCommand { get; set; }
        public bool IsSoftCloseSelected { get; set; } = true;
        public bool IsForcedCloseSelected { get; set; }
        public bool IsEnduranceSelected { get; set; }
        public bool IsWaterProofingSelected { get; set; }
        
       public MainWarningViewModel(
           NavigationStore navigationStore,
           INavigationService _ReliabilitynavigationService,
           INavigationService _EndurancenavigationService,
           INavigationService _DeformationnavigationService,
           INavigationService _WaterProofingnavigationService)
        {
            _navigationStore = navigationStore;
            ReliabilityCommand = new NavigateCommand(_ReliabilitynavigationService);
            EnduranceCommand = new NavigateCommand(_EndurancenavigationService);
            DeformationCommand = new NavigateCommand(_DeformationnavigationService);
            WaterProofingCommand = new NavigateCommand(_WaterProofingnavigationService);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

        }
        private void OnCurrentViewModelChanged()
        {
            IsSoftCloseSelected = false;
            IsForcedCloseSelected = false;
            IsEnduranceSelected = false;
            IsWaterProofingSelected = false;
            if (CurrentViewModel is SoftCloseWarningViewModel) IsSoftCloseSelected = true;
            if (CurrentViewModel is ForcedCloseWarningViewModel) IsForcedCloseSelected = true;
            if (CurrentViewModel is EnduranceWarningViewModel) IsEnduranceSelected = true;
            if (CurrentViewModel is WaterProofingWarningViewModel) IsWaterProofingSelected = true;
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }

}
