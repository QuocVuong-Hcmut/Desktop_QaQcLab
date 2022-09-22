
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces
{
    public interface ISoftCloseConfigurationRepository
    {
        Task InsertAsync(SoftCloseTest entry);
        Task ClearAsync();
        Task<SoftCloseTest> LoadConfigurationsAsync();
        Task UpdateConfiguration(SoftCloseTest config);
    }
}
