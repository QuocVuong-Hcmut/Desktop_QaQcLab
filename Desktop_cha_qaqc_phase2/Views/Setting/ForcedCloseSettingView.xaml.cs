using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SettingViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

namespace Desktop_cha_qaqc_phase2.Views.Setting
{
    /// <summary>
    /// Interaction logic for EnduranceSettingView.xaml
    /// </summary>
    public partial class ForcedCloseSettingView : UserControl
    {
        
        public ForcedCloseSettingView()
        {
            InitializeComponent();
            
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Window confirmsetting = new ConfirmSettingView();
        //    confirmsetting.DataContext = this.DataContext;
        //    confirmsetting.ShowDialog();

        //}
    }
}
