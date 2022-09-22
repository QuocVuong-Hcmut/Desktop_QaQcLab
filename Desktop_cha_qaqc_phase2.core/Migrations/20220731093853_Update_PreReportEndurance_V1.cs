using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    public partial class Update_PreReportEndurance_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreReportEndurances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Time1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Time2 = table.Column<int>(type: "INTEGER", nullable: false),
                    Force1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Force2 = table.Column<int>(type: "INTEGER", nullable: false),
                    Number1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Number2 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreReportEndurances", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreReportEndurances");
        }
    }
}
