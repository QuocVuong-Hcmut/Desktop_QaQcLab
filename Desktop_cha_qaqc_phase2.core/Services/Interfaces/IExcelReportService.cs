
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Services.Interfaces
{
    public interface IExcelExportService
    {
        Task<ServiceResponse> ExportSoftClose(string path, SoftCloseTest report, bool mode);
        Task<ServiceResponse> ExportDeformation(string path, ForcedCloseTest report);
        Task<ServiceResponse> ExportCurlingForce(string path, CurlingForceTest report);
        Task<ServiceResponse> ExportStaticLoad(string path, StaticLoadTest report);
        Task<ServiceResponse> ExportRockTest(string path, RockTest report);
        Task<ServiceResponse> ExportWaterProofing(string path, WaterProofingTest report);
    }
}
