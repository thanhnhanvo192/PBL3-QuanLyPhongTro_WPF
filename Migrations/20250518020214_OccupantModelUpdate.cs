using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongTro.Migrations
{
    /// <inheritdoc />
    public partial class OccupantModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_Phone",
                table: "Occupants",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "_PermanentAddress",
                table: "Occupants",
                newName: "PermanentAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Occupants",
                newName: "_Phone");

            migrationBuilder.RenameColumn(
                name: "PermanentAddress",
                table: "Occupants",
                newName: "_PermanentAddress");
        }
    }
}
