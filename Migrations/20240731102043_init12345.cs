using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OCR_E_gov.Migrations
{
    /// <inheritdoc />
    public partial class init12345 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Capital_Company_Company_TableCompanyId",
                table: "Capital");

            migrationBuilder.DropForeignKey(
                name: "FK_Witness_Company_Company_TableCompanyId",
                table: "Witness");

            migrationBuilder.DropIndex(
                name: "IX_Witness_Company_TableCompanyId",
                table: "Witness");

            migrationBuilder.DropIndex(
                name: "IX_Capital_Company_TableCompanyId",
                table: "Capital");

            migrationBuilder.DropColumn(
                name: "Company_TableCompanyId",
                table: "Witness");

            migrationBuilder.DropColumn(
                name: "Company_TableCompanyId",
                table: "Capital");

            migrationBuilder.CreateIndex(
                name: "IX_Witness_CompanyId",
                table: "Witness",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Capital_CompanyId",
                table: "Capital",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Capital_Company_CompanyId",
                table: "Capital",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Witness_Company_CompanyId",
                table: "Witness",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Capital_Company_CompanyId",
                table: "Capital");

            migrationBuilder.DropForeignKey(
                name: "FK_Witness_Company_CompanyId",
                table: "Witness");

            migrationBuilder.DropIndex(
                name: "IX_Witness_CompanyId",
                table: "Witness");

            migrationBuilder.DropIndex(
                name: "IX_Capital_CompanyId",
                table: "Capital");

            migrationBuilder.AddColumn<int>(
                name: "Company_TableCompanyId",
                table: "Witness",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Company_TableCompanyId",
                table: "Capital",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Witness_Company_TableCompanyId",
                table: "Witness",
                column: "Company_TableCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Capital_Company_TableCompanyId",
                table: "Capital",
                column: "Company_TableCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Capital_Company_Company_TableCompanyId",
                table: "Capital",
                column: "Company_TableCompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Witness_Company_Company_TableCompanyId",
                table: "Witness",
                column: "Company_TableCompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
