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
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "JobApplications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlLink",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Accounts_CreatedById",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_CreatedById",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "UrlLink",
                table: "JobApplications");
        }
    }
}
