using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Timers;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel
{
    public class WaterProofingSupervisorViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private ConfirmSettingViewModel _confirmSettingViewModel;
        public ConfirmSettingViewModel ConfirmSettingViewModel { get => _confirmSettingViewModel; }
        public ILiveChartService LiveChartService { get; set; }
        private readonly S71200WaterProofingMachineService _supervisorService;
        private readonly ISignalRService _signalRService;
        public string TimeOperation { get; set; }
        public int Minute_PV { get; set; }
        public int Hour_PV { get; set; }
        public int Second_PV { get; set; }
        public bool StartStatus { get; set; }
        public bool WarningStatus { get; set; }
        public float Temperature_PV { get; set; }
        public float TemperatureDelay_PV { get; set; }
        public float Temperature_SP { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public static event Action<bool> UpdateIsCheckReport;
        public static event Action UpdateDatabase;
        public Timer timer1;
        private bool canPlotingChart;
        public IDatabaseService _databaseService;
        public WaterProofingSupervisorViewModel(S71200WaterProofingMachineService waterproofing, 
            ILiveChartService liveChartService, 
            IDatabaseService databaseService,
            ConfirmSettingViewModel confirmSettingViewModel, 
            ISignalRService signalRService)
        {
            _databaseService=databaseService;
            #region liveChart
            LiveChartService = liveChartService;
            LiveChartService.SeriesCollection[0].Values.Clear();
            LiveChartService.SeriesCollection[1].Values.Clear();
            LiveChartService.Labels.Clear();
            LiveChartService.SeriesCollection = new SeriesCollection()
            {
                new LineSeries
                {
                    // có dấu cách cuối để legend trong live chart k bị dính viền
                    Title = "Nhiệt độ SP ",
                    Values = new ChartValues<double>{},
                    PointGeometry = null,
                },
                new LineSeries
                {
                    // có dấu cách cuối để legend trong live chart k bị dính viền
                    Title = "Nhiệt độ PV ",
                    Values = new ChartValues<double>{},
                    PointGeometry = null
                }
            };
            #endregion
            #region Command
            StartCommand = new RelayCommand(() =>
            {
                _confirmSettingViewModel.IsOpen = true;
                _confirmSettingViewModel.ConfirmAction += Start;
            });
            StopCommand = new RelayCommand(() =>
            {
                _confirmSettingViewModel.IsOpen = true;
                _confirmSettingViewModel.ConfirmAction += Stop;
            });
            ResetCommand = new RelayCommand(() =>
            {
                _confirmSettingViewModel.IsOpen = true;
                _confirmSettingViewModel.ConfirmAction += Reset;
            });
            #endregion
            _signalRService = signalRService;
            _confirmSettingViewModel = confirmSettingViewModel;
            _confirmSettingViewModel.CancelAction += CancelConfirm;
            _supervisorService = waterproofing;
            timer1 = new Timer()
            {
                Interval = 600000,
                Enabled = false
            };
            timer1.Elapsed += Timer1_Tick;
            timer1.Start();
            _supervisorService.DataUpdated += Update;

        }
        private void CancelConfirm(object? sender, EventArgs e)
        {
            _confirmSettingViewModel.ConfirmAction -= Stop;
            _confirmSettingViewModel.ConfirmAction -= Start;
            _confirmSettingViewModel.ConfirmAction -= Reset;

        }

        private void Timer1_Tick(object? sender, ElapsedEventArgs e)
        {
            canPlotingChart = true;
            
        }
        private void Stop(Object sender, EventArgs eventArgs)
        {
            _supervisorService.SendStatus("stop");
            UpdateIsCheckReport?.Invoke(false);
            _confirmSettingViewModel.ConfirmAction -= Stop;    

        }
        private void Start(Object sender, EventArgs eventArgs)
        {
            _supervisorService.SendStatus("start");
            UpdateIsCheckReport?.Invoke(true);
          //  UpdateDatabase?.Invoke();
            _confirmSettingViewModel.ConfirmAction -= Start;

        }
        private void Reset(Object sender, EventArgs eventArgs)
        {
            _supervisorService.SendStatus("reset");
            
            _confirmSettingViewModel.ConfirmAction -= Reset;
        }
        //public void InsertReport (PreReportWaterProofing preReportWaterProofing)
        //{
        //    _databaseService.InsertPreReportWaterProofing(preReportWaterProofing);
        //}
        //public bool IsRuning = false;
        //public bool IsCompleted = false;
        private async void Update(WaterProofingMachineMonitoringData monitoringData)
        {
            //IsCompleted=monitoringData.Complete_System;
            //if ( IsRuning==false&&IsCompleted==true )
            //{
            //    InsertReport((new PreReportWaterProofing( ) { DateTime=DateTime.Now,Temperature=monitoringData.TemperatureSP,Time=monitoringData.HourSP+monitoringData.MinuteSP/60 }));
            //    UpdateDatabase?.Invoke( );
            //}
            //IsRuning=IsCompleted;
            StartStatus = monitoringData.GreenLight;
            WarningStatus = monitoringData.RedLight;
            Temperature_PV = monitoringData.TemperaturePV;
            TemperatureDelay_PV=monitoringData.TemperatureDelay;
            // convert to string giờ : phút : giây ..
            // Tostring(00) format 2 chữ số
            TimeOperation = monitoringData.HourPV.ToString("00") + ":" + monitoringData.MinutePV.ToString("00") + ":" + monitoringData.SecondPV.ToString("00");
            #region Chart
            // Sau 1 phút thì mới cho vẽ
            if (canPlotingChart)
            {
                LiveChartService.SeriesCollection[0].Values.Add((double)monitoringData.TemperatureSP);
                LiveChartService.SeriesCollection[1].Values.Add(Math.Round(monitoringData.TemperaturePV,3));
                canPlotingChart=false;
            }
            #endregion

            var apisupervisor = new WaterProofingMonitorData
            {
                Status = monitoringData.GreenLight,
                Alarm = monitoringData.RedLight,
                TemperatureSp = monitoringData.TemperatureSP,
                HourSp = monitoringData.HourSP,
                MinuteSp = monitoringData.MinuteSP,
                //format giùm mobile
                TemperaturePv = Math.Round(monitoringData.TemperaturePV, 3),
                HourPv = monitoringData.HourPV,
                MinutePv = monitoringData.MinutePV,
                SecondPv = monitoringData.SecondPV,
            };
            var result = await _signalRService.WaterProofingMonitorData(apisupervisor);

        }
    }
}
