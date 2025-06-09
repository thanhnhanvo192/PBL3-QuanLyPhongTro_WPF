using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongTro.Migrations
{
    /// <inheritdoc />
    public partial class CreateEnumFaultType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTenantFault",
                table: "Fixes");

            migrationBuilder.AddColumn<int>(
                name: "WhoFault",
                table: "Fixes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhoFault",
                table: "Fixes");

            migrationBuilder.AddColumn<bool>(
                name: "IsTenantFault",
                table: "Fixes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
