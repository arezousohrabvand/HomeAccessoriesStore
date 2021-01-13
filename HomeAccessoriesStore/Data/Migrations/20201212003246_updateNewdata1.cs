using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAccessoriesStore.Data.Migrations
{
    public partial class updateNewdata1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrdersId",
                table: "OrderDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Order_OrdersId",
                table: "OrderDetail",
                column: "OrdersId",
                principalTable: "Order",
                principalColumn: "OrdersId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Order_OrdersId",
                table: "OrderDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrdersId",
                table: "OrderDetail",
                column: "OrdersId",
                principalTable: "Order",
                principalColumn: "OrdersId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
