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
    /// Interaction logic for DeformatingReportView.xaml
    /// </summary>
    public partial class MainMenuReportView : UserControl
    {
        public MainMenuReportView()
        {
            InitializeComponent();
        }



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedText = "";
            ComboBoxItem  purposeCB = PurposeCombobox.SelectedItem as ComboBoxItem;
            if(!(purposeCB is null))
            {
                selectedText = purposeCB.Content.ToString();

            }

            if (selectedText == "Khác")
            {
                desctiption.IsEnabled = true;
            }
            else 
            { 
                desctiption.IsEnabled = false;
                desctiption.Text = "";
            }

        }
    }
}
