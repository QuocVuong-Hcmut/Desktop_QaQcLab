using AutoMapper;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel
{
    public class SoftCloseReportViewModel: Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        #region Private Field
        private readonly ILogoSoftCloseMachineService _supervisorService;
        private readonly IApiService _apiService;
        private readonly ProductStore _productStore;
        private readonly TesterStore _testerStore;
        private readonly IDatabaseService _databaseService;
        private readonly IExcelExportService _excelExportService;
        private readonly DataConfigDialogViewModel _dataConfigDialogViewModel;
        private readonly ListExportedReportViewModel _listExportedReportViewModel;

        /// <summary>
        /// ViewModel của DataConfigDiaglogView
        /// </summary>
        public DataConfigDialogViewModel DataConfigDialogViewModel
        {
            get => _dataConfigDialogViewModel;
        }
        /// <summary>
        /// ViewModel của ListExportedReportView
        /// </summary>
        public ListExportedReportViewModel ListExportedReportViewModel
        {
            get => _listExportedReportViewModel;
        }

        /// <summary>
        /// Điều kiện xét reset Db
        /// </summary>
        private int? preNumberClosingPV;
        #endregion
        #region Property
        public ObservableCollection<string> ListProductNames
        {
            get => _productStore.ListProductName;
        }
        public ObservableCollection<string> ListTesterIds
        {
            get => _testerStore.ListTesterIds;
        }
        private ObservableCollection<SoftCloseTestSample> listReport = new ObservableCollection<SoftCloseTestSample> { new SoftCloseTestSample( ) };
        public ObservableCollection<SoftCloseTestSample> ListReport
        {
            get { return listReport; }
            set
            {
                listReport=value;
                OnPropertyChanged( );
            }
        }
        private IEnumerable<SoftCloseTestGET> preListReport;
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
        public int TestPurpose
        {
            get { return testPurpose; }
            set
            {
                if ( testPurpose!=value )
                {
                    testPurpose=value;
                    if ( testPurpose!=3 )
                    {
                        comment="";
                    }
                    UpdateConfigurationDatabase( );
                }
            }
        }
        #endregion
        #region Command
        /// <summary>
        /// Nút Xem dữ liệu
        /// </summary>
        public ICommand DataWatchingCommand { get; set; }
        /// <summary>
        /// Nút xuất báo cáo
        /// </summary>
        public ICommand ExportDataCommand { get; set; }
        /// <summary>
        /// 
        /// Nút Truy xuất
        /// </summary>
        public ICommand ImportDataCommand { get; set; }
        /// <summary>
        /// Gọi đến lệnh này khi có sự kiện thay đổi trong datagrid
        /// </summary>
        public ICommand UpdateDataCommand { get; set; }
        #endregion
        #region Contructor
        public SoftCloseReportViewModel (
            ILogoSoftCloseMachineService supervisorService,
            IDatabaseService databaseService,
            IExcelExportService excelExportService,
            DataConfigDialogViewModel DataConfigDialogViewModel,
            ListExportedReportViewModel listExportedReportViewModel,
            IApiService apiService,
            ProductStore productStore,
            TesterStore testerStore)
        {
            _supervisorService=supervisorService;
            _apiService=apiService;
            _productStore=productStore;
            _testerStore=testerStore;
            _databaseService=databaseService;
            _excelExportService=excelExportService;
            _dataConfigDialogViewModel=DataConfigDialogViewModel;
            _dataConfigDialogViewModel.SaveAction+=SaveAction;
            Comment="";
            _listExportedReportViewModel=listExportedReportViewModel;
            _listExportedReportViewModel.SelectiedReportChange+=ReportSelected;
            ExportDataCommand=new RelayCommand(ExportTests);
            DataWatchingCommand=new RelayCommand(( ) => _dataConfigDialogViewModel.IsOpen=true);
            UpdateDataCommand=new RelayCommand(UpdateSheetDatabase);
            ImportDataCommand=new RelayCommand(GetExportedTests);
            Initialization( );
            _supervisorService.ResetStatus+=ResetDatabase;
        }

        private void ReportSelected (object obj)
        {
            if ( obj!=null )
            {
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

                    // format dữ liệu seeding
                    if ( selectedReport.Samples.Count>20 )
                    {
                        for ( int i = selectedReport.Samples.Count-1;i>19;i-- )
                        {
                            selectedReport.Samples.RemoveAt(i);
                        }
                    }
                    foreach ( var item in selectedReport.Samples )
                    {
                        var sample = new SoftCloseTestSample
                        {
                            NumberOfClosing=item.NumberOfClosing,
                            NumberOfError=item.NumberOfError,
                            Note=item.Note,
                            Tester=item.Tester.ToString( ),
                            FallTimeLid=item.SeatLidResult.FallTime,
                            IsBumperLidIntact=item.SeatLidResult.IsBumperIntact,
                            IsBumperLidUnleaked=item.SeatLidResult.IsUnleaked,
                            IsLidPassed=item.SeatLidResult.Passed,
                            FallTimeRing=item.SeatRingResult.FallTime,
                            IsBumperRingIntact=item.SeatRingResult.IsBumperIntact,
                            IsBumperRingUnleaked=item.SeatRingResult.IsUnleaked,
                            IsRingPassed=item.SeatRingResult.Passed
                        };
                        ListReport.Add(sample);
                    }
                }

            }
        }
        #endregion
        #region Initialization 
        private async void Initialization ( )
        {
            try
            {
                //Load Configuration
                var preconfig = await _databaseService.LoadSoftCloseConfiguration( );
                if ( preconfig!=null )
                {
                    productName=preconfig.ProductName;
                    comment=preconfig.Note;
                    timeStampStart=preconfig.StartDate;
                    timeStampFinish=preconfig.EndDate;
                    testPurpose=preconfig.TestPurpose;
                }
                else
                {
                    var idata = new SoftCloseTest( );
                    await _databaseService.InsertSoftCloseConfiguration(idata);
                }
                //Load product
                Product=(from p in _productStore.ListProducts
                         where p.Name==productName
                         select p).FirstOrDefault( );
                //Load Tester
                if ( testerId!=null&&testerId!="" )
                {
                    Tester=(from p in _testerStore.ListTesters
                            where p.testerID==testerId
                            select p).FirstOrDefault( );
                }
                else
                {
                    TesterId=_testerStore.ListTesterIds.FirstOrDefault( );
                    Tester=(from p in _testerStore.ListTesters
                            where p.testerID==testerId
                            select p).FirstOrDefault( );
                }
                // Load preNumberOfClosing 
                var data = await _databaseService.LoadSoftCloseTestReport( );
                //update Tester
                //if ( data!=null )
                //{
                //    foreach ( var item in data )
                //    {
                //        item.Tester=Tester.ToString( );
                //    }
                //}
                if ( (data!=null)&&(data.Count( )!=0) )
                {

                    preNumberClosingPV=data.ElementAt(data.Count( )-1).NumberOfClosing;
                }
                else
                {
                    preNumberClosingPV=0;
                }

            }
            catch
            {

            }
        }
        #endregion
        #region ExportExcel
        private async void GetExportedTests ( )
        {
            ListExportedReportViewModel.IsOpen=true;
            var timestart = TimeStampStart.AddDays(-1);
            var timestop = TimeStampFinish.AddDays(+1);
            var results = await _apiService.GetSoftCloseReport(timestart,timestop);
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
            try
            {
                var testingMachine = new SoftCloseTest
                {
                    Note=Comment,
                    ProductName=ProductName,
                    TestPurpose=TestPurpose,
                    StartDate=TimeStampStart,
                    EndDate=TimeStampFinish,
                };
                List<SoftCloseTestSample> sheets = new List<SoftCloseTestSample>( );
                foreach ( var item in ListReport )
                {
                    sheets.Add(item);
                    item.Tester=Tester.ToToggleString( );
                }
                testingMachine.Testsheet=sheets;
                var result = await _excelExportService.ExportSoftClose("04_SoftClose.xlsm",testingMachine,_dataConfigDialogViewModel.Mode);

                if ( result.Success )
                {
                    HistorySoftCloseReport historySoftCloseReport = new HistorySoftCloseReport( )
                    {
                        NameTest="Bài test độ bền êm",
                        Timestamp=DateTime.Now
                    };
                    await _databaseService.InsertHistoryReliability(historySoftCloseReport);

                    SoftCloseTestPOST ApiReport = new SoftCloseTestPOST
                    {
                        Note=Comment,
                        ProductId=Product.Id,
                        TestPurpose=TestPurpose,
                        StartDate=TimeStampStart,
                        EndDate=TimeStampFinish,
                        EmployeeId=Tester.testerID
                    };
                    var samples = new List<SoftCloseTestSamplePOST>( );
                    foreach ( var sheet in ListReport )
                    {
                        var data = new SoftCloseTestSamplePOST
                        {
                            NumberOfError=sheet.NumberOfError,
                            Note=sheet.Note,
                            NumberOfClosing=sheet.NumberOfClosing,
                            SeatRingResult=new SoftCloseTestSampleResultAPI( )
                            {
                                FallTime=(int)sheet.FallTimeRing,
                                IsBumperIntact=sheet.IsBumperRingIntact,
                                IsUnleaked=sheet.IsBumperRingUnleaked,
                                Passed=sheet.IsRingPassed
                            },
                            SeatLidResult=new SoftCloseTestSampleResultAPI( )
                            {
                                FallTime=(int)sheet.FallTimeLid,
                                IsBumperIntact=sheet.IsBumperLidIntact,
                                IsUnleaked=sheet.IsBumperLidUnleaked,
                                Passed=sheet.IsLidPassed
                            }
                        };
                        samples.Add(data);
                    }
                    ApiReport.Samples=samples;

                    var post = await _apiService.PostSoftCloseReport(ApiReport);

                    if ( post.Success )
                    {
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
            catch
            {

            }

        }
        #endregion
        #region Database service
        private async void SaveAction (object? ob,EventArgs e)
        {
            listReport.Clear( );
            var SampleIndex = _dataConfigDialogViewModel.GetListSortedIndexes( );
            var data = await _databaseService.LoadSoftCloseReportBySample(SampleIndex);
            foreach ( var item in data )
            {
                if ( item==null ) continue;
                ListReport.Add(item);
            }
        }
        private async void ResetDatabase ( )
        {
            await _databaseService.ClearSoftCloseTestSample( );
        }
        private async void UpdateSheetDatabase ( )
        {
            try
            {
                await _databaseService.UpdateSoftCloseReportByListSample(ListReport);
            }
            catch
            {

            }
        }
        private async void UpdateConfigurationDatabase ( )
        {
            try
            {
                var testingConfigurations = new SoftCloseTest
                {
                    Id=1,
                    TestPurpose=Convert.ToInt32(TestPurpose),
                    ProductName=ProductName,
                    Note=Comment,
                    StartDate=TimeStampStart,
                    EndDate=TimeStampFinish
                };
                await _databaseService.UpdateSoftCloseConfiguration(testingConfigurations);
            }
            catch ( Exception ex )
            {
                //Console.WriteLine(ex.ToString());
            }
        }
        #endregion
    }
}
