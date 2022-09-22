using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    public partial class durationwaterproofing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "WaterProofingTestSamples",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "WaterProofingTestId",
                table: "WaterProofingTestSamples",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaterProofingTestSamples_WaterProofingTestId",
                table: "WaterProofingTestSamples",
                column: "WaterProofingTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterProofingTestSamples_WaterProofingTests_WaterProofingTestId",
                table: "WaterProofingTestSamples",
                column: "WaterProofingTestId",
                principalTable: "WaterProofingTests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterProofingTestSamples_WaterProofingTests_WaterProofingTestId",
                table: "WaterProofingTestSamples");

            migrationBuilder.DropIndex(
                name: "IX_WaterProofingTestSamples_WaterProofingTestId",
                table: "WaterProofingTestSamples");

            migrationBuilder.DropColumn(
                name: "WaterProofingTestId",
                table: "WaterProofingTestSamples");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Duration",
                table: "WaterProofingTestSamples",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
