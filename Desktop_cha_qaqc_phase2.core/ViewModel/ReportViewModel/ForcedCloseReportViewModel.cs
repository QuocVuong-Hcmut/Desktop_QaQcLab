using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Domain.Stores;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
namespace Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel
{
    public class ForcedCloseReportViewModel: Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly ILogoForceCloseMachineService _supervisorService;
        private readonly ILogoForceCloseService _forceCloseService;
        private readonly IDatabaseService _databaseService;
        private readonly IApiService _apiService;
        private readonly IExcelExportService _excelExportService;
        private readonly ProductStore _productStore;
        private readonly TesterStore _testerStore;
        public bool IsCheckGetReport { set; get; } = true;
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
        public ObservableCollection<string> ListProductNames
        {
            get => _productStore.ListProductName;
        }
        public ObservableCollection<string> ListTesterIds
        {
            get => _testerStore.ListTesterIds;
        }

        private ListExportedReportViewModel _listExportedReportViewModel;
        public ListExportedReportViewModel ListExportedReportViewModel
        {
            get => _listExportedReportViewModel;
        }
        private ObservableCollection<ForcedCloseTestSample> listReport = new ObservableCollection<ForcedCloseTestSample> { };
        public ObservableCollection<ForcedCloseTestSample> ListPreReport = new ObservableCollection<ForcedCloseTestSample> { };
        public ObservableCollection<ForcedCloseTestSample> ListReport
        {
            get { return listReport; }
            set
            {
                listReport=value;
                OnPropertyChanged( );
            }
        }
        private string comment = "";
        public string Comment
        {
            get { return comment; }
            set { if ( comment!=value ) { comment=value; UpdateConfigurationDatabase( ); } }
        }
        private DateTime timeStampStart = DateTime.Now;
        public DateTime TimeStampStart
        {
            get { return timeStampStart; }
            set { if ( timeStampStart!=value ) { timeStampStart=value; UpdateConfigurationDatabase( ); } }
        }
        private DateTime timeStampFinish = DateTime.Now;

        public DateTime TimeStampFinish
        {
            get { return timeStampFinish; }
            set { if ( timeStampFinish!=value ) { timeStampFinish=value; UpdateConfigurationDatabase( ); } }
        }
        private int testPurpose;
        private IEnumerable<ForcedCloseTestGET> preListReport;

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
        #region tester
        public string abc { get; set; }
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
                //Cập nhật Product
                Product=(from p in _productStore.ListProducts
                         where value==p.Name
                         select p).FirstOrDefault( );
            }
        }
        #endregion
        #region Command 
        public ICommand ExportDataCommand { get; set; }
        public ICommand ImportDataCommand { get; set; }
        public ICommand UpdateDataCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand BackPreReportCommand { get; set; }
        #endregion
        public ForcedCloseReportViewModel (
            ILogoForceCloseMachineService supervisorService,
            ILogoForceCloseService logoForceCloseService,
            IDatabaseService databaseService,
            IExcelExportService excelExportService,
            ListExportedReportViewModel listExportedReportViewModel,
            IApiService apiService,
            ProductStore productStore,
            TesterStore testerStore)
        {
            _forceCloseService = logoForceCloseService;
            _listExportedReportViewModel =listExportedReportViewModel;
            _listExportedReportViewModel.SelectiedReportChange+=ReportSelected;
            _supervisorService=supervisorService;
            _productStore=productStore;
            _testerStore=testerStore;
            _supervisorService.DataUpdated+=UpdateDatabase;

            _databaseService=databaseService;
            _excelExportService=excelExportService;
            _apiService=apiService;
            Comment="";
            Initialization( );
            ListReport.Clear( );
            ExportDataCommand =new RelayCommand(ExportTests);
            ImportDataCommand=new RelayCommand(GetExportedTests);
            UpdateDataCommand=new RelayCommand(UpdateSheetDatabase);
            ResetCommand = new RelayCommand(Reset);
            BackPreReportCommand=new RelayCommand(async ( ) =>
            {
                IsCheckGetReport=true;
                ListReport.Clear( );
                foreach ( var item in _databaseService.LoadPreReportForcedClose( ).Result )
                {
                    ListReport.Add(new ForcedCloseTestSample( ) { FallTimeLid=item.TimeClosing1,NumberOfClosing=item.Number,FallTimeRing=item.TimeClosing2 });
                }
            });
        }
        private async void Reset()
        {
            _forceCloseService.ControlActive("resetnumber");
            _databaseService.DeletePreReportForcedClose();
            ListReport.Clear();
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

                    if ( selectedReport.samples.Count>6 )
                    {
                        for ( int i = selectedReport.samples.Count-1;i>5;i-- )
                        {
                            selectedReport.samples.RemoveAt(i);
                        }
                    }
                    //else if (selectedReport.samples.Count < 6)
                    //{
                    //    for (int i = selectedReport.samples.Count; i < 6; i++)
                    //    {
                    //        ForcedCloseTestSampleGET forcedCloseTestGET = new ForcedCloseTestSampleGET();
                    //        selectedReport.samples.Add(forcedCloseTestGET);
                    //    }
                    //}
                    foreach ( var item in selectedReport.samples )
                    {
                        var sample = new ForcedCloseTestSample
                        {
                            NumberOfClosing=item.NumberOfClosing,
                            NumberOfError=item.NumberOfError,
                            Note=item.Note,
                            Tester=item.Tester.ToString( ),
                            FallTimeLid=item.SeatLidResult.FallTime,
                            IsLidIntact=item.SeatLidResult.IsIntact,
                            IsLidUnleaked=item.SeatLidResult.IsUnleaked,
                            IsLidPassed=item.SeatLidResult.Passed,
                            FallTimeRing=item.SeatRingResult.FallTime,
                            IsRingIntact=item.SeatRingResult.IsIntact,
                            IsRingUnleaked=item.SeatRingResult.IsUnleaked,
                            IsRingPassed=item.SeatRingResult.Passed
                        };
                        ListReport.Add(sample);
                    }
                }
            }
        }
        private async void Initialization ( )
        {
            try
            {
                //Load sheet trước đó 
                var previoussheets = await _databaseService.LoadFocedCloseReport( );
                if ( (previoussheets!=null)&&(previoussheets.Count( )!=0) )
                {

                    foreach ( var sheet in previoussheets )
                    {
                        ListReport.Add(sheet);
                    };
                }
                //Load configuration
                var preconfig = await _databaseService.LoadForcedCloseConfiguration( );
                if ( preconfig!=null )
                {

                    ProductName=preconfig.ProductName;
                    Comment=preconfig.Note;
                    TimeStampStart=preconfig.StartDate;
                    TimeStampFinish=preconfig.EndDate;
                    TestPurpose=preconfig.TestPurpose;
                }
                else
                {
                    var idata = new ForcedCloseTest( );
                    await _databaseService.InsertForcedCloseConfiguration(idata);
                }
                //Load product
                Product=(from p in _productStore.ListProducts
                         where p.Name==productName
                         select p).FirstOrDefault( );
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
            catch
            {
            }
        }
        private async void GetExportedTests ( )
        {
            _listExportedReportViewModel.IsOpen=true;
            var timestart = TimeStampStart.AddDays(-1);
            var timestop = TimeStampFinish.AddDays(+1);
            var results = await _apiService.GetForcedCloseReport(timestart,timestop);
            if ( results.Success==true )
            {
                try
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
                catch
                {
                    MessageBox.Show("Không có dữ liệu trong thời gian này","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }
        private async void ExportTests ( )
        {
            var Condition = (from item in ListReport
                             where item.IsReport==true
                             select item).Any( );
            if ( Condition )
            {
                var testingMachine = new ForcedCloseTest
                {
                    Note=Comment,
                    ProductName=ProductName,
                    TestPurpose=TestPurpose,
                    StartDate=TimeStampStart,
                    EndDate=TimeStampFinish
                };
                List<ForcedCloseTestSample> sheets = new List<ForcedCloseTestSample>( );
                foreach ( var item in ListReport )
                {
                    sheets.Add(item);
                    item.Tester=Tester.ToToggleString( );
                }
                testingMachine.Testsheet=sheets;
                var result = await _excelExportService.ExportDeformation("05_ForcedClose.xlsm",testingMachine);
                if ( result!=null )
                {
                    if ( result.Success )
                    {
                        HistoryForcedCloseReport historyForcedCloseReport = new HistoryForcedCloseReport( )
                        {
                            NameTest="Bài test cưỡng bức",
                            Timestamp=DateTime.Now
                        };
                        await _databaseService.InsertHistoryDeformation(historyForcedCloseReport);
                        ForcedCloseTestPOST ApiReport = new ForcedCloseTestPOST( );
                        ApiReport.Note=Comment;
                        ApiReport.ProductId=Product.Id;
                        ApiReport.TestPurpose=TestPurpose;
                        ApiReport.StartDate=TimeStampStart;
                        ApiReport.EndDate=TimeStampFinish;
                        ApiReport.EmployeeId=Tester.employeeId;
                        var samples = new List<ForcedCloseTestSamplePOST>( );
                        foreach ( var sheet in ListReport )
                        {
                            var data = new ForcedCloseTestSamplePOST( );
                            data.NumberOfError=sheet.NumberOfError;
                            data.NumberOfClosing=sheet.NumberOfClosing;
                            data.Note=sheet.Note;
                            data.SeatRingResult=new ForcedCloseTestSampleResultAPI( )
                            {
                                FallTime=(int)sheet.FallTimeRing,
                                IsIntact=sheet.IsRingIntact,
                                IsUnleaked=sheet.IsRingUnleaked,
                                Passed=sheet.IsRingPassed
                            };
                            data.SeatLidResult=new ForcedCloseTestSampleResultAPI( )
                            {
                                FallTime=(int)sheet.FallTimeLid,
                                IsIntact=sheet.IsLidIntact,
                                IsUnleaked=sheet.IsRingUnleaked,
                                Passed=sheet.IsLidPassed
                            };
                            samples.Add(data);
                        }
                        ApiReport.samples=samples;

                        var post = await _apiService.PostForcedCloseReport(ApiReport);

                        if ( post.Success )
                        {
                            foreach ( var item in ListReport )
                            {
                                item.IsReport=true;
                            }
                            _databaseService.DeletePreReportForcedClose( );
                            MessageBox.Show("Gửi dữ liệu lên Server thành công","Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Gửi dữ liệu lên Server thất bại","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                    }
                }
            }

        }
        private async void UpdateConfigurationDatabase ( )
        {
            try
            {
                var testingConfigurations = new ForcedCloseTest
                {
                    Id=1,
                    TestPurpose=Convert.ToInt32(TestPurpose),
                    ProductName=ProductName,
                    Note=Comment,
                    StartDate=TimeStampStart,
                    EndDate=TimeStampFinish
                };
                await _databaseService.UpdateForcedCloseConfiguration(testingConfigurations);
            }
            catch ( Exception ex )
            {
            }
        }
        private async void UpdateSheetDatabase ( )
        {
            await _databaseService.UpdateForcedCloseReport(ListReport);
        }
        public void Database(ForcedCloseMachineMonitoringData monitoringData)
        {
            if (monitoringData.NumberOfClosingPV < 5000)
            {
                if (_databaseService.LoadPreReportForcedClose().Result.Count() > 0)
                {
                    ListReport.Clear();
                }
            }
            else if (monitoringData.NumberOfClosingPV > 5000 && monitoringData.NumberOfClosingPV < 10000)
            {
                if (_databaseService.LoadPreReportForcedClose().Result.Count() < 1)
                {
                    _databaseService.InsertPreReportForcedClose(new PreForceClose() { Number = 5000, TimeClosing1 = 0, TimeClosing2 = 0});
                    UpdateListReport();
                }
            }
            else if (monitoringData.NumberOfClosingPV > 10000 && monitoringData.NumberOfClosingPV < 15000)
            {
                if (listReport.Count < 2)
                {
                    _databaseService.InsertPreReportForcedClose(new PreForceClose() { Number = 10000, TimeClosing1 = 0 });
                    UpdateListReport();
                }
            }
            else if (monitoringData.NumberOfClosingPV > 15000 && monitoringData.NumberOfClosingPV < 20000)
            {
                if (listReport.Count < 3)
                {
                    _databaseService.InsertPreReportForcedClose(new PreForceClose() { Number = 15000, TimeClosing1 = 0, TimeClosing2 = 0 });
                    UpdateListReport();
                }
            }
            else if (monitoringData.NumberOfClosingPV > 20000 && monitoringData.NumberOfClosingPV < 25000)
            {
                if (listReport.Count < 4)
                {
                    _databaseService.InsertPreReportForcedClose(new PreForceClose() { Number = 20000, TimeClosing1 = 0 , TimeClosing2 = 0 });
                    UpdateListReport();

                }
            }
            else if (monitoringData.NumberOfClosingPV > 25000 && monitoringData.NumberOfClosingPV < 29999)
            {
                if (listReport.Count < 5)
                {
                    _databaseService.InsertPreReportForcedClose(new PreForceClose() { Number = 25000, TimeClosing1 = 0, TimeClosing2 = 0 });
                    UpdateListReport();

                }
            }
            else if (monitoringData.NumberOfClosingPV == 29999)
            {
                if (listReport.Count < 6)
                {
                    _databaseService.InsertPreReportForcedClose(new PreForceClose() { Number = 30000, TimeClosing1 = 0, TimeClosing2 =0 });
                    UpdateListReport();
                }

            }
            else
            {
              //  ListReport.Clear();
              //  UpdateListReport();
            }
        }
        //public void Database (ForcedCloseMachineMonitoringData monitoringData)
        //{
        //    if ( monitoringData.NumberOfClosingPV<5000 )
        //    {
        //        if ( _databaseService.LoadPreReportForcedClose( ).Result.Count( )>0 )
        //        {
        //            ListReport.Clear( );
        //        }
        //    }
        //    else if ( monitoringData.NumberOfClosingPV>5000&&monitoringData.NumberOfClosingPV<10000 )
        //    {
        //        if ( _databaseService.LoadPreReportForcedClose( ).Result.Count( )<1 )
        //        {
        //            _databaseService.InsertPreReportForcedClose(new PreForceClose( ) { Number=5000,TimeClosing1=(int)monitoringData.ClosingSmoothTime });
        //            UpdateListReport( );
        //        }
        //    }
        //    else if ( monitoringData.NumberOfClosingPV>10000&&monitoringData.NumberOfClosingPV<15000 )
        //    {
        //        if ( listReport.Count<2 )
        //        {
        //            _databaseService.InsertPreReportForcedClose(new PreForceClose( ) { Number=10000,TimeClosing1=(int)monitoringData.ClosingSmoothTime });
        //            UpdateListReport( );
        //        }
        //    }
        //    else if ( monitoringData.NumberOfClosingPV>15000&&monitoringData.NumberOfClosingPV<20000 )
        //    {
        //        if ( listReport.Count<3 )
        //        {
        //            _databaseService.InsertPreReportForcedClose(new PreForceClose( ) { Number=15000,TimeClosing1=(int)monitoringData.ClosingSmoothTime });
        //            UpdateListReport( );
        //        }
        //    }
        //    else if ( monitoringData.NumberOfClosingPV>20000&&monitoringData.NumberOfClosingPV<25000 )
        //    {
        //        if ( listReport.Count<4 )
        //        {
        //            _databaseService.InsertPreReportForcedClose(new PreForceClose( ) { Number=20000,TimeClosing1=(int)monitoringData.ClosingSmoothTime });
        //            UpdateListReport( );

        //        }
        //    }
        //    else if ( monitoringData.NumberOfClosingPV>25000&&monitoringData.NumberOfClosingPV<30000 )
        //    {
        //        if ( listReport.Count<5 )
        //        {
        //            _databaseService.InsertPreReportForcedClose(new PreForceClose( ) { Number=25000,TimeClosing1=(int)monitoringData.ClosingSmoothTime });
        //            UpdateListReport( );

        //        }
        //    }
        //    else if ( monitoringData.NumberOfClosingPV>29990 )
        //    {
        //        if ( listReport.Count<6 )
        //        {
        //            _databaseService.InsertPreReportForcedClose(new PreForceClose( ) { Number=30000,TimeClosing1=(int)monitoringData.ClosingSmoothTime });
        //            UpdateListReport( );
        //        }

        //    }
        //    else
        //    {
        //        ListReport.Clear( );
        //        UpdateListReport( );
        //    }
        //}

        private async void UpdateDatabase (ForcedCloseMachineMonitoringData monitoringData)
        {
            if ( IsCheckGetReport )
            {
                Database(monitoringData);
            }
            

        }
        public void UpdateListReport ( )
        {
            if ( _databaseService.LoadPreReportForcedClose( ).Result.Count( )!=ListReport.Count( ) )
            {

                ListReport.Clear( );
                foreach ( var item in _databaseService.LoadPreReportForcedClose( ).Result )
                {
                    ListReport.Add(new ForcedCloseTestSample( ) { FallTimeLid=item.TimeClosing1,NumberOfClosing=item.Number, FallTimeRing=item.TimeClosing2 });
                }
            }
        }
    }
}
