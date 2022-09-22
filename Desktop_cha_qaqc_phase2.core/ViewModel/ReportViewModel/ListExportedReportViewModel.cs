using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel
{
    public class ListExportedReportViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        public bool IsOpen { get; set; } = false;
        public ObservableCollection<Test> ListExportedReport { get; set; } = new ObservableCollection<Test>();
        private object _selectedReport;
        public Object SelectedReport
        { 
            get => _selectedReport;
            set
            {
                _selectedReport = value;
                SelectiedReportChange?.Invoke(value);
            } 
        }  
        public ICommand ConfirmCommand { get; set; }
        public event Action<Object> SelectiedReportChange;
        public ListExportedReportViewModel()
        {
            ConfirmCommand = new RelayCommand(() => { IsOpen = false; });
        }
    }
}
