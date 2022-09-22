using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    public partial class update_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurlingForceTestingConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeStampStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeStampFinish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false)
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
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberOfTest = table.Column<string>(type: "TEXT", nullable: true)
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
                    TimeStampStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeStampFinish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeformationTestingConfigurations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EnduranceHistoryReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NameTest = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnduranceHistoryReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnduranceSettingParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompressionForceSettingsystem12 = table.Column<float>(type: "REAL", nullable: false),
                    CompressionForceSettingsystem3 = table.Column<float>(type: "REAL", nullable: false),
                    TimeOccupying12 = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeOccupying3 = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberClick12 = table.Column<short>(type: "INTEGER", nullable: false),
                    NumberClick3 = table.Column<short>(type: "INTEGER", nullable: false),
                    system12 = table.Column<bool>(type: "INTEGER", nullable: false),
                    system3 = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnduranceSettingParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReliabilityHistoryReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberOfTest = table.Column<string>(type: "TEXT", nullable: true)
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
                    TimeStampStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeStampFinish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
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
                    TimeStampStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeStampFinish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false)
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
                    TimeStampStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeStampFinish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TestPurpose = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false)
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
                    Load = table.Column<double>(type: "REAL", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: true),
                    DeformationDegree = table.Column<double>(type: "REAL", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true),
                    CurlingForceTestingConfigurationsId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    NumberOfTest = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeSmoothClosingLid = table.Column<double>(type: "REAL", nullable: false),
                    StatusLidNotBreak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusLidNotLeak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusLidResult = table.Column<string>(type: "TEXT", nullable: true),
                    TimeSmoothClosingPlinth = table.Column<double>(type: "REAL", nullable: true),
                    StatusPlinthNotBreak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusPlinthNotLeak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusPlinthResult = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true),
                    DeformationTestingConfigurationsID = table.Column<int>(type: "INTEGER", nullable: true)
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
                    NumberOfTest = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeSmoothClosingLid = table.Column<double>(type: "REAL", nullable: false),
                    StatusLidNotFall = table.Column<string>(type: "TEXT", nullable: true),
                    StatusLidNotLeak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusLidResult = table.Column<string>(type: "TEXT", nullable: true),
                    TimeSmoothClosingPlinth = table.Column<double>(type: "REAL", nullable: true),
                    StatusPlinthNotFall = table.Column<string>(type: "TEXT", nullable: true),
                    StatusPlinthNotLeak = table.Column<string>(type: "TEXT", nullable: true),
                    StatusPlinthResult = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    StaffCheck = table.Column<string>(type: "TEXT", nullable: true),
                    ReliabilityTestingConfigurationsID = table.Column<int>(type: "INTEGER", nullable: true)
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
                    TestedTimes = table.Column<int>(type: "INTEGER", nullable: true),
                    Passed = table.Column<bool>(type: "INTEGER", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true),
                    RockTestTestingConfigurationsId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfError = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Tester = table.Column<string>(type: "TEXT", nullable: true),
                    StaticLoadTestingConfigurationsId = table.Column<int>(type: "INTEGER", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurlingForceTestSheets");

            migrationBuilder.DropTable(
                name: "DeformationHistoryReports");

            migrationBuilder.DropTable(
                name: "DeformationTestSheet");

            migrationBuilder.DropTable(
                name: "EnduranceHistoryReports");

            migrationBuilder.DropTable(
                name: "EnduranceSettingParameters");

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
        }
    }
}
