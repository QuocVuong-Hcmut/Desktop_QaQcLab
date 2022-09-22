using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel
{
    public class ConfirmSettingViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        public bool IsOpen { get; set; } = false;
        public string Title { get; set; } = "Bạn có xác nhận thao tác ?";
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public event EventHandler ConfirmAction;
        public event EventHandler CancelAction;
        public ConfirmSettingViewModel()
        {
            ConfirmCommand = new RelayCommand(() => { ConfirmAction?.Invoke(this, new EventArgs()); IsOpen = false; });

            CancelCommand = new RelayCommand(() => { CancelAction?.Invoke(this, new EventArgs()); IsOpen = false; });

        }
    }
}
    