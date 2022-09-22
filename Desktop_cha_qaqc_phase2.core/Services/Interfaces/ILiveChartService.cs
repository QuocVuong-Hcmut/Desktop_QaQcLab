using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Services.Interfaces
{
    public interface ILiveChartService
    {
        public ObservableCollection<string> Labels { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public Func<double, string> YFormatter { get; set; }
    }
}
