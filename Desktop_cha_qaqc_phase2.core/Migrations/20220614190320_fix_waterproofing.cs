using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    public partial class fix_waterproofing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "WaterProofingTestSamples");

            migrationBuilder.AddColumn<bool>(
                name: "Passed",
                table: "WaterProofingTestSamples",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passed",
                table: "WaterProofingTestSamples");

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "WaterProofingTestSamples",
                type: "TEXT",
                nullable: true);
        }
    }
}
