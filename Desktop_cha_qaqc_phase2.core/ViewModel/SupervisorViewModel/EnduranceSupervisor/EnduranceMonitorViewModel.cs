using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.PlcS71200;
using Desktop_cha_qaqc_phase2.Core.Services.Implement;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.SupervisorViewModel
{
     public class EnduranceMonitorViewModel : Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels.BaseViewModel
    {
        private  IS71200EnduranceMachineService _is71200ModellingMachineService;
        #region properties
        private int _compressionForce1;

        public int CompressionForce1
        {
            get { return _compressionForce1; }
            set
            {
                _compressionForce1 = value;
                OnPropertyChanged();
            }
        }
        private int _compressionForce2;

        public int CompressionForce2
        {
            get { return _compressionForce2; }
            set
            {
                _compressionForce2 = value;
                OnPropertyChanged();
            }
        }

        private int _compressionForce3;

        public int CompressionForce3
        {
            get { return _compressionForce3; }
            set
            {
                _compressionForce3 = value;
                OnPropertyChanged();
            }
        }
        private int _timeOccupying1;

        public int TimeOccupying1
        {
            get { return _timeOccupying1; }
            set
            {
                _timeOccupying1 = value;
                OnPropertyChanged();
            }
        }

        private int _timeOccupying2;

        public int TimeOccupying2
        {
            get { return _timeOccupying2; }
            set
            {
                _timeOccupying2 = value;
                OnPropertyChanged();
            }
        }
        private int _timeOccupying3;

        public int TimeOccupying3
        {
            get { return _timeOccupying3; }
            set
            {
                _timeOccupying3 = value;
                OnPropertyChanged();
            }
        }
        private int _numberClick1;

        public int NumberClick1
        {
            get { return _numberClick1; }
            set
            {
                _numberClick1 = value;
                OnPropertyChanged();
            }
        }
        private int _numberClick2;

        public int NumberClick2
        {
            get { return _numberClick2; }
            set
            {
                _numberClick2 = value;
                OnPropertyChanged();
            }
        }
        private int _numberClick3;


        public int NumberClick3
        {
            get { return _numberClick3; }
            set
            {
                _numberClick3 = value;
                OnPropertyChanged();
            }
        }

        private bool _system1;


        public bool System1
        {
            get { return _system1; }
            set
            {
                _system1 = value;
                OnPropertyChanged();
            }
        }

        private bool _system2;


        public bool System2
        {
            get { return _system2; }
            set
            {
                _system2 = value;
                OnPropertyChanged();
            }
        }
        #endregion
        private void UpdateView(EnduranceMachineMonitoringData monitordata)
        {
            CompressionForce1 = monitordata.Cylinder1ForceProcessValue;
            CompressionForce2 = monitordata.Cylinder2ForceProcessValue;
            CompressionForce3 = monitordata.Cylinder3ForceProcessValue;
            TimeOccupying1 = monitordata.HoldingTime1ProcessValue;
            TimeOccupying2 = monitordata.HoldingTime2ProcessValue;
            TimeOccupying3 = monitordata.HoldingTime3ProcessValue;
            NumberClick1 =monitordata.NumberOfPresses12SP - monitordata.NumberOfPresses1ProcessValue;
            NumberClick2 =monitordata.NumberOfPresses12SP - monitordata.NumberOfPresses2ProcessValue;
            NumberClick3 =monitordata.NumberOfPresses3SP- monitordata.NumberOfPresses3ProcessValue;
            System1 = monitordata.SelectSystem1;
            System2 = monitordata.SelectSystem2;
        }
        public EnduranceMonitorViewModel(S71200EnduranceMachineService is71200ModellingMachine)
        {
            _is71200ModellingMachineService = is71200ModellingMachine;
            _is71200ModellingMachineService.DataUpdated += UpdateView;


        }
    }
}
