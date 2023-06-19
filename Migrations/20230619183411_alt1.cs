using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetInItBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class alt1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_CreatedById",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Accounts_CreatedById",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_CreatedById",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Offers_CreatedById",
                table: "Offers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Accounts_CreatedById",
                table: "Offers",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
