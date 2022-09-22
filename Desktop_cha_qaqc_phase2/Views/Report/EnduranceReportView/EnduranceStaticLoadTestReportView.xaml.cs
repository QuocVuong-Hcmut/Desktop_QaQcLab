using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop_cha_qaqc_phase2.Views.Report.EnduranceReportView
{
    /// <summary>
    /// Interaction logic for EnduranceImpactTestReportView.xaml
    /// </summary>
    public partial class EnduranceStaticLoadTestReportView : UserControl
    {
        public EnduranceStaticLoadTestReportView()
        {
            InitializeComponent();
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var vm = (Core.ViewModel.ReportViewModel.EnduranceReportViewModel.EnduranceStaticLoadTestReportViewModel)DataContext;
            vm.UpdateDataCommand.Execute(null);
        }
    }
}
