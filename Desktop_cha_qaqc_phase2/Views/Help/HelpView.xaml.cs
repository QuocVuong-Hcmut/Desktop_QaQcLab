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
using System.Windows.Threading;

namespace Desktop_cha_qaqc_phase2.Views.Help
{
    /// <summary>
    /// Interaction logic for HelpView.xaml
    /// </summary>
    public partial class HelpView : UserControl
    {
        DispatcherTimer TLoad = new DispatcherTimer( );
        public HelpView()
        {
            InitializeComponent();
            TLoad.Interval=TimeSpan.FromSeconds(1);
            TLoad.Tick+=TLoad_Tick;
            LBTime.Content=" / 00:03:14";
          //  pdf.Source=new Uri(@"C:\Users\Quoc Vuong\Downloads\Hướng Dẫn Vận Hành Ứng Dụng Máy Tính.docx"); 

        }
        private void TLoad_Tick (object? sender,EventArgs e)
        {
            Slider.Value=EVideo.Position.TotalSeconds;
            LBRealTime.Content=EVideo.Position;
        }

        private void BTPlay_Click (object sender,RoutedEventArgs e)
        {
            TLoad.Start( );
            LBRealTime.Content="00:00:00";
        }

        private void BTStop_Click (object sender,RoutedEventArgs e)
        {
            TLoad.Stop( );
            LBRealTime.Content="00:00:00";
        }

        private void BTPause_Click (object sender,RoutedEventArgs e)
        {
            TLoad.Stop( );

        }

        private void Button_Click (object sender,RoutedEventArgs e)
        {
            LBTime.Content=" / 00:05:10";
            Slider.Maximum=310;
        }

        private void Button_Click_1 (object sender,RoutedEventArgs e)
        {
            LBTime.Content=" / 00:04:45";
            Slider.Maximum=285;
        }

        private void Button_Click_2 (object sender,RoutedEventArgs e)
        {
            LBTime.Content=" / 00:02:47";
            Slider.Maximum=167;
        }

        private void Button_Click_3 (object sender,RoutedEventArgs e)
        {
            LBTime.Content=" / 00:03:26";
            Slider.Maximum=206;
        }
    }
}
