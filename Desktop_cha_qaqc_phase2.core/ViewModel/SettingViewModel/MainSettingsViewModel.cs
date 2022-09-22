using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
//using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Desktop_cha_qaqc_phase2.Core.Commands;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.SettingViewModel
{
    public class MainSettingsViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
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
        public MainSettingsViewModel(NavigationStore navigationStore,
           INavigationService _ReliabilityNavigationService,
           INavigationService _EnduranceNavigationService,
           INavigationService _DeformationNavigationService,
           INavigationService _WaterProofingNavigationService)
        {
            _navigationStore = navigationStore;
            ReliabilityCommand = new NavigateCommand(_ReliabilityNavigationService);
            EnduranceCommand = new NavigateCommand(_EnduranceNavigationService);
            DeformationCommand = new NavigateCommand(_DeformationNavigationService);
            WaterProofingCommand = new NavigateCommand(_WaterProofingNavigationService);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        private void OnCurrentViewModelChanged()
        {
            IsSoftCloseSelected = false;
            IsForcedCloseSelected = false;
            IsEnduranceSelected = false;
            IsWaterProofingSelected = false;
            if(CurrentViewModel is SoftCloseSettingsViewModel) IsSoftCloseSelected = true;
            if(CurrentViewModel is ForcedCloseSettingsViewModel) IsForcedCloseSelected = true;
            if(CurrentViewModel is EnduranceSettingsViewModel) IsEnduranceSelected = true;
            if(CurrentViewModel is WaterProofingSettingsViewModel) IsWaterProofingSelected = true;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
