﻿using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces
{
    public interface ICurlingForceConfigurationRepository
    {
        Task InsertConfiguration(CurlingForceTest entry);
        Task UpdateConfiguration(CurlingForceTest entry);
        Task<CurlingForceTest> LoadConfiguration();    
        Task ClearConfigurationAsync();
    }
}
