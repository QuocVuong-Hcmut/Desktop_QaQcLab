using AutoMapper;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Domain.Stores;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SettingViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel
{
    public class WaterProofingReportViewModel: Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly IDatabaseService _databaseService;
        private readonly IApiService _apiService;
        private readonly IExcelExportService _excelExportService;
        private ListExportedReportViewModel _listExportedReportViewModel;
        private IEnumerable<WaterProofingTestGET> preListReport;
        private readonly ProductStore _productStore;
        private readonly TesterStore _testerStore;
        public ObservableCollection<string> ListProductNames
        {
            get => _productStore.ListProductName;
        }
        public ObservableCollection<string> ListTesterIds
        {
            get => _testerStore.ListTesterIds;
        }
        public ListExportedReportViewModel ListExportedReportViewModel { get => _listExportedReportViewModel; }
        private ObservableCollection<WaterProofingTestSample> listReport = new ObservableCollection<WaterProofingTestSample> { };
        public ObservableCollection<WaterProofingTestSample> ListReport

        {
            get { return listReport; }
            set
            {
                listReport=value;
            }
        }
        public ObservableCollection<WaterProofingTestSample> ListPreReport = new ObservableCollection<WaterProofingTestSample>( )
;


        #region tester
        private string testerId;
        public string TesterId
        {
            get => testerId;
            set
            {
                Application.Current.Dispatcher.Invoke(new Action(( ) =>
                {
                    testerId=value;
                    Tester=(from p in _testerStore.ListTesters
                            where value==p.ToToggleString( )
                            select p).FirstOrDefault( );
                    foreach ( var item in ListReport )
                    {
                        item.Tester=Tester.lastName+" "+Tester.firstName;
                    }
                    CollectionViewSource.GetDefaultView(ListReport).Refresh( );
                    OnPropertyChanged("ListReport");
                }));

            }
        }
        public Tester Tester;
        #endregion
        #region product
        public Product Product;
        private string productName = "";
        public string ProductName
        {
            get { return productName; }
            set
            {

                if ( productName!=value )
                {
                    productName=value;
                    UpdateConfigurationDatabase( );
                }
                Product=(from p in _productStore.ListProducts
                         where value==p.Name
                         select p).FirstOrDefault( );
            }
        }
        #endregion
        private bool isEdit;

        public bool IsEdit
        {
            get { return isEdit; }
            set
            {
                if ( isEdit!=value )
                {
                    isEdit=value;
                    OnPropertyChanged( );
                }
            }
        }
        private string comment;

        public string Comment
        {
            get { return comment; }
            set
            {
                if ( comment!=value )
                {
                    comment=value;
                    UpdateConfigurationDatabase( );
                }
            }
        }
        private DateTime timeStampStart = DateTime.Now;

        public DateTime TimeStampStart
        {
            get { return timeStampStart; }
            set
            {
                if ( timeStampStart!=value )
                {
                    timeStampStart=value;
                    UpdateConfigurationDatabase( );
                }
            }
        }
        private DateTime timeStampFinish = DateTime.Now;

        public DateTime TimeStampFinish
        {
            get { return timeStampFinish; }
            set
            {
                if ( timeStampFinish!=value )
                {
                    timeStampFinish=value;
                    UpdateConfigurationDatabase( );
                }
            }
        }

        private int testPurpose;

        public int TestPurpose
        {
            get { return testPurpose; }
            set
            {
                if ( testPurpose!=value )
                {

                    testPurpose=value;
                    if ( testPurpose!=3 ) { comment=""; }
                    UpdateConfigurationDatabase( );
                }
            }
        }
        public bool IsCheckGetReport { set; get; } = true;
        public ICommand ExportDataCommand { get; set; }
        public ICommand ImportDataCommand { get; set; }
        public ICommand UpdateDataCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand BackPreReportCommand { get; set; }
        private ControlPlcService _controlPlcService;
        private S71200WaterProofingMachineService _s71200WaterProofingMachineService;
        public WaterProofingReportViewModel (IDatabaseService databaseService,
            ListExportedReportViewModel listExportedReportViewModel,
            IExcelExportService excelExportService,
            IApiService apiService,
            ProductStore productStore,
            TesterStore testerStore,
            ControlPlcService controlPlcService,
            S71200WaterProofingMachineService waterproofing)
        {
            _controlPlcService=controlPlcService;
            _s71200WaterProofingMachineService=waterproofing;
            _s71200WaterProofingMachineService.DataUpdated+=LoadPreReport;
            WaterProofingSupervisorViewModel.UpdateIsCheckReport+=WaterProofingSupervisorViewModel_UpdateIsCheckReport;
            _databaseService=databaseService;
            _apiService=apiService;
            _excelExportService=excelExportService;
            _productStore=productStore;
            _testerStore=testerStore;
            WaterProofingSettingsViewModel.UpdatePreReport+=WaterProofingSettingsViewModel_UpdatePreReport;
            Comment="";
            _listExportedReportViewModel=listExportedReportViewModel;
            _listExportedReportViewModel.SelectiedReportChange+=ReportSelected;

            Initialization( );
            //  WaterProofingSupervisorViewModel.UpdateDatabase+=SubPreNumber;
            ExportDataCommand=new RelayCommand(ExportTests);
            ImportDataCommand=new RelayCommand(GetExportedTests);
            UpdateDataCommand=new RelayCommand(UpdateSheetDatabase);
            UpdateDataCommand=new RelayCommand(UpdateSheetDatabase);
            ResetCommand=new RelayCommand(Reset);
            IsEdit=true;
            BackPreReportCommand=new RelayCommand(async ( ) =>
            {
                IsCheckGetReport=true;
                ListReport.Clear( );
                var listTotal = _databaseService.LoadPreReportWaterProofing( ).Result;
                int i = 1;
                foreach ( var item in listTotal )
                {
                    ListReport.Add(new WaterProofingTestSample( ) { Id=i++,Duration=item.Time,Temperature=item.Temperature });
                }
            });
        }
        private async void Reset ( )
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn reset lại bài test","Thông báo",MessageBoxButton.OKCancel,MessageBoxImage.Question);
            if ( result==MessageBoxResult.OK )
            {
                _controlPlcService._waterproofing.SetMemoryBit("resetnumber");
                _databaseService.DeletePreReportWaterProofing( );
                ListReport.Clear( );
            }
        }

        private void WaterProofingSupervisorViewModel_UpdateIsCheckReport (bool obj)
        {
            IsCheckGetReport=obj;
            if ( IsCheckGetReport )
            {
                ListReport.Clear( );
            }
        }



        private void WaterProofingSettingsViewModel_UpdatePreReport (WaterProofingTestSample obj)
        {
            ListPreReport.Add(obj);
        }

        public int PreNumber = 0;
        public int Number = 0;
        //public void SubPreNumber ( )
        //{
        //    PreNumber--;
        //}
        public void InsertReport (PreReportWaterProofing preReportWaterProofing)
        {
            _databaseService.InsertPreReportWaterProofing(preReportWaterProofing);
        }
        private void LoadPreReport (Domain.Models.PlcS71200.WaterProofingMachineMonitoringData Data)
        {
            if ( IsCheckGetReport )
            {
                Number=Data.NumberPreReport;
                if ( Number==0&&Number!=PreNumber )
                {
                    _databaseService.DeletePreReportWaterProofing( );
                }
                if ( Number==0 )
                {

                    ListReport.Clear( );
                }
                else if ( Number==PreNumber+1 )
                {
                    if ( Number==1 )
                    {
                        if ( _databaseService.LoadPreReportWaterProofing( ).Result.Count==0 )
                        {
                            InsertReport((new PreReportWaterProofing( ) { DateTime=DateTime.Now,Temperature=Data.TemperatureSP,Time=Data.HourSP+Data.MinuteSP/60 }));
                            var listTotal = _databaseService.LoadPreReportWaterProofing( ).Result;
                            int i = 1;
                            foreach ( var item in listTotal )
                            {
                                ListReport.Add(new WaterProofingTestSample( ) { Id=i++,IdPreReport=item.Id,Duration=item.Time,Temperature=item.Temperature });
                            }
                        }
                    }
                    else
                    {
                        InsertReport((new PreReportWaterProofing( ) { DateTime=DateTime.Now,Temperature=Data.TemperatureSP,Time=Data.HourSP+Data.MinuteSP/60 }));
                        var listTotal = _databaseService.LoadPreReportWaterProofing( ).Result;
                        int i = 1;
                        foreach ( var item in listTotal )
                        {
                            ListReport.Add(new WaterProofingTestSample( ) { Id=i++,IdPreReport=item.Id,Duration=item.Time,Temperature=item.Temperature });
                        }
                    }


                } else if(PreNumber ==0 && Number>1 )
                {
                    var listTotal = _databaseService.LoadPreReportWaterProofing( ).Result;
                    int i = 1;
                    foreach ( var item in listTotal )
                    {
                        ListReport.Add(new WaterProofingTestSample( ) { Id=i++,IdPreReport=item.Id,Duration=item.Time,Temperature=item.Temperature });
                    }
                }
                PreNumber=Number;

            }

        }

        private void ReportSelected (object obj)
        {
            if ( obj!=null )
            {
                IsCheckGetReport=false;
                var selectedtest = (Test)obj;
                var selectedReport = (from p in preListReport
                                      where p.EndDate==selectedtest.EndDate
                                      where p.StartDate==selectedtest.StartDate
                                      select p).FirstOrDefault( );
                if ( selectedReport!=null )
                {
                    TimeStampStart=selectedReport.StartDate;
                    TimeStampFinish=selectedReport.EndDate;
                    TestPurpose=selectedReport.TestPurpose;
                    ProductName=selectedReport.Product.Name;
                    Comment=selectedReport.Note;
                    ListReport.Clear( );

                    // Dữ liệu seeding bị ngu
                    if ( selectedReport.Samples.Count>5 )
                    {
                        for ( int i = selectedReport.Samples.Count-1;i>4;i-- )
                        {
                            selectedReport.Samples.RemoveAt(i);
                        }
                    }
                    foreach ( var item in selectedReport.Samples )
                    {
                        var sample = new WaterProofingTestSample
                        {
                            NumberOfError=item.NumberOfError,
                            Tester=item.Tester.ToString( ),
                            Note=item.Note,
                            Duration=item.Duration,
                            Passed=item.Passed,
                            Temperature=item.Temperature,
                        };
                        ListReport.Add(sample);
                    }
                }
            }
        }

        private async void GetExportedTests ( )
        {
            ListExportedReportViewModel.IsOpen=true;
            var timestart = TimeStampStart.AddDays(-1);
            var timestop = TimeStampFinish.AddDays(+1);
            var results = await _apiService.GetWaterProofingReport(timestart,timestop);
            if ( results.Success )
            {
                preListReport=results.Resource.Items;
                ListExportedReportViewModel.ListExportedReport.Clear( );
                foreach ( var result in results.Resource.Items )
                {
                    Test testconfig = new( )
                    {
                        StartDate=result.StartDate,
                        EndDate=result.EndDate,
                        Note=result.Note,
                        TestPurpose=result.TestPurpose,
                        ProductName=result.Product.Name,
                    };
                    ListExportedReportViewModel.ListExportedReport.Add(testconfig);
                }
                MessageBox.Show("Truy xuất thành công","Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu trong thời gian này","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);

            }

        }
        private async void ExportTests ( )
        {
            var Condition = (from item in ListReport
                             where item.IsReport==true
                             select item).Any( );
            if ( !Condition )
            {
                try
                {
                    var testingMachine = new WaterProofingTest
                    {
                        Note=Comment,
                        ProductName=ProductName,
                        TestPurpose=TestPurpose,
                        StartDate=TimeStampStart,
                        EndDate=TimeStampFinish
                    };
                    List<WaterProofingTestSample> sheets = new List<WaterProofingTestSample>( );
                    foreach ( var item in ListReport )
                    {
                        sheets.Add(item);
                        item.Tester=Tester.ToToggleString( );
                    }
                    testingMachine.TestSheets=sheets;
                    var result = await _excelExportService.ExportWaterProofing("21_WaterProofing.xlsm",testingMachine);
                    if ( result.Success )
                    {
                        HistoryWaterProofingReport historySoftCloseReport = new HistoryWaterProofingReport( )
                        {
                            NameTest="Bài test chống thấm ",
                            Timestamp=DateTime.Now
                        };
                        await _databaseService.InsertHistoryWaterProofing(historySoftCloseReport);

                        WaterProofingTestPOST ApiReport = new WaterProofingTestPOST
                        {
                            Note=Comment,
                            ProductId=Product.Id,
                            TestPurpose=TestPurpose,
                            StartDate=TimeStampStart,
                            EndDate=TimeStampFinish,
                            EmployeeId=Tester.employeeId
                        };
                        var samples = new List<WaterProofingSamplePOST>( );
                        foreach ( var sheet in ListReport )
                        {
                            var data = new WaterProofingSamplePOST
                            {
                                NumberOfError=sheet.NumberOfError,
                                Note=sheet.Note,
                                Duration=(int)sheet.Duration,
                                Temperature=sheet.Temperature,
                                Passed=sheet.Passed

                            };
                            samples.Add(data);
                        }
                        ApiReport.Samples=samples;

                        var post = await _apiService.PostWaterProofing(ApiReport);

                        if ( post.Success )
                        {
                            // _controlPlcService._waterproofing.SetMemoryBit("resetnumber");
                            foreach ( var sheet in ListReport )
                            {
                                sheet.IsReport=true;
                            }
                            _databaseService.DeletePreReportWaterProofing( );
                            MessageBox.Show("Gửi dữ liệu lên Server thành công","Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);

                        }
                        else
                        {
                            MessageBox.Show("Gửi dữ liệu lên Server thất bại","Cảnh báo",MessageBoxButton.OK,MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                    }
                }

                catch { }
            }
            else
            {
                MessageBox.Show("Bài test đã báo cáo trước đó");
            }


        }

        public async void Initialization ( )
        {
            //Load pre report
            //var preReport = await _databaseService.LoadWaterProofingReportAsync();
            //foreach (var report in  preReport)
            //{
            //    ListReport.Add(report);
            //}
            //// Load pre config
            //var preConfig = await _databaseService.LoadWaterProofingConfiguration();
            //if (preConfig != null)
            //{
            //    timeStampStart = preConfig.StartDate;
            //    timeStampFinish = preConfig.EndDate;
            //    productName = preConfig.ProductName;
            //    testPurpose = preConfig.TestPurpose;
            //    comment = preConfig.Note;
            //}
            //else 
            //{
            //    var idata = new WaterProofingTest();
            //    await _databaseService.InsertWaterProofingConfiguration(idata);
            //}
            ////Load product
            //Product = (from p in _productStore.ListProducts
            //           where p.Name == productName
            //           select p).FirstOrDefault();
            //Load Tester
            if ( testerId!=null&&testerId!="" )
            {

                Tester=(from p in _testerStore.ListTesters
                        where p.testerID==p.ToToggleString( )
                        select p).FirstOrDefault( );
            }
            else
            {
                TesterId=_testerStore.ListTesterIds.FirstOrDefault( );
                Tester=(from p in _testerStore.ListTesters
                        where p.testerID==p.ToToggleString( )
                        select p).FirstOrDefault( );
            }
        }

        private void UpdateConfigurationDatabase ( )
        {
            WaterProofingTest test = new WaterProofingTest( )
            {
                Id=1,
                StartDate=timeStampStart,
                EndDate=timeStampFinish,
                ProductName=productName,
                TestPurpose=testPurpose,
                Note=comment
            };
            _databaseService.UpdateWaterProofingConfiguration(test);
        }
        private async void UpdateSheetDatabase ( )
        {
            try
            {
                await _databaseService.UpdateWaterProofingReport(ListReport);
            }
            catch
            {

            }
        }


    }
}
