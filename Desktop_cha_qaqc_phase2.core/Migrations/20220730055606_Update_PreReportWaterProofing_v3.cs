using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    public partial class Update_PreReportWaterProofing_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "WaterProofingTestSamples",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "PreReportWaterProofings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Temperature = table.Column<int>(type: "INTEGER", nullable: false),
                    Time = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreReportWaterProofings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreReportWaterProofings");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "WaterProofingTestSamples",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
