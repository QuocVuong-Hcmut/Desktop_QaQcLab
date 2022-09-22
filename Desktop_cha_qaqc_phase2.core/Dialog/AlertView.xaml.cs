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
using System.Windows.Shapes;

namespace Desktop_cha_qaqc_phase2.Core.Dialog
{
    /// <summary>
    /// Interaction logic for AlertView.xaml
    /// </summary>
    public partial class AlertView : Window
    {
        public AlertView()
        {
            InitializeComponent();
        }
        public void Alert(string mes)
        {
            Message.Text = mes; 
            this.ShowDialog();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
