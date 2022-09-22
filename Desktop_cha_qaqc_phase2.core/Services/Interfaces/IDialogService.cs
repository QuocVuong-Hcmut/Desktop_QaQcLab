using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.Services.Interfaces
{
    public interface IDialogService
    {
        public string Message { get; set; }
        public ICommand ConfirmCommand { get; set; }

        public event EventHandler ConfirmEvent;
        /// <summary>
        /// 1 là Notifycation
        /// 2 là Alert
        /// </summary>
        /// <param name="title"></param>
        /// <param name="mode"></param>
        public void ShowDialog(string message, int mode);
    }
}
