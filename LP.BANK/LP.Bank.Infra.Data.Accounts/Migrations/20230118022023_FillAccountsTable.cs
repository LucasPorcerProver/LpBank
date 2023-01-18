using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LP.Bank.Infra.Data.Accounts.Migrations
{
    public partial class FillAccountsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "public",
                table: "bank_account",
                columns: new[] { "id", "ammount", "number" },
                values: new object[,]
                {
                    { new Guid("0cdc56fe-94a6-4bec-9c66-1ee8320f8fb8"), 999m, 8445865m },
                    { new Guid("35a71451-1fe9-459b-a88c-80b60238de50"), 50m, 9224968m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "bank_account",
                keyColumn: "id",
                keyValue: new Guid("0cdc56fe-94a6-4bec-9c66-1ee8320f8fb8"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "bank_account",
                keyColumn: "id",
                keyValue: new Guid("35a71451-1fe9-459b-a88c-80b60238de50"));
        }
    }
}
