using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetInItBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class alt5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Companies_CompanyId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CompanyId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Payments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CompanyId",
                table: "Payments",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Companies_CompanyId",
                table: "Payments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
