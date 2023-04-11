using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetInItBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Alt_Migration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubEndsAt",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "SubEndsAt",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubEndsAt",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "SubEndsAt",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
