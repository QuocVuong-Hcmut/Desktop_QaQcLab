using Desktop_cha_qaqc_phase2.Core.Commands;
using Desktop_cha_qaqc_phase2.Core.Dialog;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.Services.Implement
{
    public class DialogService : BaseViewModel,IDialogService
    {
        public string Message { get; set; } = "Lorem ipsum donor";
        public int Mode { get; set; } 

        public ICommand ConfirmCommand 
        { get; 
            set; }

        public event EventHandler ConfirmEvent;
        public DialogService()
        {
            ConfirmCommand = new RelayCommand(() =>
            {
                ConfirmEvent?.Invoke(this, new EventArgs());
            });
        }
        /// <summary>
        /// Mode 1 = Notify,
        /// Mode 2 = Alert
        /// </summary>
        /// <param name="message"></param>
        /// <param name="mode"></param>
        public void ShowDialog(string message, int mode)
        {
            Message = message;
            if (mode == 1)
            {
                Thread thread = new Thread(() =>
                {
                   NotifycationView view = new NotifycationView();
                    view.Message.Text = message;
                    view.ResizeMode = System.Windows.ResizeMode.NoResize;
                    view.Height = 180;
                    view.Width = 700;
                    view.ShowDialog();
                    
                });
                thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
                thread.Start();
            }
            else if (mode == 2)
            {
                Thread thread = new Thread(() =>
                {
                    AlertView view= new AlertView();
                    view.Message.Text = message;
                    view.ResizeMode = System.Windows.ResizeMode.NoResize;
                    view.Height = 180;
                    view.Width = 700;
                    view.ShowDialog();
                });
                thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
                thread.Start();
            }
        }
        public class RemoveWarningEvent : EventArgs
        {


        }
        //public enum Mode
        //{
        //    Notify,
        //    Alert
        //}
    }
}
