using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModel;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.HelpViewModel
{
    public class MainHelpViewModel : BaseViewModels.BaseViewModel
    {

        public ICommand SoftMachineCommand { get; set; }
        public ICommand ForceMachineCommand { get; set; }
        public ICommand EnduranceMachineCommand { get; set; }
        public ICommand WaterProofingMachineCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public ICommand PDFCommand { get; set; }
        public ICommand VideoCommand { get; set; }
        public ICommand DurationChangeCommand { get; set; }
        private int duration;
        public int Duration { get => duration; set { duration=value; OnPropertyChanged( ); } }
        private TimeSpan position;
        public TimeSpan Position { get => position; set { position=value; OnPropertyChanged( ); } }
        private string stateVideo;
        public string StateVideo { get => stateVideo; set { stateVideo=value; OnPropertyChanged( ); } }
        private string sourceVideo;
        public string SourceVideo { get => sourceVideo; set { sourceVideo=value; OnPropertyChanged( ); } }
        private string sourcePDF;
        public string SourcePDF { get => sourcePDF; set { sourcePDF=value; OnPropertyChanged( ); } }
        private bool isVideo;
        public bool IsVideo { get => isVideo; set { isVideo=value; OnPropertyChanged( ); } }
        private bool isPDF;
        public bool IsPDF { get => isPDF; set { isPDF=value; OnPropertyChanged( ); } }
        public MainHelpViewModel ( )
        {
            StopCommand=new RelayCommand(async ( ) => await Stop( ));
            StartCommand=new RelayCommand(async ( ) => await Start( ));
            PDFCommand=new RelayCommand(async ( ) => await PDF( ));
            VideoCommand=new RelayCommand(async ( ) => await Video( ));
            PauseCommand=new RelayCommand(async ( ) => await Pause( ));
            SoftMachineCommand=new RelayCommand(async ( ) => await ChangeSourceVideoSoft( ));
            ForceMachineCommand=new RelayCommand(async ( ) => await ChangeSourceVideoForce( ));
            EnduranceMachineCommand=new RelayCommand(async ( ) => await ChangeSourceVideoEndurance( ));
            WaterProofingMachineCommand=new RelayCommand(async ( ) => await ChangeSourceVideoWater( ));

            DurationChangeCommand=new RelayObjectCommand<System.Windows.Controls.MediaElement>((p) => { return true; },async (p) => DurationChange(p));
            StateVideo="Stop";
            IsPDF=true;
            IsVideo=false;
            SourceVideo=@".\MRE.mp4";
            SourcePDF=@".\Hướng Dẫn Vận Hành Ứng Dụng Máy Tính.docx";

        }
        private Task ChangeSourceVideoSoft ( )
        {

            SourceVideo=@".\MRE.mp4";
            Duration=0;
            StateVideo="Stop";
            return Task.CompletedTask;
        }
        private Task ChangeSourceVideoForce ( )
        {

            SourceVideo=@".\MCB.mp4";
            Duration=0;
            StateVideo="Stop";
            return Task.CompletedTask;
        }
        private Task ChangeSourceVideoEndurance ( )
        {

            SourceVideo=@".\MKTDBBD.mp4";
            Duration=0;
            StateVideo="Stop";
            return Task.CompletedTask;
        }
        private Task ChangeSourceVideoWater ( )
        {

            SourceVideo=@".\MKTCT.mp4";
            Duration=0;
            StateVideo="Stop";
            return Task.CompletedTask;
        }
        private Task Stop ( )
        {

            StateVideo="Stop";
            Duration=0;
            return Task.CompletedTask;
        }
        private Task DurationChange (System.Windows.Controls.MediaElement p)
        {
            (p as System.Windows.Controls.MediaElement).Position=TimeSpan.FromSeconds(Duration);

            return Task.CompletedTask;
        }
        private Task Start ( )
        {
            StateVideo="Play";
            Duration=0;
            return Task.CompletedTask;
        }
        private Task Pause ( )
        {
            StateVideo="Pause";

            return Task.CompletedTask;
        }
        private Task PDF ( )
        {
            IsPDF=true;
            IsVideo=false;
            return Task.CompletedTask;
        }
        private Task Video ( )
        {
            IsPDF=false;
            IsVideo=true;
            return Task.CompletedTask;
        }

    }
}
