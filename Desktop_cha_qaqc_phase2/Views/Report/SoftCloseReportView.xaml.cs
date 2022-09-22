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

namespace Desktop_cha_qaqc_phase2.Views.Report
{
    /// <summary>
    /// Interaction logic for ReliabilityReportView.xaml
    /// </summary>
    public partial class SoftCloseReportView : UserControl
    {
        public SoftCloseReportView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedText = "";
            ComboBoxItem purposeCB = PurposeCombobox.SelectedItem as ComboBoxItem;
            if (!(purposeCB is null))
            {
                selectedText = purposeCB.Content.ToString();

            }

            if (selectedText == "Khác")
            {
                description.IsEnabled = true;
            }
            else
            {
                description.IsEnabled = false;
                description.Text = "";
            }

        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
           var vm = (Core.ViewModel.ReportViewModel.SoftCloseReportViewModel)DataContext;
            vm.UpdateDataCommand.Execute(null);
        }
    }
}
