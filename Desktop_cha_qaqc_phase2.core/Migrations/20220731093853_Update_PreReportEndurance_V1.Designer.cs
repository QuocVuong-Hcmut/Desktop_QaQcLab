﻿// <auto-generated />
using System;
using Desktop_cha_qaqc_phase2.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220731093853_Update_PreReportEndurance_V1")]
    partial class Update_PreReportEndurance_V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.CurlingForceTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestPurpose")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CurlingForceTests");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.CurlingForceTestSample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CurlingForceTestId")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("DeformationDegree")
                        .HasColumnType("REAL");

                    b.Property<int?>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Load")
                        .HasColumnType("REAL");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfError")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tester")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CurlingForceTestId");

                    b.ToTable("CurlingForceTestSamples");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.EnduranceSettingParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("CompressionForceSettingsystem12")
                        .HasColumnType("REAL");

                    b.Property<float>("CompressionForceSettingsystem3")
                        .HasColumnType("REAL");

                    b.Property<short>("NumberClick12")
                        .HasColumnType("INTEGER");

                    b.Property<short>("NumberClick3")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeOccupying12")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeOccupying3")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("system12")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("system3")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("EnduranceSettingParameters");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.ForcedCloseTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestPurpose")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ForcedCloseTests");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.ForcedCloseTestSample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("FallTimeLid")
                        .HasColumnType("REAL");

                    b.Property<double>("FallTimeRing")
                        .HasColumnType("REAL");

                    b.Property<int?>("ForcedCloseTestId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLidIntact")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLidPassed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLidUnleaked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRingIntact")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRingPassed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRingUnleaked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfClosing")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfError")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tester")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ForcedCloseTestId");

                    b.ToTable("ForcedCloseTestSamples");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.HistoryEnduranceReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameTest")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HistoryEnduranceReports");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.HistoryForcedCloseReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameTest")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HistoryForcedCloseReports");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.HistorySoftCloseReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameTest")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HistorySoftCloseReports");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.HistoryWaterProofingReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameTest")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HistoryWaterProofingReports");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.PreReportEndurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Force1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Force2")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number1")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Number2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Time1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Time2")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("PreReportEndurances");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.PreReportWaterProofing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Temperature")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Time")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("PreReportWaterProofings");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.RockTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestPurpose")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("RockTests");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.RockTestSample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Load")
                        .HasColumnType("REAL");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfError")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Passed")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RockTestId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TestedTimes")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tester")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RockTestId");

                    b.ToTable("RockTestSamples");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.SoftCloseTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestPurpose")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SoftCloseTests");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.SoftCloseTestSample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("FallTimeLid")
                        .HasColumnType("REAL");

                    b.Property<double>("FallTimeRing")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsBumperLidIntact")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBumperLidUnleaked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBumperRingIntact")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBumperRingUnleaked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLidPassed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRingPassed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfClosing")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfError")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SoftCloseTestId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SoftCloseTestId1")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tester")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SoftCloseTestId");

                    b.HasIndex("SoftCloseTestId1");

                    b.ToTable("SoftCloseTestSamples");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.StaticLoadTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestPurpose")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("StaticLoadTests");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.StaticLoadTestSample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfError")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StaticLoadTestId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tester")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StaticLoadTestId");

                    b.ToTable("StaticLoadTestSamples");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.WaterProofingTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestPurpose")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("WaterProofingTests");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.WaterProofingTestSample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Duration")
                        .HasColumnType("REAL");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfError")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Passed")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Temperature")
                        .HasColumnType("REAL");

                    b.Property<string>("Tester")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("WaterProofingTestId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("WaterProofingTestId");

                    b.ToTable("WaterProofingTestSamples");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.CurlingForceTestSample", b =>
                {
                    b.HasOne("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.CurlingForceTest", null)
                        .WithMany("TestSheet")
                        .HasForeignKey("CurlingForceTestId");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.ForcedCloseTestSample", b =>
                {
                    b.HasOne("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.ForcedCloseTest", null)
                        .WithMany("Testsheet")
                        .HasForeignKey("ForcedCloseTestId");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.RockTestSample", b =>
                {
                    b.HasOne("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.RockTest", null)
                        .WithMany("TestSheets")
                        .HasForeignKey("RockTestId");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.SoftCloseTestSample", b =>
                {
                    b.HasOne("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.SoftCloseTest", null)
                        .WithMany("Testsheet")
                        .HasForeignKey("SoftCloseTestId");

                    b.HasOne("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.SoftCloseTest", null)
                        .WithMany()
                        .HasForeignKey("SoftCloseTestId1");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.StaticLoadTestSample", b =>
                {
                    b.HasOne("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.StaticLoadTest", null)
                        .WithMany("TestSheets")
                        .HasForeignKey("StaticLoadTestId");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.WaterProofingTestSample", b =>
                {
                    b.HasOne("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.WaterProofingTest", null)
                        .WithMany("TestSheets")
                        .HasForeignKey("WaterProofingTestId");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.CurlingForceTest", b =>
                {
                    b.Navigation("TestSheet");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.ForcedCloseTest", b =>
                {
                    b.Navigation("Testsheet");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.RockTest", b =>
                {
                    b.Navigation("TestSheets");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.SoftCloseTest", b =>
                {
                    b.Navigation("Testsheet");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.StaticLoadTest", b =>
                {
                    b.Navigation("TestSheets");
                });

            modelBuilder.Entity("Desktop_cha_qaqc_phase2.Core.Domain.Models.Resource.WaterProofingTest", b =>
                {
                    b.Navigation("TestSheets");
                });
#pragma warning restore 612, 618
        }
    }
}
