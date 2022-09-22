using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using ProductVertificationDesktopApp.Core.Domain.Communication;

namespace Desktop_cha_qaqc_phase2.Core.Services.Interfaces
{

    public interface IApiService
    {
        #region SoftCLose
        Task<ServiceResponse> PostSoftCloseReport(SoftCloseTestPOST settingMachine);
        Task<ServiceResourceResponse<QueryResult<SoftCloseTestGET>>> GetSoftCloseReport(DateTime? startTime, DateTime? stopTime);
        #endregion
        #region ForcedClose
        Task<ServiceResponse> PostForcedCloseReport(ForcedCloseTestPOST settingMachine);
        Task<ServiceResourceResponse<QueryResult<ForcedCloseTestGET>>> GetForcedCloseReport(DateTime? startTime, DateTime? stopTime);
        #endregion
        #region StaticLoad
        Task<ServiceResponse> PostStaticLoad(StaticLoadTestPOST report);
        Task<ServiceResourceResponse<QueryResult<StaticLoadTestGET>>> GetStaticLoadReport(DateTime? startTime, DateTime? stopTime);
        #endregion
        #region CurlingForce
        Task<ServiceResponse> PostCurlingForce(CurlingForceTestPOST report);
        Task<ServiceResourceResponse<QueryResult<CurlingForceTestGET>>> GetCurlingForceReport(DateTime? startTime, DateTime? stopTime);
        #endregion
        #region RockTest
        Task<ServiceResponse> PostRockTest(RockTestPOST report);
        Task<ServiceResourceResponse<QueryResult<RockTestGET>>> GetRockTestReport(DateTime? startTime, DateTime? stopTime);
        #endregion
        #region WaterProofing
        Task<ServiceResponse> PostWaterProofing(WaterProofingTestPOST report);
        Task<ServiceResourceResponse<QueryResult<WaterProofingTestGET>>> GetWaterProofingReport(DateTime? startTime, DateTime? stopTime);
        #endregion
        #region Warning
        Task<ServiceResponse> PostSoftCloseWarning(AlarmCode warningcode);
        Task<ServiceResponse> PostForcedCloseWarning(AlarmCode warningcode);
        Task<ServiceResponse> PostEnduranceWarning(AlarmCode warningcode);
        Task<ServiceResponse> PostWaterProofingWarning(AlarmCode warningcode);
        Task<ServiceResourceResponse<QueryResult<AlarmCode>>> GetWarning(DateTime? startTime, DateTime? stopTime, string option);
        #endregion
        #region ProductID
        Task<ServiceResponse> PostProduct(Product product);
        Task<ServiceResourceResponse<ObservableCollection<Product>>> GetProduct();
        Task<ServiceResponse> DeleteProductByID(Product product);

        #endregion
        #region TesterID
        Task<ServiceResourceResponse<ObservableCollection<Tester>>> GetTester();
        #endregion
    }
}
