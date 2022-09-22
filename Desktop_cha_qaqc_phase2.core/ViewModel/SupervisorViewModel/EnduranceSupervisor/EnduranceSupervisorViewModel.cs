using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel
{
    public class EnduranceSupervisorViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private readonly S71200EnduranceMachineService _supervisorService;
        private readonly NavigationStore _navigationStore;
        private readonly ConfirmSettingViewModel _confirtSettingViewModel;
        private readonly ISignalRService _signalRService;
        private IDatabaseService _databaseService;
        public ConfirmSettingViewModel ConfirmSettingViewModel { get { return _confirtSettingViewModel; } }
        public Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand SettingCommand { get; set; }
        public ICommand SupervisorCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand CancelSetting { get; set; }
        // Mở trang para trước
        public bool IsParaSelected { get; set; } = false;
        public bool IsMonitorSelected { get; set; } = true;
        public static event Action UpdateDatabase;
        #region properties
        private bool _connectFlag;
        public bool ConnectFlag
        {
            get => _connectFlag;
            set
            {
                _connectFlag = value;
            }
        }

        private bool _startStatus;
        public bool StartStatus
        {
            get => _startStatus;
            set
            {
                _startStatus = value;
            }
        }

        private int _modeStatus;
        public int ModeStatus
        {
            get => _modeStatus;
            set
            {
                _modeStatus = value;
            }
        }

        private bool _warning;
        public bool Warning
        {
            get => _warning;
            set
            {
                _warning = value;
            }
        }

        #endregion
        public EnduranceSupervisorViewModel(NavigationStore navigationStore,
           INavigationService _SettingnavigationService,
           INavigationService _SupervisornavigationService,
           S71200EnduranceMachineService s71200ModellingMachineService,
            ConfirmSettingViewModel confirmSettingViewModel,ISignalRService signalRService,IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _confirtSettingViewModel=confirmSettingViewModel;
            _confirtSettingViewModel.CancelAction+=CancelConfirm;
            _supervisorService=s71200ModellingMachineService;
            _supervisorService.DataUpdated+=Update;
            _signalRService=signalRService;
            _navigationStore=navigationStore;
            SettingCommand=new NavigateCommand(_SettingnavigationService);
            SupervisorCommand=new NavigateCommand(_SupervisornavigationService);
            //Control Command
            StartCommand=new RelayCommand(Start);
            StopCommand=new RelayCommand(Stop);
            ResetCommand=new RelayCommand(Reset);
            _navigationStore.CurrentViewModelChanged+=OnCurrentViewModelChanged;
            _databaseService=databaseService;
        }

        private void CancelConfirm(object? sender, EventArgs e)
        {
            _confirtSettingViewModel.ConfirmAction -= ConfirmAction;
        }

        private void Start()
        {
            _supervisorService.SendStatus("Start");
            _confirtSettingViewModel.ConfirmAction += ConfirmAction;
            _confirtSettingViewModel.IsOpen = true;
            UpdateDatabase?.Invoke();
        }

        private void Stop()
        {
            _supervisorService.SendStatus("Stop");
            _confirtSettingViewModel.IsOpen = true;
            _confirtSettingViewModel.ConfirmAction += ConfirmAction;

        }
        private void Reset()
        {
            _supervisorService.SendStatus("Reset");
            _confirtSettingViewModel.IsOpen = true;
            _confirtSettingViewModel.ConfirmAction += ConfirmAction;
        }
        private void ConfirmAction(object? sender, EventArgs e)
        {
            _supervisorService.SendStatus("confirm");
            _confirtSettingViewModel.ConfirmAction -= ConfirmAction;

        }
        //public void InsertReport (PreReportEndurance preReportEndurance)
        //{
        //    _databaseService.InsertPreReportEndurance(preReportEndurance);

        //}
    //    public bool IsRuning = false;
    //    public bool IsCompleted = false;
    //    IsCompleted=!monitoringData.Start;
    //        if (IsRuning==false&&IsCompleted==true )
    //        {
    //            InsertReport ((new PreReportEndurance ( )
    //    {
    //        DateTime=DateTime.Now,Force1=monitoringData.Cylinder12ForceSP,Force2=monitoringData.Cylinder3ForceSP,
    //                Time2=monitoringData.HoldingTime3SP, Time1=monitoringData.HoldingTime12SP , Number1=monitoringData.NumberOfPresses12SP, Number2=monitoringData.NumberOfPresses3SP}));
    //            UpdateDatabase?.Invoke ( );
    //}
    //IsRuning=IsCompleted;
        private async void Update(EnduranceMachineMonitoringData monitoringData)
        {

            ConnectFlag = true;
            StartStatus = monitoringData.Start;
            ModeStatus = monitoringData.Mode;
            Warning = monitoringData.Warning;
            #region api

            var apisupervisor = new EnduranceMonitorData
            {
                NumberOfTestPv1 = monitoringData.NumberOfPresses1ProcessValue,
                NumberOfTestPv2 = monitoringData.NumberOfPresses2ProcessValue,
                NumberOfTestPv3 = monitoringData.NumberOfPresses3ProcessValue,
                NumberOfTestSp12 = monitoringData.NumberOfPresses12SP,
                NumberOfTestSp3 = monitoringData.NumberOfPresses3SP,
                ErrorCode = monitoringData.ErrorCode,
                ModeStatus = monitoringData.Mode,
                ForceCylinderSp12 = monitoringData.Cylinder12ForceSP,
                ForceCylinderSp3 = monitoringData.Cylinder3ForceSP,
                TimeHoldSp12 = monitoringData.HoldingTime12SP,
                TimeHoldSp3 = monitoringData.HoldingTime3SP,
                Seclect1 = monitoringData.SelectSystem1,
                Seclect2 = monitoringData.SelectSystem2,
                RedStatus = (monitoringData.Warning),
                GreenStatus = monitoringData.Start,
                ErrorStatus = monitoringData.ErrorStatus
            };
            var result = await _signalRService.EnduranceMonitoringData(apisupervisor);

            #endregion
        }
        private void OnCurrentViewModelChanged()
        {
            IsParaSelected = false;
            IsMonitorSelected = false;
            if (CurrentViewModel is EnduranceParaViewModel) IsParaSelected = true;
            if (CurrentViewModel is EnduranceMonitorViewModel) IsMonitorSelected = true;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
