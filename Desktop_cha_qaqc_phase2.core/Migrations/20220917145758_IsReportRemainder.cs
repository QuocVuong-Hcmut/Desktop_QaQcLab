using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    public partial class IsReportRemainder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPreReport",
                table: "WaterProofingTestSamples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPreReport",
                table: "StaticLoadTestSamples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPreReport",
                table: "SoftCloseTestSamples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPreReport",
                table: "RockTestSamples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsReport",
                table: "PreReportEndurances",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReport",
                table: "PreForceCloses",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IdPreReport",
                table: "ForcedCloseTestSamples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPreReport",
                table: "CurlingForceTestSamples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPreReport",
                table: "WaterProofingTestSamples");

            migrationBuilder.DropColumn(
                name: "IdPreReport",
                table: "StaticLoadTestSamples");

            migrationBuilder.DropColumn(
                name: "IdPreReport",
                table: "SoftCloseTestSamples");

            migrationBuilder.DropColumn(
                name: "IdPreReport",
                table: "RockTestSamples");

            migrationBuilder.DropColumn(
                name: "IsReport",
                table: "PreReportEndurances");

            migrationBuilder.DropColumn(
                name: "IsReport",
                table: "PreForceCloses");

            migrationBuilder.DropColumn(
                name: "IdPreReport",
                table: "ForcedCloseTestSamples");

            migrationBuilder.DropColumn(
                name: "IdPreReport",
                table: "CurlingForceTestSamples");
        }
    }
}
