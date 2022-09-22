using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_cha_qaqc_phase2.Core.Migrations
{
    public partial class fix_softclosedb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBumperRingNotLeaked",
                table: "SoftCloseTestSamples",
                newName: "IsBumperRingUnleaked");

            migrationBuilder.RenameColumn(
                name: "IsBumperLidNotLeaked",
                table: "SoftCloseTestSamples",
                newName: "IsBumperLidUnleaked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBumperRingUnleaked",
                table: "SoftCloseTestSamples",
                newName: "IsBumperRingNotLeaked");

            migrationBuilder.RenameColumn(
                name: "IsBumperLidUnleaked",
                table: "SoftCloseTestSamples",
                newName: "IsBumperLidNotLeaked");
        }
    }
}
