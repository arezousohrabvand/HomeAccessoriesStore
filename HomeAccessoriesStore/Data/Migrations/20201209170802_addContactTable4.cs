using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAccessoriesStore.Data.Migrations
{
    public partial class addContactTable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrdersID",
                table: "OrderDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrdersId",
                table: "OrderDetail",
                column: "OrdersId",
                principalTable: "Order",
                principalColumn: "OrdersId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrdersId",
                table: "OrderDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrdersID",
                table: "OrderDetail",
                column: "OrdersId",
                principalTable: "Order",
                principalColumn: "OrdersId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
