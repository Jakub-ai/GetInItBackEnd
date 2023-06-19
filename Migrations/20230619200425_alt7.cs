using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetInItBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class alt7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "EmploymentTypeOffer");

            migrationBuilder.CreateTable(
                name: "OfferEmploymentType",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    EmploymentTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferEmploymentType", x => new { x.OfferId, x.EmploymentTypeId });
                    table.ForeignKey(
                        name: "FK_OfferEmploymentType_EmploymentTypes_EmploymentTypeId",
                        column: x => x.EmploymentTypeId,
                        principalTable: "EmploymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferEmploymentType_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferEmploymentType_EmploymentTypeId",
                table: "OfferEmploymentType",
                column: "EmploymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "OfferEmploymentType");

            migrationBuilder.CreateTable(
                name: "EmploymentTypeOffer",
                columns: table => new
                {
                    EmploymentTypesId = table.Column<int>(type: "int", nullable: false),
                    OffersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentTypeOffer", x => new { x.EmploymentTypesId, x.OffersId });
                    table.ForeignKey(
                        name: "FK_EmploymentTypeOffer_EmploymentTypes_EmploymentTypesId",
                        column: x => x.EmploymentTypesId,
                        principalTable: "EmploymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmploymentTypeOffer_Offers_OffersId",
                        column: x => x.OffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentTypeOffer_OffersId",
                table: "EmploymentTypeOffer",
                column: "OffersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
