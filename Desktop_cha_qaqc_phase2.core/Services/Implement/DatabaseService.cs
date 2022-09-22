
using ProductVertificationDesktopApp.Core.Domain;
using ProductVertificationDesktopApp.Core.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Desktop_cha_qaqc_phase2.Core.Services.Interfaces;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Microsoft.EntityFrameworkCore;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories;
using System.Collections.ObjectModel;
using Desktop_cha_qaqc_phase2.Core.Persistence.Repositories.Interfaces;

namespace Desktop_cha_qaqc_phase2.Core.Services.Implement
{
    public class DatabaseService : IDatabaseService
    {

        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public DatabaseService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        #region SoftCloseConfiguration
        public async Task<SoftCloseTest> LoadSoftCloseConfiguration()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var softCloseConfigurationRepository = new SoftCloseConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                return await softCloseConfigurationRepository.LoadConfigurationsAsync();
            }
        }
        public async Task<ServiceResponse> UpdateSoftCloseConfiguration(SoftCloseTest softCloseTest)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var softCloseConfigurationRepository = new SoftCloseConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await softCloseConfigurationRepository.UpdateConfiguration(softCloseTest);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {

                    var error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> InsertSoftCloseConfiguration(SoftCloseTest softCloseTest)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var softCloseConfigurationRepository = new SoftCloseConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await softCloseConfigurationRepository.InsertAsync(softCloseTest);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {

                    var error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        #endregion
        #region SoftCloseReport
        public async Task<ServiceResponse> UpdateSoftCloseReportByListSample(IList<SoftCloseTestSample> list)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _reliabilitytestingsheetRepository = new SoftCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _reliabilitytestingsheetRepository.UpdateByNumClosing(list);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {

                    Error error = new Error
                    {
                        ErrorCode = "Database.Insert",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> InsertSoftCloseReport(SoftCloseTestSample entry)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _reliabilitytestingsheetRepository = new SoftCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _reliabilitytestingsheetRepository.InsertAsync(entry);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {

                    Error error = new Error
                    {
                        ErrorCode = "Database.Insert",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<IEnumerable<SoftCloseTestSample>> LoadSoftCloseTestReport()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                ISoftCloseReportRepository _reliabilitytestingsheetRepository = new SoftCloseReportRepository(context);
                return await _reliabilitytestingsheetRepository.LoadAsync();
            }
        }
        public async Task<IEnumerable<SoftCloseTestSample>> LoadSoftCloseReportBySample(List<int> samples)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _reliabilitytestingsheetRepository = new SoftCloseReportRepository(context);
                return await _reliabilitytestingsheetRepository.GetAllByNumClosing(samples);
            }
        }
        public async Task<ServiceResponse> ClearSoftCloseTestSample()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var softCloseReportRepository = new SoftCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);

                try
                {
                    await softCloseReportRepository.ClearAsync();
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode = "Database.Clear",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        #endregion
        #region ForcedCloseConfiguration
        public async Task<ServiceResponse> UpdateForcedCloseConfiguration(ForcedCloseTest forcedCloseTest)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var forcedCloseConfigurationRepository = new ForcedCloseConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await forcedCloseConfigurationRepository.UpdateConfiguration(forcedCloseTest);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ForcedCloseTest> LoadForcedCloseConfiguration()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var forcedCloseConfigurationRepository = new ForcedCloseConfigurationRepository(context);
                return await forcedCloseConfigurationRepository.LoadConfigurationAsync();
            }
        }
        public async Task<ServiceResponse> InsertForcedCloseConfiguration(ForcedCloseTest softCloseTest)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var forcedCloseConfigurationRepository = new ForcedCloseConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await forcedCloseConfigurationRepository.InsertAsync(softCloseTest);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {

                    var error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }

        #endregion
        #region ForcedCloseReport

        public async Task<ServiceResponse> InsertFocedCloseReport(ForcedCloseTestSample entry)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _deformationTestingSheetRepository = new ForcedCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _deformationTestingSheetRepository.InsertAsync(entry);
                    await _unitOfWork.SaveChangeAsync();

                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Insert",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }

        public async Task<IEnumerable<ForcedCloseTestSample>> LoadFocedCloseReport()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _deformationTestingSheetRepository = new ForcedCloseReportRepository(context);
                return await _deformationTestingSheetRepository.LoadAsync();
            }
        }

        public async Task<ServiceResponse> ClearForcedCloseSheet()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _deformationTestingSheetRepository = new ForcedCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await _deformationTestingSheetRepository.ClearDefomationSheet();
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode = "Database.Clear",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> UpdatePreReportForcedClose ( PreForceClose preForceClose )
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var _deformationTestingSheetRepository = new ForcedCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
     
                     _deformationTestingSheetRepository.UpdateAsync(preForceClose);
                    await _unitOfWork.SaveChangeAsync( );
                    return ServiceResponse.Successful( );
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode="Database.Clear",
                        Message="Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> UpdateForcedCloseReport(IEnumerable<ForcedCloseTestSample> listReport)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var forcedCloseReportRepository = new ForcedCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await forcedCloseReportRepository.UpdateReport(listReport);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode = "Database.Clear",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }

        }

        #endregion
        #region EnduranceSettingParamter
        public async Task<ServiceResponse> EditEnduranceSetting(EnduranceSettingParameter entry)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _enduranceSettingParameterRepository = new EnduranceSettingParameterRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _enduranceSettingParameterRepository.UpdateAsync(entry);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.EnduranceSetting.Edit",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<EnduranceSettingParameter> LoadEnduranceSetting(int ID)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _enduranceSettingParameterRepository = new EnduranceSettingParameterRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    var data = _enduranceSettingParameterRepository.Load(ID);
                    await _unitOfWork.SaveChangeAsync();
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
        #region StaticLoadConfiguration
        public async Task<ServiceResponse> UpdatePreReportEndurance (PreReportEndurance preReportEndurance)
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var _deformationTestingSheetRepository = new StaticLoadReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _deformationTestingSheetRepository.UpdateAsync(preReportEndurance);
                    await _unitOfWork.SaveChangeAsync( );
                    return ServiceResponse.Successful( );
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode="Database.Clear",
                        Message="Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> UpdateStaticLoadConfiguration(StaticLoadTest config)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var staticLoadConfigurationRepository = new StaticLoadConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    staticLoadConfigurationRepository.UpdateConfiguration(config);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<StaticLoadTest> LoadStaticLoadConfiguration()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var staticLoadConfigurationRepository = new StaticLoadConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    return await staticLoadConfigurationRepository.LoadConfiguration();
                }
                catch
                {
                    return null;
                }
            }
        }
        public async Task<ServiceResponse> InsertStaticLoadConfiguration(StaticLoadTest config)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var staticLoadConfigurationRepository = new StaticLoadConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    staticLoadConfigurationRepository.InsertAsync(config);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }

        #endregion
        #region StaticLoadReport
        public async Task<ServiceResponse> UpdateCurlingForceReport(ObservableCollection<StaticLoadTestSample> reportStaticLoad)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _staticLoadRepository = new StaticLoadReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await _staticLoadRepository.UpdateAsync(reportStaticLoad);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Insert",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> InsertStaticLoadTestSample(StaticLoadTestSample report)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var staticLoadConfigurationRepository = new StaticLoadReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await staticLoadConfigurationRepository.InsertAsync(report);
                    await _unitOfWork.SaveChangeAsync();

                    return ServiceResponse.Successful();
                }
                catch (Exception ex)
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Insert",
                        Message = ex.ToString()
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> ClearStaticLoad()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _staticLoadRepository = new StaticLoadReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await _staticLoadRepository.ClearAsync();
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode = "Database.Clear",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<IList<StaticLoadTestSample>> LoadStaticLoadReport()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _staticLoadRepository = new StaticLoadReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    var data = await _staticLoadRepository.LoadAsync();
                    await _unitOfWork.SaveChangeAsync();
                    return (IList<StaticLoadTestSample>)data;
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
        #region CurlingForceConfiguration
        public async Task<ServiceResponse> UpdateCurlingForceConfiguration(CurlingForceTest config)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var curlingForceConfigurationRepository = new CurlingForceConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    curlingForceConfigurationRepository.UpdateConfiguration(config);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> InsertCurlingForceConfiguration(CurlingForceTest config)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var curlingForceConfigurationRepository = new CurlingForceConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await curlingForceConfigurationRepository.InsertConfiguration(config);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<CurlingForceTest> LoadCurlingForceConfiguration()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var curlingForceConfigurationRepository = new CurlingForceConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    return await curlingForceConfigurationRepository.LoadConfiguration();
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
        #region CurlingForceReport
        public async Task<ServiceResponse> UpadateCurlingForceReport(ObservableCollection<CurlingForceTestSample> listReport)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _curlingForceRepository = new CurlingForceReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await _curlingForceRepository.UpdateAsync(listReport);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> InsertCurlingForceTestSample(ObservableCollection<CurlingForceTestSample> report)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _curlingForceRepository = new CurlingForceReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await _curlingForceRepository.InsertAsync(report);
                    await _unitOfWork.SaveChangeAsync();

                    return ServiceResponse.Successful();
                }
                catch (Exception ex)
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Insert",
                        Message = ex.ToString()
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> ClearCurlingForceTestSample()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _curlingForceRepository = new CurlingForceReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await _curlingForceRepository.ClearAsync();
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode = "Database.Clear",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<IList<CurlingForceTestSample>> LoadCurlingForceTestSample()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _curlingForceRepository = new CurlingForceReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                var result = await _curlingForceRepository.LoadAsync();
                await _unitOfWork.SaveChangeAsync();
                return result;
            }
        }
        #endregion
        #region RockTestConfiguration    
        public async Task<ServiceResponse> UpdateRockTestConfiguration(RockTest config)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var rockTestConfigurationRepository = new RockTestConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    rockTestConfigurationRepository.UpdateConfiguration(config);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<RockTest> LoadRockTestConfiguration()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var rockTestConfigurationRepository = new RockTestConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    return await rockTestConfigurationRepository.LoadConfiguration();
                }
                catch
                {
                    return null;
                }
            }
        }
        public async Task<ServiceResponse> InsertRockTestConfiguration(RockTest config)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var rockTestConfigurationRepository = new RockTestConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await rockTestConfigurationRepository.InsertConfiguration(config);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }

        #endregion
        #region RockTestReport
        public async Task<ServiceResponse> UpdateRockTestSample(ObservableCollection<RockTestSample> listReport)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var rockTestReportRepository = new RockTestReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await rockTestReportRepository.UpdateAsync(listReport);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> InsertRockTestSample(RockTestSample report)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _rockTestRepository = new RockTestReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await _rockTestRepository.InsertAsync(report);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Insert",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> ClearRockTestSample()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _rockTestRepository = new RockTestReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    await _rockTestRepository.ClearAsync();
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode = "Database.Clear",
                        Message = "Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<IList<RockTestSample>> LoadRockTestSample()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _rockTestRepository = new RockTestReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                var data = await _rockTestRepository.LoadAsync();
                await _unitOfWork.SaveChangeAsync();
                return data;
            }
        }
        #endregion
        #region WaterProofingConfiguration

        public async Task<ServiceResponse> UpdateWaterProofingConfiguration(WaterProofingTest config)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _waterProofingConfigRepository = new WaterProofingConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _waterProofingConfigRepository.UpdateConfiguration(config);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> InsertWaterProofingConfiguration(WaterProofingTest config)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _waterProofingConfigRepository = new WaterProofingConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _waterProofingConfigRepository.InsertConfiguration(config);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Update",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public Task<WaterProofingTest> LoadWaterProofingConfiguration()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _waterProofingConfigRepository = new WaterProofingConfigurationRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    return _waterProofingConfigRepository.LoadConfiguration();
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
        #region WaterProofingReport
        public async Task<ServiceResponse> UpdateWaterProofingReport(IList<WaterProofingTestSample> reports)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _waterProofingReportRepository = new WaterProofingReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _waterProofingReportRepository.UpdateReport(reports);
                    _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    return ServiceResponse.Failed(new Error("Database.UPDATE", "Lỗi Database"));
                }
            }
        }
        public async Task<IEnumerable<WaterProofingTestSample>> LoadWaterProofingReportAsync()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _waterProofingReportRepository = new WaterProofingReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    return await _waterProofingReportRepository.LoadReportAsync();
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
        #region HistorySoftClose
        public async Task<ServiceResponse> InsertHistoryReliability(HistorySoftCloseReport report)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _historyReliabilityReport = new HistorySoftCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _historyReliabilityReport.InsertAsync(report);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Insert",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<IList<HistorySoftCloseReport>> LoadHistoryReliability(DateTime timestart, DateTime timestop)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _historyReliabilityReport = new HistorySoftCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    var data = _historyReliabilityReport.Load(timestart, timestop);
                    await _unitOfWork.SaveChangeAsync();
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion
        #region HistoryForcedClose
        public async Task<ServiceResponse> InsertHistoryDeformation(HistoryForcedCloseReport report)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _deformationReportRepository = new HistoryForcedCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _deformationReportRepository.InsertAsync(report);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Insert",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<IList<HistoryForcedCloseReport>> LoadHistoryDeformation(DateTime timestart, DateTime timestop)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _deformationReportRepository = new HistoryForcedCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    var data = _deformationReportRepository.LoadAsync(timestart, timestop);
                    await _unitOfWork.SaveChangeAsync();
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
        #region HistoryEndurance
        public async Task<ServiceResponse> InsertHistoryEndurance(HistoryEnduranceReport report)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _historyEnduranceReportRepository = new HistoryEnduranceReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _historyEnduranceReportRepository.InsertAsync(report);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Insert",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<IList<HistoryEnduranceReport>> LoadHistoryEndurance(DateTime timestart, DateTime timestop)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var _historyEnduranceReportRepository = new HistoryEnduranceReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    var data = _historyEnduranceReportRepository.Load(timestart, timestop);
                    await _unitOfWork.SaveChangeAsync();
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
        #region Waterproofing
        public async Task<ServiceResponse> InsertHistoryWaterProofing(HistoryWaterProofingReport report)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var historyWaterProofingReport = new HistoryWaterProofingReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    historyWaterProofingReport.InsertAsync(report);
                    await _unitOfWork.SaveChangeAsync();
                    return ServiceResponse.Successful();
                }
                catch
                {
                    Error error = new Error
                    {
                        ErrorCode = "Database.Insert",
                        Message = "Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<ServiceResponse> UpdatePreReportWaterProofing (PreReportWaterProofing preReportWaterProofing)
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var _deformationTestingSheetRepository = new WaterProofingReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {

                    _deformationTestingSheetRepository.UpdateAsync(preReportWaterProofing);
                    await _unitOfWork.SaveChangeAsync( );
                    return ServiceResponse.Successful( );
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode="Database.Clear",
                        Message="Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
        public async Task<IList<HistoryWaterProofingReport>> LoadHistoryWaterProofing(DateTime timestart, DateTime timestop)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var historyWaterProofingReport = new HistoryWaterProofingReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    var data = historyWaterProofingReport.Load(timestart, timestop);
                    await _unitOfWork.SaveChangeAsync();
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<ServiceResponse> DeletePreReportWaterProofing (PreReportWaterProofing PreReportWaterProofing = null)
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var softCloseReportRepository = new WaterProofingReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);

                try
                {
                    await softCloseReportRepository.ClearPreReportAsync( );
                    await _unitOfWork.SaveChangeAsync( );
                    return ServiceResponse.Successful( );
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode="Database.Clear",
                        Message="Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }

        public async Task<ServiceResponse> InsertPreReportWaterProofing (PreReportWaterProofing entry)
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var _reliabilitytestingsheetRepository = new WaterProofingReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _reliabilitytestingsheetRepository.InsertPreReportAsync(entry);
                    await _unitOfWork.SaveChangeAsync( );
                    return ServiceResponse.Successful( );
                }
                catch
                {

                    Error error = new Error
                    {
                        ErrorCode="Database.Insert",
                        Message="Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }

        public async Task<IList<PreReportWaterProofing>> LoadPreReportWaterProofing ( )
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var softCloseConfigurationRepository = new WaterProofingReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                return await softCloseConfigurationRepository.LoadPreReport( );
            }
        }
        public async Task<ServiceResponse> DeletePreReportEndurance (PreReportEndurance preReportEndurance =null )
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var softCloseReportRepository = new StaticLoadReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);

                try
                {
                    await softCloseReportRepository.ClearAsync(preReportEndurance);
                    await _unitOfWork.SaveChangeAsync( );
                    return ServiceResponse.Successful( );
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode="Database.Clear",
                        Message="Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }

        public async Task<ServiceResponse> InsertPreReportEndurance (PreReportEndurance entry)
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var _reliabilitytestingsheetRepository = new EnduranceSettingParameterRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _reliabilitytestingsheetRepository.InsertPreReportAsync(entry);
                    await _unitOfWork.SaveChangeAsync( );
                    return ServiceResponse.Successful( );
                }
                catch
                {

                    Error error = new Error
                    {
                        ErrorCode="Database.Insert",
                        Message="Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }

        public async Task<IList<PreReportEndurance>> LoadPreReportEndurance ( )
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var softCloseConfigurationRepository = new EnduranceSettingParameterRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                return await softCloseConfigurationRepository.LoadPreReport( );
            }
        }

        Task<IEnumerable<RockTestSample>> IDatabaseService.LoadRockTestSample ( )
        {
            throw new NotImplementedException( );
        }
        //
        public async Task<ServiceResponse> DeletePreReportForcedClose (PreForceClose preForceClose )
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var softCloseReportRepository = new ForcedCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);

                try
                {
                    await softCloseReportRepository.ClearPreReportAsync(preForceClose);
                    await _unitOfWork.SaveChangeAsync( );
                    return ServiceResponse.Successful( );
                }
                catch
                {
                    var error = new Error
                    {
                        ErrorCode="Database.Clear",
                        Message="Lỗi database."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }
       
        public async Task<ServiceResponse> InsertPreReportForcedClose (PreForceClose entry)
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var _reliabilitytestingsheetRepository = new ForcedCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                try
                {
                    _reliabilitytestingsheetRepository.InsertPreReportAsync(entry);
                    await _unitOfWork.SaveChangeAsync( );
                    return ServiceResponse.Successful( );
                }
                catch
                {

                    Error error = new Error
                    {
                        ErrorCode="Database.Insert",
                        Message="Lỗi khác."
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }

        public async Task<IList<PreForceClose>> LoadPreReportForcedClose ( )
        {
            using ( var context = _contextFactory.CreateDbContext( ) )
            {
                var softCloseConfigurationRepository = new ForcedCloseReportRepository(context);
                var _unitOfWork = new UnitOfWork(context);
                return await softCloseConfigurationRepository.LoadPreReport( );
            }
        }


        #endregion
    }
}

