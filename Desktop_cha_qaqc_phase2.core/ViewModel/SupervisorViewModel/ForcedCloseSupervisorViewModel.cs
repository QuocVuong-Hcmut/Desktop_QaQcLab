using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.LOGO_;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel
{
    public class ForcedCloseSupervisorViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly ILogoForceCloseMachineService _supervisorService;
        private readonly ConfirmSettingViewModel _confirmSettingViewModel;
        private readonly ISignalRService _signalRService;

        public ConfirmSettingViewModel ConfirmSettingViewModel
        {
            get { return _confirmSettingViewModel; }
        }
        private int _timeCloseingPV;
        public int TimeClosingPV { get { return _timeCloseingPV; } set { _timeCloseingPV = value; OnPropertyChanged(); } }
        private int _timeOpenPV;

        public int TimeOpenPV { get { return _timeOpenPV; } set { _timeOpenPV = value; OnPropertyChanged(); } }
        private float _smoothTimeClosing;

        public float SmoothTimeClosing { get { return _smoothTimeClosing; } set { _smoothTimeClosing = value; OnPropertyChanged(); } }
        private int _numberClosingPV;

        public int NumberClosingPV { get { return _numberClosingPV; } set { _numberClosingPV = value; OnPropertyChanged(); } }
        private bool _supervisorStatusRun;

        public bool SupervisorStatusRun { get { return _supervisorStatusRun; } set { _supervisorStatusRun = value; OnPropertyChanged(); } }
        private bool _supervisorStatusWarn;

        public bool SupervisorStatusWarn { get { return _supervisorStatusWarn; } set { _supervisorStatusWarn = value; OnPropertyChanged(); } }
        private bool _supervisorStatusMode;

        public bool SupervisorStatusMode
        {
            get { return _supervisorStatusMode; }
            set { _supervisorStatusMode = value; }
        }
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }

        public ForcedCloseSupervisorViewModel(ILogoForceCloseMachineService logoModelingMachineService, ConfirmSettingViewModel confirmSettingViewModel, ISignalRService signalRService)
        {
            _supervisorService = logoModelingMachineService;
            _confirmSettingViewModel = confirmSettingViewModel;
            _confirmSettingViewModel.CancelAction += CancelConfirm;
            _signalRService = signalRService;

            //Command 
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);

            _supervisorService.DataUpdated += Update;

        }

        private void CancelConfirm(object? sender, EventArgs e)
        {
            _confirmSettingViewModel.ConfirmAction += StartConfirmAction;
            _confirmSettingViewModel.ConfirmAction += StopConfirmAction;
        }

        private void Start()
        {
            _confirmSettingViewModel.IsOpen = true;
            _confirmSettingViewModel.ConfirmAction += StartConfirmAction;
        }
        private void StartConfirmAction(object? sender, EventArgs e)
        {
            _supervisorService.SendStatus("start");
            _confirmSettingViewModel.ConfirmAction -= StartConfirmAction;

        }
        private void Stop()
        {
            _confirmSettingViewModel.IsOpen = true;
            _confirmSettingViewModel.ConfirmAction += StopConfirmAction;
        }

        private void StopConfirmAction(object? sender, EventArgs e)
        {
            _supervisorService.SendStatus("stop");
            _confirmSettingViewModel.ConfirmAction -= StopConfirmAction;


        }
        private async void Update(ForcedCloseMachineMonitoringData monitoringData)
        {
            TimeClosingPV = monitoringData.TimeClosePV;
            TimeOpenPV = monitoringData.TimeOpenPV;
            SmoothTimeClosing = (float)monitoringData.ClosingSmoothTime;
            NumberClosingPV = monitoringData.NumberOfClosingPV;
            SupervisorStatusRun = monitoringData.Run;
            SupervisorStatusWarn = monitoringData.Warn;
            SupervisorStatusMode = monitoringData.Mode;
            #region api
            var apisupervisor = new ForcedCloseMonitorData
            {
                NumberClosingSp = monitoringData.NumberOfClosingSP,
                NumberClosingPv = monitoringData.NumberOfClosingPV,
                TimeLidClose = monitoringData.TimeCloseSP,
                TimeLidOpen = monitoringData.TimeOpenSP,
                Status = monitoringData.Run,
                Alarm = monitoringData.Warn
            };
            var result = await _signalRService.ForcedCloseMonitoringData(apisupervisor);
            #endregion

        }

    }
}
