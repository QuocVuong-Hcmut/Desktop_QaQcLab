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
    public class DataConfigDialogViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        public bool IsOpen { get; set; }
        private bool _mode;
        public bool Mode
        {
            get => _mode;
            set
            {
                _mode = value;

                if (_mode)
                {
                    NumOfSamples = 0;
                    if (ListSamples != null) ListSamples.Clear();
                    for (int i = 1; i <= 20; i++)
                    {

                        ListSamples.Add(new ListSample(i, i * 5000));
                    }
                }
                else
                {
                    ListSamples.Clear();
                }
            }
        }
        private int numOfSamples;
        public int NumOfSamples
        {
            get { return numOfSamples; }
            set
            {   //gioi han so lan lay mau
                if (value > 100) value = 100;
                if (value < 0) value = 0;
                numOfSamples = value;
                if (ListSamples != null)
                {
                    if (NumOfSamples > ListSamples.Count)
                    {
                        for (int i = ListSamples.Count; i < numOfSamples; i++)
                        {
                            ListSamples.Add(new ListSample(i + 1, 0));
                        }
                    }
                    else if (numOfSamples < ListSamples.Count)
                    {
                        for (int i = ListSamples.Count - 1; i > numOfSamples - 1; i--)
                        {
                            ListSamples.RemoveAt(i);
                            //_preSamples.Add(ListSamples[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < numOfSamples; i++)
                    {
                        ListSamples.Add(new ListSample(i, 0));
                    }
                }
            }

        }
        //private ObservableCollection<ListSample> _preSamples;

        private ObservableCollection<ListSample> _listSamples;
        public ObservableCollection<ListSample> ListSamples
        {
            get => _listSamples;
            set
            {
                _listSamples = value;
            }
        }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public event EventHandler SaveAction;
        public event EventHandler CancelAction;

        public DataConfigDialogViewModel()
        {
            SaveCommand = new RelayCommand(() => { SaveAction?.Invoke(this, new EventArgs()); IsOpen = false; });
            CancelCommand = new RelayCommand(() => { CancelAction?.Invoke(this, new EventArgs()); IsOpen = false; });
            _listSamples = new ObservableCollection<ListSample>();
        }
        public List<int> GetListSortedIndexes()
        {

            var ListSamples = this.ListSamples;
            List<int> SampleIndexes = new List<int> { };

            for (int i = 0; i < ListSamples.Count; i++)
            {
                SampleIndexes.Add(ListSamples[i].Sample);
            }
            SampleIndexes.Sort();
            return SampleIndexes;
        }
        public class ListSample
        {
            public int stt { get; set; }
            private int _sample;
            public int Sample
            {
                get => _sample;
                set
                {
                    if (_sample != value)
                    {
                        _sample = value;
                    }
                }
            }
            public ListSample(int stt, int sample)
            {
                this.stt = stt;
                this.Sample = sample;
            }
        }
    }
}
