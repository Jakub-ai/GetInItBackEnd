using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetInItBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class alt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Accounts_CreatedById",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_CreatedById",
                table: "JobApplications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_CreatedById",
                table: "JobApplications",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Accounts_CreatedById",
                table: "JobApplications",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
