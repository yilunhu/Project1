using Microsoft.EntityFrameworkCore.Migrations;

namespace abcBank.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "AccountId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "AccID",
                table: "Translogs");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_CustomerID",
                table: "Accounts",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Translogs_Accounts_AccountID",
                table: "Translogs",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customers_CustomerID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Translogs_Accounts_AccountID",
                table: "Translogs");

            migrationBuilder.AddForeignKey(
                name: "AccountId",
                table: "Accounts",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "AccID",
                table: "Translogs",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
