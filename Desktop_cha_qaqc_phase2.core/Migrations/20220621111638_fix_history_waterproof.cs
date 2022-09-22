using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    public partial class fix_history_waterproof : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "HistoryWaterProofingReports");

            migrationBuilder.DropColumn(
                name: "NumberOfTest",
                table: "HistorySoftCloseReports");

            migrationBuilder.DropColumn(
                name: "NumberOfTest",
                table: "HistoryForcedCloseReports");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "HistoryWaterProofingReports",
                newName: "NameTest");

            migrationBuilder.AddColumn<string>(
                name: "NameTest",
                table: "HistorySoftCloseReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameTest",
                table: "HistoryForcedCloseReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NameTest",
                table: "HistoryEnduranceReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameTest",
                table: "HistorySoftCloseReports");

            migrationBuilder.DropColumn(
                name: "NameTest",
                table: "HistoryForcedCloseReports");

            migrationBuilder.RenameColumn(
                name: "NameTest",
                table: "HistoryWaterProofingReports",
                newName: "Duration");

            migrationBuilder.AddColumn<int>(
                name: "Temperature",
                table: "HistoryWaterProofingReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfTest",
                table: "HistorySoftCloseReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfTest",
                table: "HistoryForcedCloseReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameTest",
                table: "HistoryEnduranceReports",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
