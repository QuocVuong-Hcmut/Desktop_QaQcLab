 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Desktop_cha_qaqc_phase2.Core.Domain.Communication.WebApi.DataContractAttribute;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System.Collections.ObjectModel;

namespace Desktop_cha_qaqc_phase2.Core.Services.Interfaces
{
    public interface IDatabaseService
    {
        //SoftClose
        //Config
        Task<ServiceResponse> UpdateSoftCloseConfiguration(SoftCloseTest softCloseTest);
        Task<ServiceResponse> InsertSoftCloseConfiguration(SoftCloseTest softCloseTest);
        Task<SoftCloseTest> LoadSoftCloseConfiguration();
        //Report
        Task<ServiceResponse> UpdateSoftCloseReportByListSample(IList<SoftCloseTestSample> listReport);
        Task<ServiceResponse> InsertSoftCloseReport(SoftCloseTestSample entry);
        Task<IEnumerable<SoftCloseTestSample>> LoadSoftCloseTestReport();
        Task<IEnumerable<SoftCloseTestSample>> LoadSoftCloseReportBySample(List<int> samples);
        Task<ServiceResponse> ClearSoftCloseTestSample();
        //ForcedClose
        Task<ServiceResponse> DeletePreReportForcedClose (PreForceClose preForceClose =null );
        Task<ServiceResponse> InsertPreReportForcedClose (PreForceClose entry);
        Task<ServiceResponse> UpdatePreReportForcedClose (PreForceClose entry);
        Task<IList<PreForceClose>> LoadPreReportForcedClose ( );
        //Config
        Task<ServiceResponse> InsertForcedCloseConfiguration(ForcedCloseTest testingConfigurations);
        Task<ServiceResponse> UpdateForcedCloseConfiguration(ForcedCloseTest testingConfigurations);
        Task<ForcedCloseTest> LoadForcedCloseConfiguration();
        //Report
        Task<ServiceResponse> InsertFocedCloseReport(ForcedCloseTestSample entry);
        Task<IEnumerable<ForcedCloseTestSample>> LoadFocedCloseReport();
        Task<ServiceResponse> ClearForcedCloseSheet();
        Task<ServiceResponse> UpdateForcedCloseReport(IEnumerable<ForcedCloseTestSample> listReport);
        //Endurance Setting 
        Task<ServiceResponse> DeletePreReportEndurance (PreReportEndurance preReportEndurance=null );
        Task<ServiceResponse> InsertPreReportEndurance (PreReportEndurance entry);
        Task<ServiceResponse> UpdatePreReportEndurance (PreReportEndurance entry);
        Task<IList<PreReportEndurance>> LoadPreReportEndurance ( );
        Task<ServiceResponse> EditEnduranceSetting(EnduranceSettingParameter entry);
        Task<EnduranceSettingParameter> LoadEnduranceSetting(int ID);
        //StaticLoad
        //Config
        Task<ServiceResponse> UpdateStaticLoadConfiguration(StaticLoadTest entry);
        Task<ServiceResponse> InsertStaticLoadConfiguration(StaticLoadTest entry);
        Task<StaticLoadTest> LoadStaticLoadConfiguration();
        //Report
        Task<ServiceResponse> UpdateCurlingForceReport(ObservableCollection<StaticLoadTestSample> entry);
        Task<ServiceResponse> InsertStaticLoadTestSample(StaticLoadTestSample report);
        Task<ServiceResponse> ClearStaticLoad();
        Task<IList<StaticLoadTestSample>> LoadStaticLoadReport();

        //CurlingForce
        //Config
        Task<ServiceResponse> UpdateCurlingForceConfiguration(CurlingForceTest entry);
        Task<ServiceResponse> InsertCurlingForceConfiguration(CurlingForceTest entry);

        Task<CurlingForceTest> LoadCurlingForceConfiguration();
        //Report
        Task<ServiceResponse> InsertCurlingForceTestSample(ObservableCollection<CurlingForceTestSample> report);
        Task<ServiceResponse> ClearCurlingForceTestSample();
        Task<IList<CurlingForceTestSample>> LoadCurlingForceTestSample();
        Task<ServiceResponse> UpadateCurlingForceReport(ObservableCollection<CurlingForceTestSample> listReport);

        //RockTest
        //Config
        Task<ServiceResponse> UpdateRockTestConfiguration(RockTest entry);
        Task<ServiceResponse> InsertRockTestConfiguration(RockTest entry);

        Task<RockTest> LoadRockTestConfiguration();
        //Report
        Task<ServiceResponse> UpdateRockTestSample(ObservableCollection<RockTestSample> listReport);
        Task<ServiceResponse> InsertRockTestSample(RockTestSample report);
        Task<ServiceResponse> ClearRockTestSample();
        Task<IEnumerable<RockTestSample>> LoadRockTestSample();
        //WaterProofing
        Task<ServiceResponse> DeletePreReportWaterProofing (PreReportWaterProofing preReportWaterProofing=null);
        Task<ServiceResponse> InsertPreReportWaterProofing (PreReportWaterProofing entry );
        Task<ServiceResponse> UpdatePreReportWaterProofing(PreReportWaterProofing entry );
        Task<IList<PreReportWaterProofing>> LoadPreReportWaterProofing ( );
        //Config
        Task<ServiceResponse> UpdateWaterProofingConfiguration(WaterProofingTest entry);
        Task<ServiceResponse> InsertWaterProofingConfiguration(WaterProofingTest entry);
        Task<WaterProofingTest> LoadWaterProofingConfiguration();
        //Report
        Task<ServiceResponse> UpdateWaterProofingReport(IList<WaterProofingTestSample> entry);
        Task<IEnumerable<WaterProofingTestSample>> LoadWaterProofingReportAsync();
        //HistoryReliability
        Task<ServiceResponse> InsertHistoryReliability(HistorySoftCloseReport report);
        Task<IList<HistorySoftCloseReport>> LoadHistoryReliability(DateTime timestart, DateTime timestop);
        //HistoryDeformation
        Task<ServiceResponse> InsertHistoryDeformation(HistoryForcedCloseReport report);
        Task<IList<HistoryForcedCloseReport>> LoadHistoryDeformation(DateTime timestart, DateTime timestop);
        //HistoryEndurance
        Task<ServiceResponse> InsertHistoryEndurance(HistoryEnduranceReport report);
        Task<IList<HistoryEnduranceReport>> LoadHistoryEndurance(DateTime timestart, DateTime timestop);
        //History waterproofing
        Task<ServiceResponse> InsertHistoryWaterProofing(HistoryWaterProofingReport historySoftCloseReport);
        Task<IList<HistoryWaterProofingReport>> LoadHistoryWaterProofing(DateTime timestart, DateTime timestop);

    }
}
