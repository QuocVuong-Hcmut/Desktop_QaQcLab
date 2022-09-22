
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource;
using Microsoft.EntityFrameworkCore.Design;

namespace Desktop_cha_qaqc_phase2.Core.Persistence.Context
{
    public class ApplicationDbContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext (string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>( );
            //optionsBuilder.UseSqlite(@"Data Source=D:\sourceCs\luận văn\Desktop_cha_qaqc_phase2\Desktop_cha_qaqc_phase2\bin\Debug\net6.0-windows\win-x64\Desktop_cha_qaqc_phase2.db");
            //optionsBuilder.UseSqlite(@"Data Source=D:\sourceCs\luận văn\Desktop_cha_qaqc_phase2\Desktop_cha_qaqc_phase2.core\Desktop_cha_qaqc_phase2.db");
            optionsBuilder.UseSqlite(@"Data Source= Desktop_cha_qaqc_phase2.db");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }

    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        #region SoftClose
        public DbSet<SoftCloseTest> SoftCloseTests { get; set; }
        public DbSet<SoftCloseTestSample> SoftCloseTestSamples { get; set; }
        #endregion
        #region ForcedClose
        public DbSet<ForcedCloseTest> ForcedCloseTests { get; set; }
        public DbSet<ForcedCloseTestSample> ForcedCloseTestSamples { get; set; }
        #endregion
        #region HistoryReport
        public DbSet<HistorySoftCloseReport> HistorySoftCloseReports { get; set; }
        public DbSet<HistoryForcedCloseReport> HistoryForcedCloseReports { get; set; }
        public DbSet<HistoryEnduranceReport> HistoryEnduranceReports { get; set; }
        public DbSet<HistoryWaterProofingReport> HistoryWaterProofingReports { get; set; }
        #endregion
        #region Endurance Parameter
        public DbSet<EnduranceSettingParameter> EnduranceSettingParameters { get; set; }
        #endregion
        #region StaticLoad
        public DbSet<StaticLoadTest> StaticLoadTests { get; set; }
        public DbSet<StaticLoadTestSample> StaticLoadTestSamples { get; set; }
        #endregion
        #region CurlingForce
        public DbSet<CurlingForceTest> CurlingForceTests { get; set; }
        public DbSet<CurlingForceTestSample> CurlingForceTestSamples { get; set; }
        #endregion
        #region RockTest
        public DbSet<RockTest> RockTests { get; set; }
        public DbSet<RockTestSample> RockTestSamples { get; set; }
        #endregion
        #region WaterProofing
        public DbSet<WaterProofingTest> WaterProofingTests { get; set; }
        public DbSet<WaterProofingTestSample> WaterProofingTestSamples { get; set; }
        public DbSet<PreReportWaterProofing> PreReportWaterProofings { get; set; }
        public DbSet<PreReportEndurance> PreReportEndurances { set; get; }
        public DbSet<PreForceClose> PreForceCloses { set; get; }
        #endregion
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite(@"Data Source=D:\sourceCs\luận văn\Desktop_cha_qaqc_phase2\Desktop_cha_qaqc_phase2\bin\Debug\net6.0-windows\win-x64\Desktop_cha_qaqc_phase2.db");
            //optionsBuilder.UseSqlite(@"Data Source=D:\sourceCs\luận văn\Desktop_cha_qaqc_phase2\Desktop_cha_qaqc_phase2.core\Desktop_cha_qaqc_phase2.db");
            optionsBuilder.UseSqlite(@"Data Source= Desktop_cha_qaqc_phase2.db");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            #region SoftClose
            modelBuilder
                .Entity<SoftCloseTest>(entity =>
                {
                    entity.HasKey(r => r.Id);
                    entity.HasMany<SoftCloseTestSample>( );
                });
            modelBuilder
               .Entity<SoftCloseTestSample>(entity =>
               {
                   entity.HasKey(r => r.Id);
                   entity.Property(p => p.NumberOfClosing).IsRequired(true);
                   entity.Property(p => p.FallTimeLid).IsRequired(true);
                   entity.Property(p => p.FallTimeRing).IsRequired(true);
               });

            #endregion
            #region ForcedClose
            modelBuilder
                .Entity<ForcedCloseTest>(entity =>
                {
                    entity.HasKey(r => r.Id);
                });
            modelBuilder
    .Entity<PreForceClose>(entity =>
    {
        entity.HasKey(r => r.Id);
    });
            modelBuilder
               .Entity<ForcedCloseTestSample>(entity =>
               {
                   entity.HasKey(r => r.Id);
                   entity.Property(p => p.NumberOfClosing).IsRequired(true);
                   entity.Property(p => p.FallTimeLid).IsRequired(true);
               });
            #endregion
            #region Endurance 
            //Endurance Setting Parameter
            modelBuilder
                .Entity<EnduranceSettingParameter>( )
                .HasKey(a => a.Id);
            modelBuilder.Entity<PreReportEndurance>( )
                        .HasKey(k => k.Id);
            //StaticLoad
            modelBuilder
                .Entity<StaticLoadTest>( )
                .HasKey(a => a.Id);
            modelBuilder
                .Entity<StaticLoadTestSample>( )
                .HasKey(a => a.Id);
            //CurlingForce
            modelBuilder
               .Entity<CurlingForceTest>( )
               .HasKey(a => a.Id);
            modelBuilder
               .Entity<CurlingForceTestSample>(entity =>
               {
                   entity.HasKey(p => p.Id);
                   entity.Property(p => p.Load).IsRequired(false);
                   entity.Property(p => p.Duration).IsRequired(false);
               });
            //Rock Test
            modelBuilder
                .Entity<RockTest>( )
                .HasKey(a => a.Id);
            modelBuilder.Entity<RockTestSample>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Load).IsRequired(false);
            });
            #endregion
            #region History
            modelBuilder.Entity<HistorySoftCloseReport>( )
                .HasKey(k => k.Id);

            modelBuilder.Entity<HistoryForcedCloseReport>( )
                .HasKey(k => k.Id);

            modelBuilder.Entity<HistoryEnduranceReport>( )
                .HasKey(k => k.Id);

            modelBuilder.Entity<HistoryWaterProofingReport>( )
                .HasKey(k => k.Id);
            #endregion
            #region WaterProofing
            modelBuilder.Entity<WaterProofingTest>( )
                .HasKey(k => k.Id);

            modelBuilder.Entity<WaterProofingTestSample>( )
                .HasKey(k => k.Id);
            modelBuilder.Entity<PreReportWaterProofing>( )
                .HasKey(k => k.Id);
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
