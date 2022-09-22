using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    public partial class Update_PreForceClosing_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Number2",
                table: "PreReportEndurances",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "PreForceCloses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeClosing1 = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeClosing2 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreForceCloses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreForceCloses");

            migrationBuilder.AlterColumn<string>(
                name: "Number2",
                table: "PreReportEndurances",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
