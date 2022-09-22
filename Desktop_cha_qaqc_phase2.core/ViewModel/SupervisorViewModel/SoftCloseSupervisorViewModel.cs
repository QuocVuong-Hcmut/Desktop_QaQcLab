using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Linq;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel
{
    public class SoftCloseSupervisorViewModel: Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly ILogoSoftCloseMachineService _supervisorService;
        private readonly IDatabaseService _databaseService;
        private readonly ISignalRService _signalRService;
        public ILiveChartService LiveChartService { get; set; }
        private readonly ConfirmSettingViewModel _confirmSettingViewModel;
        public ConfirmSettingViewModel ConfirmSettingViewModel { get => _confirmSettingViewModel; }
        private int _timeCloseingPV;

        public int TimeClosingPV { get { return _timeCloseingPV; } set { _timeCloseingPV=value; OnPropertyChanged( ); } }
        private int _timeOpenPV;

        public int TimeOpenPV { get { return _timeOpenPV; } set { _timeOpenPV=value; OnPropertyChanged( ); } }
        private float _smoothTimeClosing;

        public float SmoothTimeClosing { get { return _smoothTimeClosing; } set { _smoothTimeClosing=value; OnPropertyChanged( ); } }
        private float _smoothTimeClosingPlinth;

        public float SmoothTimeClosingPlinth
        {
            get => _smoothTimeClosingPlinth;
            set
            {
                _smoothTimeClosingPlinth=value;
            }
        }

        private int _numberClosingPV;

        public int NumberClosingPV { get { return _numberClosingPV; } set { _numberClosingPV=value; OnPropertyChanged( ); } }
        private bool _supervisorStatusRun;

        public bool SupervisorStatusRun { get { return _supervisorStatusRun; } set { _supervisorStatusRun=value; OnPropertyChanged( ); } }
        private bool _supervisorStatusWarn;

        public bool SupervisorStatusWarn { get { return _supervisorStatusWarn; } set { _supervisorStatusWarn=value; OnPropertyChanged( ); } }
        private bool _supervisorStatusMode;

        public bool SupervisorStatusMode
        {
            get { return _supervisorStatusMode; }
            set { _supervisorStatusMode=value; }
        }
        #region Commmand
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand ResetLiveChart { get; set; }
        #endregion
        public SoftCloseSupervisorViewModel (
            ILogoSoftCloseMachineService logoModelingMachineService1,
            ILiveChartService liveChartService,
            ConfirmSettingViewModel confimSettingViewModel,
            IDatabaseService databaseService,ISignalRService signalRService)
        {
            _confirmSettingViewModel=confimSettingViewModel;
            _confirmSettingViewModel.CancelAction+=CancelConfirm;

            LiveChartService=liveChartService;
            _supervisorService=logoModelingMachineService1;
            _databaseService=databaseService;
            _signalRService=signalRService;
            LoadPreviousChart( );
            //Command
            StartCommand=new RelayCommand(Start);
            StopCommand=new RelayCommand(Stop);
            _supervisorService.DataUpdated+=UpdateControlStatus;
            _supervisorService.ResetStatus+=ResetChart;
        }
        private async void LoadPreviousChart ( )
        {
            //Clear Chart
            LiveChartService.SeriesCollection[0].Values.Clear( );
            LiveChartService.SeriesCollection[1].Values.Clear( );
            LiveChartService.Labels.Clear( );
            //Load dữ liệu từ DB
            var data = await _databaseService.LoadSoftCloseTestReport( );
            if ( data!=null&&data.Count( )!=0 )
            {
                //foreach (var item in data)
                //{
                //    LiveChartService.SeriesCollection[0].Values.Add(item.FallTimeLid);
                //    LiveChartService.SeriesCollection[1].Values.Add(item.FallTimeRing);
                //    LiveChartService.Labels.Add(item.NumberOfClosing.ToString());
                //}
            }
        }
        private async void ResetChart ( )
        {
            LiveChartService.SeriesCollection[0].Values.Clear( );
            LiveChartService.SeriesCollection[1].Values.Clear( );
            LiveChartService.Labels.Clear( );
            //await _databaseService.ClearSoftCloseTestSample();

        }
        private void CancelConfirm (object? sender,EventArgs e)
        {
            _confirmSettingViewModel.ConfirmAction-=StartConfirmAction;
            _confirmSettingViewModel.ConfirmAction-=StopConfirmAction;
        }
        private void Start ( )
        {
            _confirmSettingViewModel.IsOpen=true;
            _confirmSettingViewModel.ConfirmAction+=StartConfirmAction;
        }
        private void StartConfirmAction (object? sender,EventArgs e)
        {
            _supervisorService.SendStatus("start");
            _confirmSettingViewModel.ConfirmAction-=StartConfirmAction;

        }
        private void Stop ( )
        {
            _confirmSettingViewModel.IsOpen=true;
            _confirmSettingViewModel.ConfirmAction+=StopConfirmAction;
        }
        private void StopConfirmAction (object? sender,EventArgs e)
        {
            _supervisorService.SendStatus("stop");
            _confirmSettingViewModel.ConfirmAction-=StopConfirmAction;

        }
        private async void UpdateControlStatus (SoftCloseMachineMonitoringData monitoringData)
        {

            #region Chart và insert db
            if
            ( monitoringData.NumberOfClosingPV!=0&&
            monitoringData.SmoothTimeClosing!=0&&
            monitoringData.SmoothTimeClosingPlinth!=0&&
            monitoringData.NumberOfClosingPV%200==0&& // lấy mẫu chu kì 20 lần
            monitoringData.Run&&
            (NumberClosingPV!=monitoringData.NumberOfClosingPV
            /*monitoringData.NumberOfClosingSP == monitoringData.NumberOfClosingPV)*/) )
            {
                LiveChartService.SeriesCollection[0].Values.Add(Math.Round(monitoringData.SmoothTimeClosing,3));
                LiveChartService.Labels.Add(monitoringData.NumberOfClosingPV.ToString( ));
                LiveChartService.SeriesCollection[1].Values.Add(Math.Round(monitoringData.SmoothTimeClosingPlinth,3));
                SoftCloseTestSample sheet = new SoftCloseTestSample( )
                {
                    NumberOfClosing=monitoringData.NumberOfClosingPV,
                    FallTimeLid=Math.Round(monitoringData.SmoothTimeClosing,3),
                    FallTimeRing=Math.Round(monitoringData.SmoothTimeClosingPlinth,3),
                };
                

            }
            if
                (   monitoringData.NumberOfClosingPV!=0&&
                    monitoringData.SmoothTimeClosing!=0&&
                    monitoringData.SmoothTimeClosingPlinth!=0&&
                    monitoringData.NumberOfClosingPV%1==0&& // lấy mẫu chu kì 20 lần
                    monitoringData.Run&&
                    (NumberClosingPV!=monitoringData.NumberOfClosingPV
) )
            {

                SoftCloseTestSample sheet = new SoftCloseTestSample( )
                {
                    NumberOfClosing=monitoringData.NumberOfClosingPV,
                    FallTimeLid=Math.Round(monitoringData.SmoothTimeClosing,3),
                    FallTimeRing=Math.Round(monitoringData.SmoothTimeClosingPlinth,3),
                };
                _databaseService.InsertSoftCloseReport(sheet);

            }

            #endregion
            #region Update parameter
            TimeClosingPV=monitoringData.TimeClosePV;
            TimeOpenPV=monitoringData.TimeOpenPV;
            SmoothTimeClosing=monitoringData.SmoothTimeClosing;
            SmoothTimeClosingPlinth=monitoringData.SmoothTimeClosingPlinth;
            NumberClosingPV=monitoringData.NumberOfClosingPV;
            //Cap nhat status 
            SupervisorStatusRun=monitoringData.Run;
            SupervisorStatusWarn=monitoringData.Warn;
            SupervisorStatusMode=monitoringData.Mode;
            #endregion
            #region signal R

            var apisupervisor = new SoftCloseMonitorData
            {
                NumberClosingSp=monitoringData.NumberOfClosingSP,
                NumberClosingPv=monitoringData.NumberOfClosingPV,
                TimeLidClose=monitoringData.TimeCloseSP,
                TimeLidOpen=monitoringData.TimeOpenSP,
                Running=monitoringData.Run,
                Alarm=monitoringData.Warn
            };
            //Thread.Sleep(100);
            await _signalRService.SoftCloseMonitoringData(apisupervisor);
            #endregion}
        }
    }
}
