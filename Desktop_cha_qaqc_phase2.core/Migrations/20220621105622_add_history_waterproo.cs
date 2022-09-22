using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    public partial class add_history_waterproo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryWaterProofingReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Temperature = table.Column<int>(type: "INTEGER", nullable: false),
                    Duration = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryWaterProofingReports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryWaterProofingReports");
        }
    }
}
