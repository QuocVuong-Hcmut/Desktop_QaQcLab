using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Services.Implement
{
    public class LiveChartService : BaseViewModel,ILiveChartService
    {
        private string t1 = "Thời gian đóng êm của nắp";
        private string t2 = "Thời gian đóng êm của đế";
        private ObservableCollection<string> labels  = new ObservableCollection<string>();
        public ObservableCollection<string> Labels
        {
            get => labels;
            set
            {
                labels = value;
            }
        }
        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection 
        {
            get => seriesCollection; 
            set
            {
                seriesCollection = value;
                OnPropertyChanged();
            }
        }
        public Func<double, string> YFormatter { get; set ; }
        public LiveChartService()
        {
            SeriesCollection = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Thời gian đóng êm của đế",
                    Values = new ChartValues<double> {},
                    PointGeometrySize = 5,
                },
                new LineSeries
                {
                    Title = "Thời gian đóng êm của nắp",
                    Values = new ChartValues<double> {},
                    PointGeometrySize = 5
                }
            };
            YFormatter = val => val.ToString("f");
        }
        
    }
}
