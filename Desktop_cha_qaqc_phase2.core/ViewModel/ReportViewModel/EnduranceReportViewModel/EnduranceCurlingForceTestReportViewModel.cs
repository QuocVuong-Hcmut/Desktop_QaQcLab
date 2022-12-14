using AutoMapper;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Domain.Stores;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
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

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.ReportViewModel.EnduranceReportViewModel
{
    public class EnduranceCurlingForceTestReportViewModel: Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly IS71200EnduranceMachineService _supervisorService;
        private readonly IDatabaseService _databaseService;
        private readonly IExcelExportService _excelExportService;
        private readonly IApiService _apiService;
        private ListExportedReportViewModel _listExportedReportViewModel;
        private readonly ProductStore _productStore;
        private readonly TesterStore _testerStore;
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
        private ObservableCollection<CurlingForceTestSample> listReport = new ObservableCollection<CurlingForceTestSample> { };

        public ListExportedReportViewModel ListExportedReportViewModel
        {
            get => _listExportedReportViewModel;
        }
        private IEnumerable<CurlingForceTestGET> preListReport;
        public ObservableCollection<CurlingForceTestSample> ListReport
        {
            get { return listReport; }
            set
            {
                if ( value.Count<6 )
                {
                    listReport=value;
                    //  UpdateSheetDatabase( );
                }
            }
        }
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
                }
                UpdateConfigurationDatabase( );
            }
        }
        public bool IsCheckGetReport { set; get; } = true;
        #region Command 
        public ICommand ExportDataCommand { get; set; }
        public ICommand ImportDataCommand { get; set; }
        public ICommand UpdateDataCommand { get; set; }
        public ICommand UpdateConfigurationCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand BackPreReportCommand { get; set; }
        #endregion
        private ControlPlcService _controlPlcService;
        public EnduranceCurlingForceTestReportViewModel (
            S71200EnduranceMachineService supervisorService,
            IDatabaseService databaseService,
            IExcelExportService excelExportService,
            IApiService apiService,
            ListExportedReportViewModel listExportedReportViewModel,
            ProductStore productStore,
            TesterStore testerStore,
            ControlPlcService controlPlcService)
        {
            _controlPlcService=controlPlcService;
            _supervisorService=supervisorService;

            _databaseService=databaseService;
            _apiService=apiService;
            _excelExportService=excelExportService;
            _productStore=productStore;
            _testerStore=testerStore;
            _supervisorService.DataUpdated+=LoadPreReport;
            ListReport.Clear( );
            Comment="";
            _listExportedReportViewModel=listExportedReportViewModel;
            _listExportedReportViewModel.SelectiedReportChange+=ReportSelected;

            Initialization( );
            IsEdit=true;
            ImportDataCommand=new RelayCommand(GetExportedTests);
            ExportDataCommand=new RelayCommand(ExportTests);
            UpdateDataCommand=new RelayCommand(UpdateSheetDatabase);
            UpdateConfigurationCommand=new RelayCommand(UpdateConfigurationDatabase);
            ResetCommand=new RelayCommand(Reset);
            //     EnduranceSupervisorViewModel.UpdateDatabase+=SubPreNumber;
            BackPreReportCommand=new RelayCommand(async ( ) =>
            {
                IsCheckGetReport=true;
                ListReport.Clear( );
                var listTotal = _databaseService.LoadPreReportEndurance( ).Result;
                int i = 1;
                foreach ( var item in listTotal )
                {
                      ListReport.Add(new CurlingForceTestSample( ) { Id=i++,Load=item.Force2,Duration=item.Time2 }); ; ;
                }
            });
        }

        private async void Reset ( )
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn reset lại bài test","Thông báo",MessageBoxButton.OKCancel,MessageBoxImage.Question);
            if ( result==MessageBoxResult.OK )
            {
                _controlPlcService._endurance.SetMemoryBit("resetnumber");
                _databaseService.DeletePreReportEndurance( );
                ListReport.Clear( );
            }

        }

        public int PreNumber = 0;
        public int Number = 0;

        public void InsertReport (PreReportEndurance preReportEndurance)
        {
            _databaseService.InsertPreReportEndurance(preReportEndurance);

        }
        private void LoadPreReport (Domain.Models.PlcS71200.EnduranceMachineMonitoringData Data)
        {
            if ( IsCheckGetReport )
            {
                Number=Data.NumberReport;
                if ( Number==0&&Number!=PreNumber )
                {
                    _databaseService.DeletePreReportEndurance( );
                }
                if ( Number==0 )
                {
                    ListReport.Clear( );
                }
                else if ( Number==PreNumber+1 )
                {
                    if ( Number==1 )
                    {
                        if ( _databaseService.LoadPreReportEndurance( ).Result.Count==0 )
                        {
                            InsertReport((new PreReportEndurance( )
                            {
                                DateTime=DateTime.Now,
                                Force1=Data.Cylinder12ForceSP,
                                Force2=Data.Cylinder3ForceSP,
                                Time2=Data.HoldingTime3SP,
                                Time1=Data.HoldingTime12SP,
                                Number1=Data.NumberOfPresses12SP,
                                Number2=Data.NumberOfPresses3SP
                            }));
                            var listTotal = _databaseService.LoadPreReportEndurance( ).Result;
                            int i = 1;
                            foreach ( var item in listTotal )
                            {
                                ListReport.Add(new CurlingForceTestSample( ) { Id=i++,Load=item.Force2,Duration=item.Time2 }); ; ;
                            }
                        }
                    }
                    else
                    {
                        InsertReport((new PreReportEndurance( )
                        {
                            DateTime=DateTime.Now,
                            Force1=Data.Cylinder12ForceSP,
                            Force2=Data.Cylinder3ForceSP,
                            Time2=Data.HoldingTime3SP,
                            Time1=Data.HoldingTime12SP,
                            Number1=Data.NumberOfPresses12SP,
                            Number2=Data.NumberOfPresses3SP
                        }));
                        var listTotal = _databaseService.LoadPreReportEndurance( ).Result;
                        int i = 1;
                        foreach ( var item in listTotal )
                        {
                            ListReport.Add(new CurlingForceTestSample( ) { Id=i++,Load=item.Force2,Duration=item.Time2 }); ; ;
                        }
                    }


                }
                else if ( PreNumber==0&&Number>1 )
                {
                    var listTotal = _databaseService.LoadPreReportEndurance( ).Result;
                    int i = 1;
                    foreach ( var item in listTotal )
                    {
                        ListReport.Add(new CurlingForceTestSample( ) { Id=i++,Load=item.Force2,Duration=item.Time2 }); ; ;
                    }
                }
                PreNumber=Number;

            }



        }
        private async void Initialization ( )
        {
            try
            {
                //Load sheet trước đó
                var previoussheets = await _databaseService.LoadCurlingForceTestSample( );
                foreach ( var sheet in previoussheets )
                {
                    //  ListReport.Add(sheet);
                };
                //Load configuration
                var previousconfig = await _databaseService.LoadCurlingForceConfiguration( );
                if ( previousconfig!=null )
                {

                    ProductName=previousconfig.ProductName;
                    Comment=previousconfig.Note;
                    TimeStampStart=previousconfig.StartDate;
                    TimeStampFinish=previousconfig.EndDate;
                    TestPurpose=previousconfig.TestPurpose;
                }
                else
                {
                    var idata = new CurlingForceTest( );
                    await _databaseService.InsertCurlingForceConfiguration(idata);
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
            }
            catch
            {

            }
        }
        private async void UpdateConfigurationDatabase ( )
        {
            try
            {
                var testingConfigurations = new CurlingForceTest
                {
                    Id=1,
                    TestPurpose=Convert.ToInt32(TestPurpose),
                    ProductName=ProductName,
                    Note=Comment,
                    StartDate=TimeStampStart,
                    EndDate=TimeStampFinish
                };
                await _databaseService.UpdateCurlingForceConfiguration(testingConfigurations);
            }
            catch ( Exception ex )
            {
                //Console.WriteLine(ex.ToString());
            }
        }
        private async void UpdateSheetDatabase ( )
        {
            await _databaseService.UpadateCurlingForceReport(ListReport);
        }
        private void ReportSelected (object obj)
        {
            IsCheckGetReport=false;
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

                    // Dữ liệu seeding bị ngu
                    if ( selectedReport.Samples.Count>5 )
                    {
                        for ( int i = selectedReport.Samples.Count-1;i>4;i-- )
                        {
                            selectedReport.Samples.RemoveAt(i);
                        }
                    }
                    int id = 1;
                    foreach ( var item in selectedReport.Samples )
                    {
                        var sample = new CurlingForceTestSample
                        {
                            Id=id,
                            NumberOfError=item.NumberOfError,
                            Note=item.Note,
                            Tester=item.Tester.ToString( ),
                            Load=item.Load,
                            Duration=item.Duration,
                            DeformationDegree=item.DeformationDegree,
                        };
                        id++;
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
            var results = await _apiService.GetCurlingForceReport(timestart,timestop);
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
                var testingMachine = new CurlingForceTest
                {
                    Id=1,
                    Note=Comment,
                    ProductName=ProductName,
                    TestPurpose=TestPurpose,
                    StartDate=TimeStampStart,
                    EndDate=TimeStampFinish
                };
                List<CurlingForceTestSample> sheets = new List<CurlingForceTestSample>( );
                foreach ( var item in ListReport )
                {
                    sheets.Add(item);
                    item.Tester=Tester.ToToggleString( );
                }
                testingMachine.TestSheet=sheets;
                var success = await _excelExportService.ExportCurlingForce("08_CurlingForce.xlsm",testingMachine);
                if ( success.Success )
                {
                    HistoryEnduranceReport history = new HistoryEnduranceReport
                    {
                        Timestamp=DateTime.Now,
                        NameTest="Bài test lực uốn"
                    };
                    await _databaseService.InsertHistoryEndurance(history);

                    CurlingForceTestPOST ApiReport = new CurlingForceTestPOST
                    {
                        Note=Comment,
                        ProductId=Product.Id,
                        TestPurpose=TestPurpose,
                        StartDate=TimeStampStart,
                        EndDate=TimeStampFinish,
                        EmployeeId=Tester.employeeId
                    };
                    var samples = new List<CurlingForceTestSamplePOST>( );
                    foreach ( var sheet in ListReport )
                    {
                        var data = new CurlingForceTestSamplePOST
                        {
                            NumberOfError=sheet.NumberOfError,
                            Note=sheet.Note,
                            Load=(double)sheet.Load,
                            Duration=(int)sheet.Duration,
                            DeformationDegree=(double)sheet.DeformationDegree,
                        };

                        samples.Add(data);
                    }
                    ApiReport.Samples=samples;

                    var post = await _apiService.PostCurlingForce(ApiReport);

                    if ( post.Success )
                    {
                        MessageBox.Show("Gửi dữ liệu lên Server thành công","Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
                        _controlPlcService._endurance.SetMemoryBit("resetnumber");
                        _databaseService.DeletePreReportEndurance( );
                        foreach ( var sheet in ListReport )
                        {
                            sheet.IsReport=true;
                        }

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




        }
    }
}