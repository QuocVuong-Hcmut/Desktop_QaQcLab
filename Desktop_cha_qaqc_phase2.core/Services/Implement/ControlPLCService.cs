using Desktop_cha_qaqc_phase2.Core.Service.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop_cha_qaqc_phase2.Core.Services.Implement
{
    public class ControlPlcService
    {
        public readonly IS71200EnduranceService _endurance;
        public readonly IS71200WaterProofingService _waterproofing;
        public readonly ILogoSoftCloseService _softclose;
        public readonly ILogoForceCloseService _forcedclose;
        private readonly System.Timers.Timer _timer;
        public ControlPlcService(S71200Endurance endurance, S71200WaterProofing waterProofing, ILogoSoftCloseService reliability, ILogoForceCloseService deformation)
        {
            _endurance = endurance;
            _softclose = reliability;
            _forcedclose = deformation;
            _waterproofing = waterProofing;
            _timer = new System.Timers.Timer
            {
                Interval = 900,
                Enabled = true
            };
            _timer.Enabled = true;
            _timer.Elapsed += Timer_Tick;
        }
        private async void Timer_Tick(object sender, EventArgs args)
        {
            Application.Current.Dispatcher.Invoke(new Action(async ( ) =>
            {
                await _softclose.ReadData( );
                await _endurance.ReadData( );
                await _waterproofing.ReadData( );
                await _forcedclose.ReadData( );
            }));
        }
    }
}
