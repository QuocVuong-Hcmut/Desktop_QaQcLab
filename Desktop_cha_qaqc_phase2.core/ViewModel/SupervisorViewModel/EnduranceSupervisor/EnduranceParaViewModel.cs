using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using Desktop_cha_qaqc_phase2.Core.ViewModel.SettingViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel
{

    public class EnduranceParaViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private IS71200EnduranceMachineService _is71200ModellingMachineService;
        #region properties
        private int _compressionForce1;

        public int CompressionForce1
        {
            get { return _compressionForce1; }
            set 
            { _compressionForce1 = value;
           
            }
        }

        private int _compressionForce2;

        public int CompressionForce2
        {
            get { return _compressionForce2; }
            set 
            {
                _compressionForce2 = value;
             
            }
        }

        private int _compressionForce3;

        public int CompressionForce3
        {
            get { return _compressionForce3; }
            set 
            { 
                _compressionForce3 = value;
           
            }
        }
        private int _timeOccupying1;

        public int TimeOccupying1
        {
            get { return _timeOccupying1; }
            set
            {
                _timeOccupying1 = value;
            }
        }

        private int _timeOccupying2;

        public int TimeOccupying2
        {
            get { return _timeOccupying2; }
            set
            {
                _timeOccupying2 = value;
            }
        }
        private int _timeOccupying3;

        public int TimeOccupying3
        {
            get { return _timeOccupying3; }
            set
            {
                _timeOccupying3 = value;
            }
        }
        private int _numberClick1;

        public int NumberClick1
        {
            get { return _numberClick1; }
            set
            {
                _numberClick1 = value;
            }
        }
        private int _numberClick2;

        public int NumberClick2
        {
            get { return _numberClick2; }
            set
            {
                _numberClick2 = value;
            }
        }
        private int _numberClick3;


        public int NumberClick3
        {
            get { return _numberClick3; }
            set
            {
                _numberClick3 = value;
            }
        }

        private bool _system1;


        public bool System1
        {
            get { return _system1; }
            set
            {
                _system1 = value;
                if (!_system1)
                {
                    NumberClick1 = 0;
                    NumberClick2 = 0;
                    CompressionForce1 = 0;
                    CompressionForce2 = 0;
                    TimeOccupying1 = 0;
                    TimeOccupying2 = 0;
                }
            }
        }

        private bool _system2;


        public bool System2
        {
            get { return _system2; }
            set
            {
                _system2 = value;
                if (!_system2)
                {
                    NumberClick3 = 0;
                    TimeOccupying3= 0;
                    CompressionForce3 = 0;
                }
            }
        }
        #endregion


        private void Update(EnduranceMachineMonitoringData monitoringData)
        {
            CompressionForce1 = monitoringData.Cylinder12ForceSP;
            CompressionForce2 = monitoringData.Cylinder12ForceSP;
            CompressionForce3 = monitoringData.Cylinder3ForceSP;
            TimeOccupying1 = monitoringData.HoldingTime12SP;
            TimeOccupying2 = monitoringData.HoldingTime12SP;
            TimeOccupying3 = monitoringData.HoldingTime3SP;
            NumberClick1 =monitoringData.NumberOfPresses12SP;
            NumberClick2 = monitoringData.NumberOfPresses12SP;
            NumberClick3 = monitoringData.NumberOfPresses3SP;
            System1 = monitoringData.SelectSystem1;
            System2 = monitoringData.SelectSystem2;
        }
        public EnduranceParaViewModel(S71200EnduranceMachineService s71200ModellingMachine )
        {
            
            _is71200ModellingMachineService = s71200ModellingMachine;
            _is71200ModellingMachineService.DataUpdated += Update;

        }

    }
}
