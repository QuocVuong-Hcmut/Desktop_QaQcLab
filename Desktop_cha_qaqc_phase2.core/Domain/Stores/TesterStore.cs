using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Stores
{
    public class TesterStore
    {
        private ObservableCollection<Tester> _listTesters = new ObservableCollection<Tester>();
        private ObservableCollection<string> _listTesterIds = new ObservableCollection<string>();
        private readonly IApiService _apiService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<Tester> ListTesters
        {
            get => _listTesters;
            set
            {
                _listTesters = value;
            }
        }
        public ObservableCollection<string> ListTesterIds
        {
            get => _listTesterIds;
            set
            {
                _listTesterIds = value;
            }
        }
        public TesterStore(IApiService apiService, IDialogService dialogService)
        {
            _apiService = apiService;
            _dialogService = dialogService;
            Load();
        }

        private async Task Load()
        {
            //Test 
            //_listTesters.Add(new Tester() { testerID = "tdtu", firstName = "Tú", lastName = "Trần Đức" });
            //_listTesters.Add(new Tester() { testerID = "lctin", firstName = "Tín", lastName = "Lê Công" });
            //_listTesters.Add(new Tester() { testerID = "nvtai", firstName = "Tài", lastName = "Nguyễn Văn" });
            //_listTesterIds.Clear();
            //foreach (var tester in _listTesters)
            //{
            //    _listTesterIds.Add(tester.testerID);
            //};
            try
            {
                var result = await _apiService.GetTester( );
                if ( result.Resource!=null )
                {

                    _listTesters=result.Resource;
                    foreach ( var tester in _listTesters )
                    {
                        _listTesterIds.Add(tester.lastName+" "+tester.firstName);
                    }
                }
                else
                {
                    _listTesters=default;
                    _listTesterIds=default;
                }

            }
            catch ( Exception ex )
            {
                _dialogService.ShowDialog("Tester.Get",2);
            }
        }
    }
}
