using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderDemoCoreMVC.Migrations
{
    public partial class SeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "Created", "CustomerAddress", "CustomerName" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uliční 22, Těškov", "Aleš Vopěnka" },
                    { 2, new DateTime(2020, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Testovací 11, Zadov", "Jan Novák" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Code", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 111, "Pračka", 7600.5f },
                    { 2, 112, "Myčka", 9800.6f }
                });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[] { 1, 1, 7600.5f, 1, 1 });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[] { 2, 2, 9800.6f, 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
