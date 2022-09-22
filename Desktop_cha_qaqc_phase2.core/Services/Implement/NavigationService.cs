using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.core.ViewModel;
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

namespace Desktop_cha_qaqc_phase2.core.Services.Implement
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : BaseViewModel
    {

        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
