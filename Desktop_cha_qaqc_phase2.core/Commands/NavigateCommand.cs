using Desktop_cha_qaqc_phase2.core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desktop_cha_qaqc_phase2.Core.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
