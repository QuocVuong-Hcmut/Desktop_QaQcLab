using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurlingForceTestSheets");

            migrationBuilder.DropTable(
                name: "DeformationHistoryReports");

            migrationBuilder.DropTable(
                name: "DeformationTestSheet");

            migrationBuilder.DropTable(
                name: "ReliabilityHistoryReports");

            migrationBuilder.DropTable(
                name: "ReliabilityTestSheet");

            migrationBuilder.DropTable(
                name: "RockTestTestSheets");

            migrationBuilder.DropTable(
                name: "StaticLoadTestSheets");

            migrationBuilder.DropTable(
                name: "CurlingForceTestingConfiguration");

            migrationBuilder.DropTable(
                name: "DeformationTestingConfigurations");

            migrationBuilder.DropTable(
                name: "ReliabilityTestingConfigurations");

            migrationBuilder.DropTable(
                name: "RockTestTestingConfigurations");

            migrationBuilder.DropTable(
                name: "StaticLoadTestingConfigurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnduranceHistoryReports",
                table: "EnduranceHistoryReports");

            migrationBuilder.RenameTable(
                name: "EnduranceHistoryReports",
                newName: "HistoryEnduranceReports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryEnduranceReports",
                table: "HistoryEnduranceReports",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CurlingForceTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurlingForceTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForcedCloseTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForcedCloseTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryForcedCloseReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberOfTest = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryForcedCloseReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorySoftCloseReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberOfTest = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorySoftCloseReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RockTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RockTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoftCloseTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftCloseTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticLoadTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeStampStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeStampFinish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticLoadTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterProofingTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterProofingTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterProofingTestSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Temperature = table.Column<double>(type: "REAL", nullable: false),
                    Duration = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterProofingTestSamples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurlingForceTestSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Load = table.Column<double>(type: "REAL", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: true),
                    DeformationDegree = table.Column<double>(type: "REAL", nullable: true),
                    CurlingForceTestId = table.Column<int>(type: "INTEGER", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurlingForceTestSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurlingForceTestSamples_CurlingForceTests_CurlingForceTestId",
                        column: x => x.CurlingForceTestId,
                        principalTable: "CurlingForceTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ForcedCloseTestSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumberOfClosing = table.Column<int>(type: "INTEGER", nullable: false),
                    FallTimeLid = table.Column<double>(type: "REAL", nullable: false),
                    IsLidIntact = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsLidUnleak = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsLidPassed = table.Column<bool>(type: "INTEGER", nullable: true),
                    FallTimeRing = table.Column<double>(type: "REAL", nullable: true),
                    IsRingIntact = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsRingUnleak = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsRingPassed = table.Column<bool>(type: "INTEGER", nullable: true),
                    ForcedCloseTestId = table.Column<int>(type: "INTEGER", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForcedCloseTestSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForcedCloseTestSamples_ForcedCloseTests_ForcedCloseTestId",
                        column: x => x.ForcedCloseTestId,
                        principalTable: "ForcedCloseTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RockTestSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Load = table.Column<double>(type: "REAL", nullable: true),
                    TestedTimes = table.Column<int>(type: "INTEGER", nullable: true),
                    Passed = table.Column<bool>(type: "INTEGER", nullable: true),
                    RockTestId = table.Column<int>(type: "INTEGER", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RockTestSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RockTestSamples_RockTests_RockTestId",
                        column: x => x.RockTestId,
                        principalTable: "RockTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SoftCloseTestSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumberOfClosing = table.Column<int>(type: "INTEGER", nullable: false),
                    FallTimeLid = table.Column<double>(type: "REAL", nullable: false),
                    IsBumperLidIntact = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsBumperLidNotLeaked = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsBumperLidPassed = table.Column<bool>(type: "INTEGER", nullable: true),
                    FallTimeRing = table.Column<double>(type: "REAL", nullable: false),
                    IsBumperRingIntact = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsBumperRingNotLeaked = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsRingPassed = table.Column<bool>(type: "INTEGER", nullable: true),
                    SoftCloseTestId = table.Column<int>(type: "INTEGER", nullable: true),
                    SoftCloseTestId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftCloseTestSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoftCloseTestSamples_SoftCloseTests_SoftCloseTestId",
                        column: x => x.SoftCloseTestId,
                        principalTable: "SoftCloseTests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SoftCloseTestSamples_SoftCloseTests_SoftCloseTestId1",
                        column: x => x.SoftCloseTestId1,
                        principalTable: "SoftCloseTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaticLoadTestSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true),
                    StaticLoadTestId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticLoadTestSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticLoadTestSamples_StaticLoadTests_StaticLoadTestId",
                        column: x => x.StaticLoadTestId,
                        principalTable: "StaticLoadTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurlingForceTestSamples_CurlingForceTestId",
                table: "CurlingForceTestSamples",
                column: "CurlingForceTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ForcedCloseTestSamples_ForcedCloseTestId",
                table: "ForcedCloseTestSamples",
                column: "ForcedCloseTestId");

            migrationBuilder.CreateIndex(
                name: "IX_RockTestSamples_RockTestId",
                table: "RockTestSamples",
                column: "RockTestId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftCloseTestSamples_SoftCloseTestId",
                table: "SoftCloseTestSamples",
                column: "SoftCloseTestId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftCloseTestSamples_SoftCloseTestId1",
                table: "SoftCloseTestSamples",
                column: "SoftCloseTestId1");

            migrationBuilder.CreateIndex(
                name: "IX_StaticLoadTestSamples_StaticLoadTestId",
                table: "StaticLoadTestSamples",
                column: "StaticLoadTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurlingForceTestSamples");

            migrationBuilder.DropTable(
                name: "ForcedCloseTestSamples");

            migrationBuilder.DropTable(
                name: "HistoryForcedCloseReports");

            migrationBuilder.DropTable(
                name: "HistorySoftCloseReports");

            migrationBuilder.DropTable(
                name: "RockTestSamples");

            migrationBuilder.DropTable(
                name: "SoftCloseTestSamples");

            migrationBuilder.DropTable(
                name: "StaticLoadTestSamples");

            migrationBuilder.DropTable(
                name: "WaterProofingTests");

            migrationBuilder.DropTable(
                name: "WaterProofingTestSamples");

            migrationBuilder.DropTable(
                name: "CurlingForceTests");

            migrationBuilder.DropTable(
                name: "ForcedCloseTests");

            migrationBuilder.DropTable(
                name: "RockTests");

            migrationBuilder.DropTable(
                name: "SoftCloseTests");

            migrationBuilder.DropTable(
                name: "StaticLoadTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryEnduranceReports",
                table: "HistoryEnduranceReports");

            migrationBuilder.RenameTable(
                name: "HistoryEnduranceReports",
                newName: "EnduranceHistoryReports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnduranceHistoryReports",
                table: "EnduranceHistoryReports",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CurlingForceTestingConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStampFinish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeStampStart = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurlingForceTestingConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeformationHistoryReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumberOfTest = table.Column<string>(type: "TEXT", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeformationHistoryReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeformationTestingConfigurations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStampFinish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeStampStart = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeformationTestingConfigurations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReliabilityHistoryReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumberOfTest = table.Column<string>(type: "TEXT", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReliabilityHistoryReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReliabilityTestingConfigurations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: true),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStampFinish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeStampStart = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReliabilityTestingConfigurations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RockTestTestingConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStampFinish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeStampStart = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RockTestTestingConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticLoadTestingConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStampFinish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeStampStart = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticLoadTestingConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurlingForceTestSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurlingForceTestingConfigurationsId = table.Column<int>(type: "INTEGER", nullable: true),
                    DeformationDegree = table.Column<double>(type: "REAL", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: true),
                    Load = table.Column<double>(type: "REAL", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurlingForceTestSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurlingForceTestSheets_CurlingForceTestingConfiguration_CurlingForceTestingConfigurationsId",
                        column: x => x.CurlingForceTestingConfigurationsId,
                        principalTable: "CurlingForceTestingConfiguration",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeformationTestSheet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeformationTestingConfigurationsID = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    NumberOfTest = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusLidNotBreak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusLidNotLeak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusLidResult = table.Column<string>(type: "TEXT", nullable: true),
                    StatusPlinthNotBreak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusPlinthNotLeak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusPlinthResult = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true),
                    TimeSmoothClosingLid = table.Column<double>(type: "REAL", nullable: false),
                    TimeSmoothClosingPlinth = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeformationTestSheet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeformationTestSheet_DeformationTestingConfigurations_DeformationTestingConfigurationsID",
                        column: x => x.DeformationTestingConfigurationsID,
                        principalTable: "DeformationTestingConfigurations",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ReliabilityTestSheet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    NumberOfTest = table.Column<int>(type: "INTEGER", nullable: false),
                    ReliabilityTestingConfigurationsID = table.Column<int>(type: "INTEGER", nullable: true),
                    StaffCheck = table.Column<string>(type: "TEXT", nullable: true),
                    StatusLidNotFall = table.Column<string>(type: "TEXT", nullable: true),
                    StatusLidNotLeak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusLidResult = table.Column<string>(type: "TEXT", nullable: true),
                    StatusPlinthNotFall = table.Column<string>(type: "TEXT", nullable: true),
                    StatusPlinthNotLeak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusPlinthResult = table.Column<string>(type: "TEXT", nullable: true),
                    TimeSmoothClosingLid = table.Column<double>(type: "REAL", nullable: false),
                    TimeSmoothClosingPlinth = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReliabilityTestSheet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReliabilityTestSheet_ReliabilityTestingConfigurations_ReliabilityTestingConfigurationsID",
                        column: x => x.ReliabilityTestingConfigurationsID,
                        principalTable: "ReliabilityTestingConfigurations",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RockTestTestSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Load = table.Column<double>(type: "REAL", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Passed = table.Column<bool>(type: "INTEGER", nullable: true),
                    RockTestTestingConfigurationsId = table.Column<int>(type: "INTEGER", nullable: true),
                    TestedTimes = table.Column<int>(type: "INTEGER", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RockTestTestSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RockTestTestSheets_RockTestTestingConfigurations_RockTestTestingConfigurationsId",
                        column: x => x.RockTestTestingConfigurationsId,
                        principalTable: "RockTestTestingConfigurations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaticLoadTestSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    StaticLoadTestingConfigurationsId = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticLoadTestSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticLoadTestSheets_StaticLoadTestingConfigurations_StaticLoadTestingConfigurationsId",
                        column: x => x.StaticLoadTestingConfigurationsId,
                        principalTable: "StaticLoadTestingConfigurations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurlingForceTestSheets_CurlingForceTestingConfigurationsId",
                table: "CurlingForceTestSheets",
                column: "CurlingForceTestingConfigurationsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeformationTestSheet_DeformationTestingConfigurationsID",
                table: "DeformationTestSheet",
                column: "DeformationTestingConfigurationsID");

            migrationBuilder.CreateIndex(
                name: "IX_ReliabilityTestSheet_ReliabilityTestingConfigurationsID",
                table: "ReliabilityTestSheet",
                column: "ReliabilityTestingConfigurationsID");

            migrationBuilder.CreateIndex(
                name: "IX_RockTestTestSheets_RockTestTestingConfigurationsId",
                table: "RockTestTestSheets",
                column: "RockTestTestingConfigurationsId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticLoadTestSheets_StaticLoadTestingConfigurationsId",
                table: "StaticLoadTestSheets",
                column: "StaticLoadTestingConfigurationsId");
        }
    }
}
