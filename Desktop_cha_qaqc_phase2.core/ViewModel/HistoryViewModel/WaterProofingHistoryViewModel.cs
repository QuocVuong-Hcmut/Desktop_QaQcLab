using AutoMapper;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Dialog;
using Desktop_cha_qaqc_phase2.Core.Domain.Models;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.HistoryViewModel
{
    public class WaterProofingHistoryViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly SoftCloseHistoryViewModel _historyViewModel;
        private readonly IDatabaseService _databaseService;
        private readonly IDialogService _dialogService;
        private readonly IApiService _apiService;
        private ObservableCollection<ListEvent> listEvents = new ObservableCollection<ListEvent>();

        public ObservableCollection<ListEvent> ListEvents
        {
            get { return listEvents; }
            set { listEvents = value; }
        }
        public int Target
        {
            get;
            set;
        }
        public ICommand ImportDataCommand { get; set; }
        public DateTime TimeStampStart { get; set; } = DateTime.Now;
        public DateTime TimeStampFinish { get; set; } = DateTime.Now;

        public WaterProofingHistoryViewModel(IDatabaseService databaseService, IApiService apiService,IDialogService dialogService)
        {
            _databaseService = databaseService;
            
            _dialogService = dialogService;
            _apiService = apiService;
            ImportDataCommand = new RelayCommand(ImportData);

        }
        private async void ImportData()
        {
            if (Target == 1)
            {
                // Tên cột trong bảng 
                var _timestart = TimeStampStart.AddDays(-1);
                var _timestop = TimeStampFinish.AddDays(+1);
                var result = await _apiService.GetWarning(_timestart, _timestop, "waterproofing");

                if (result.Success == true)
                {
                    ListEvents.Clear();
                    foreach (var items in result.Resource.Items)
                    {
                        var Time = items.Timestamp;
                        var Event = items.Message ;
                        ListEvent data = new ListEvent(Time, Event);
                        ListEvents.Add(data);
                    }
                    _dialogService.ShowDialog("Truy xuất thành công", 1);
                }
                else
                {
                    if (result.Error.Message != null)
                    {
                        _dialogService.ShowDialog("Không có dữ liệu trong thời gian này", 2);

                    }
                }

            }
            else
            {
                try
                {
                    var _start = TimeStampStart.AddHours(-8);
                    var _stop = TimeStampFinish.AddHours(-8);
                    var result1 = await _databaseService.LoadHistoryReliability(_start, _stop);
                    ListEvents.Clear();
                    foreach (var items in result1)
                    {
                        ListEvent data = new ListEvent(items.Timestamp, items.NameTest);
                        ListEvents.Add(data);

                    }
                    _dialogService.ShowDialog("Truy xuất thành công", 1);

                }
                catch
                {
                    _dialogService.ShowDialog("Không có dữ liệu trong thời gian này", 2);

                }
            }
        }
    }
}