using Desktop_cha_qaqc_phase2.core.Domain.Stores;
using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.SettingViewModel
{
    public class EnduranceSettingsViewModel: Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        #region private 
        private int SelectMode;

        private readonly IS71200EnduranceMachineService _s71200ModelingMachineService;
        private readonly IDatabaseService _databaseService;
        private ConfirmSettingViewModel _confirmSettingViewModel;
        #endregion 
        private int id;

        public int Id
        {
            get { return id; }
            set { id=value; }
        }
        public bool IsCurlingForced { get; set; }
        public bool IsStaticLoad { get; set; }
        public bool IsRockTest { get; set; }
        public bool IsRandomTest1 { get; set; }
        public bool IsRandomTest2 { get; set; }
        #region Command
        public ICommand EStaticLoadCommand { get; set; }
        public ICommand ERockTestcommand { get; set; }
        public ICommand ECurlingForceCommand { get; set; }
        public ICommand ERandomTest1Command { get; set; }
        public ICommand ERandomTest2Command { get; set; }

        //Control UI
        public ICommand ChangeSettingCommand { get; set; }

        public ICommand EditSettingCommand { get; set; }
        public ICommand ConfirmSettingCommand { get; set; }
        #endregion
        #region Properties to bind from ui 

        public ConfirmSettingViewModel ConfirmSettingViewModel
        {
            get { return _confirmSettingViewModel; }
            set { _confirmSettingViewModel=value; }
        }

        private bool _enableSetting;
        public bool EnableSetting
        {
            get { return _enableSetting; }
            set
            {
                _enableSetting=value;
                OnPropertyChanged( );
            }
        }
        private bool _system1;
        public bool System1
        {
            get { return _system1; }
            set
            {
                _system1=value;
                OnPropertyChanged( );
            }
        }

        private bool _system2;
        public bool System2
        {
            get { return _system2; }
            set
            {
                _system2=value;
                OnPropertyChanged( );
            }
        }

        private float _compressionForceSystem1;
        public float CompressionForceSystem1
        {
            get { return _compressionForceSystem1; }
            set
            {
                _compressionForceSystem1=value;
                OnPropertyChanged( );
            }
        }

        private float _compressionForceSystem2;
        public float CompressionForceSystem2
        {
            get { return _compressionForceSystem2; }
            set
            {
                _compressionForceSystem2=value;
                OnPropertyChanged( );
            }
        }
        private int _timeOccupying1;
        public int TimeOccupying1
        {
            get { return _timeOccupying1; }
            set
            {
                _timeOccupying1=value;
                OnPropertyChanged( );
            }
        }
        private int _timeOccupying2;
        public int TimeOccupying2
        {
            get { return _timeOccupying2; }
            set
            {
                _timeOccupying2=value;
                OnPropertyChanged( );
            }
        }

        private short _numberClick1;
        public short NumberClick1
        {
            get { return _numberClick1; }
            set
            {
                _numberClick1=value;
                OnPropertyChanged( );
            }
        }
        private short _numberClick2;
        public short NumberClick2
        {
            get { return _numberClick2; }
            set
            {
                _numberClick2=value;
                OnPropertyChanged( );
            }
        }
        #endregion        
        public EnduranceSettingsViewModel (
           S71200EnduranceMachineService s71200ModelingMachineService,IDatabaseService databaseservice,ConfirmSettingViewModel confimSettingViewModel)
        {
            _confirmSettingViewModel=confimSettingViewModel;
            _s71200ModelingMachineService=s71200ModelingMachineService;
            _databaseService=databaseservice;
            EnduranceSupervisorViewModel.UpdateDatabase += EnduranceSupervisorViewModel_UpdateDatabase;
            EStaticLoadCommand=new RelayCommand(EStaticLoadPara);
            ERockTestcommand=new RelayCommand(ERockTestLoadPara);
            ECurlingForceCommand=new RelayCommand(ECurlingTestLoadPara);
            ERandomTest1Command=new RelayCommand(ERandomTest1LoadPara);
            ERandomTest2Command=new RelayCommand(ERandomTest2LoadPara);
            //ControlCommand
            _confirmSettingViewModel.ConfirmAction+=ConfirmOperation;
            _confirmSettingViewModel.CancelAction+=CancelOperation;
            ConfirmSettingCommand=new RelayCommand(ConfirmSettingPara);
            EditSettingCommand=new RelayCommand(EditOperation);
            ChangeSettingCommand=new RelayCommand(
                ( ) =>
                {
                    EnableSetting
                    =true;
                });
            //setup
            EStaticLoadPara( );
            EnableSetting=false;
            //_s71200ModelingMachineService.UpdateData.Add(Update);
        }

        private void EnduranceSupervisorViewModel_UpdateDatabase()
        {

        }
        #region LoadParameter
        async void EStaticLoadPara ( )
        {
            IsCurlingForced=false;
            IsStaticLoad=true;
            IsRockTest=false;
            IsRandomTest1=false;
            IsRandomTest2=false;
            Id=1;
            var data = await _databaseService.LoadEnduranceSetting(1);
            System1=data.system12;
            System2=data.system3;
            NumberClick1=data.NumberClick12;
            NumberClick2=data.NumberClick3;
            TimeOccupying1=data.TimeOccupying12;
            TimeOccupying2=data.TimeOccupying3;
            CompressionForceSystem1=data.CompressionForceSettingsystem12;
            CompressionForceSystem2=data.CompressionForceSettingsystem3;
        }
        async void ECurlingTestLoadPara ( )
        {
            IsCurlingForced=true;
            IsStaticLoad=false;
            IsRockTest=false;
            IsRandomTest1=false;
            IsRandomTest2=false;
            Id=2;
            var data = await _databaseService.LoadEnduranceSetting(2);
            System1=data.system12;
            System2=data.system3;
            NumberClick1=data.NumberClick12;
            NumberClick2=data.NumberClick3;
            TimeOccupying1=data.TimeOccupying12;
            TimeOccupying2=data.TimeOccupying3;
            CompressionForceSystem1=data.CompressionForceSettingsystem12;
            CompressionForceSystem2=data.CompressionForceSettingsystem3;
        }
        async void ERockTestLoadPara ( )
        {
            IsCurlingForced=false;
            IsStaticLoad=false;
            IsRockTest=true;
            IsRandomTest1=false;
            IsRandomTest2=false;
            Id=3;
            var data = await _databaseService.LoadEnduranceSetting(3);
            System1=data.system12;
            System2=data.system3;
            NumberClick1=data.NumberClick12;
            NumberClick2=data.NumberClick3;
            TimeOccupying1=data.TimeOccupying12;
            TimeOccupying2=data.TimeOccupying3;
            CompressionForceSystem1=data.CompressionForceSettingsystem12;
            CompressionForceSystem2=data.CompressionForceSettingsystem3;
        }
        void ERandomTest1LoadPara ( )
        {
            IsCurlingForced=false;
            IsStaticLoad=false;
            IsRockTest=false;
            IsRandomTest1=true;
            IsRandomTest2=false;
            System1=false;
            System2=false;
            CompressionForceSystem1=0;
            CompressionForceSystem2=0;
            TimeOccupying1=0;
            TimeOccupying2=0;
            NumberClick1=0;
            NumberClick2=0;
        }
        void ERandomTest2LoadPara ( )
        {
            IsCurlingForced=false;
            IsStaticLoad=false;
            IsRockTest=false;
            IsRandomTest1=false;
            IsRandomTest2=true;
            System1=false;
            System2=false;
            CompressionForceSystem1=0;
            CompressionForceSystem2=0;
            TimeOccupying1=0;
            TimeOccupying2=0;
            NumberClick1=0;
            NumberClick2=0;
        }
        #endregion
        private void ConfirmOperation (object? sender,EventArgs e)
        {
            switch ( System1 )
            {
                case true:
                    {
                        switch ( System2 )
                        {
                            case true:
                                SelectMode=3;
                                break;
                            case false:
                                SelectMode=1;
                                break;
                        }
                    }
                    break;
                case false:
                    {
                        switch ( System2 )
                        {
                            case true:
                                SelectMode=2;
                                break;
                            case false:
                                SelectMode=4;
                                break;
                        }
                    }
                    break;
            }
            _s71200ModelingMachineService.SetMemmoryBit(0,7,true,SelectMode);
        }
        private void CancelOperation (object? sender,EventArgs e)
        {
            switch ( System1 )
            {
                case true:
                    {
                        switch ( System2 )
                        {
                            case true:
                                SelectMode=3;
                                break;
                            case false:
                                SelectMode=1;
                                break;
                        }
                    }
                    break;
                case false:
                    {
                        switch ( System2 )
                        {
                            case true:
                                SelectMode=2;
                                break;
                            case false:
                                SelectMode=4;
                                break;
                        }
                    }
                    break;
            }
            _s71200ModelingMachineService.SetMemmoryBit(0,7,false,SelectMode);
        }
        private void ConfirmSettingPara ( )
        {
            if ( System1 )
            {
                _s71200ModelingMachineService.SendataReal(CompressionForceSystem1,2);
                _s71200ModelingMachineService.SendTime(TimeOccupying1,14);
                _s71200ModelingMachineService.SendataUint(NumberClick1,10);
                foreach ( var item in _databaseService.LoadPreReportEndurance( ).Result )
                {
                    if ( item.IsReport )
                    {
                        _databaseService.DeletePreReportEndurance(item);
                    }
                }

            }
            if ( System2 )
            {
                foreach ( var item in _databaseService.LoadPreReportEndurance( ).Result )
                {
                    if ( item.IsReport )
                    {
                        _databaseService.DeletePreReportEndurance(item);
                    }
                }
                _s71200ModelingMachineService.SendataReal(CompressionForceSystem2,6);
                _s71200ModelingMachineService.SendTime(TimeOccupying2,18);
                _s71200ModelingMachineService.SendataUint(NumberClick2,12);


            }
            _confirmSettingViewModel.IsOpen=true;
        }
        private async void EditOperation ( )
        {
            if ( EnableSetting )

            {
                EnduranceSettingParameter enduranceSettingParameter = new EnduranceSettingParameter( )
                {
                    Id=id,
                    NumberClick12=_numberClick1,
                    NumberClick3=_numberClick2,
                    TimeOccupying3=_timeOccupying2,
                    TimeOccupying12=_timeOccupying1,
                    CompressionForceSettingsystem12=_compressionForceSystem1,
                    CompressionForceSettingsystem3=_compressionForceSystem2,
                    system12=_system1,
                    system3=_system2,
                };
                var Edit = await _databaseService.EditEnduranceSetting(enduranceSettingParameter);
                if ( Edit.Success==false )
                {
                    //_view.Alert("Lỗi khi chỉnh sửa thông số cài đặt máy cưỡng bức");
                }
                EnableSetting=false;
            }
        }
        //private void Update(bool data)
        //{
        //    if (_s71200ModelingMachineService.Start == true || _s71200ModelingMachineService.ErrorCode == 100)
        //    {
        //        EnableSetting = false;
        //    }
        //    else
        //    {
        //        EnableSetting = true;
        //    }
        //}

    }
}
