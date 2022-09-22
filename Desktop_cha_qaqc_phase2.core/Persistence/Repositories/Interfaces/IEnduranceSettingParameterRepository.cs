using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces
{
    public interface IEnduranceSettingParameterRepository
    {
        void UpdateAsync(EnduranceSettingParameter entry);
        EnduranceSettingParameter Load(int ID);
        Task<IList<PreReportEndurance>> LoadPreReport ( );
        Task<ServiceResponse> ClearPreReportAsync ( );
        Task InsertPreReportAsync (PreReportEndurance entry);
    }
}
