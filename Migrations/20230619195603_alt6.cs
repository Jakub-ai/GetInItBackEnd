using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetInItBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class alt6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Companies_CompanyId",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Companies_CompanyId",
                table: "Accounts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Companies_CompanyId",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Companies_CompanyId",
                table: "Accounts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
