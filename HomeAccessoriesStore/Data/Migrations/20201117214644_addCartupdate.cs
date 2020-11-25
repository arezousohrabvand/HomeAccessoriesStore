using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAccessoriesStore.Data.Migrations
{
    public partial class addCartupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomersCustomerId",
                table: "Cart",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomersCustomerId",
                table: "Cart",
                column: "CustomersCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Customers_CustomersCustomerId",
                table: "Cart",
                column: "CustomersCustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Customers_CustomersCustomerId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_CustomersCustomerId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "CustomersCustomerId",
                table: "Cart");
        }
    }
}
