using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAccessoriesStore.Data.Migrations
{
    public partial class updateOrder3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentType_OrderDetailID",
                table: "PaymentTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTypes_OrderDetail_OrderDetailId",
                table: "PaymentTypes",
                column: "OrderDetailId",
                principalTable: "OrderDetail",
                principalColumn: "OrderDetailsId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTypes_OrderDetail_OrderDetailId",
                table: "PaymentTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentType_OrderDetailID",
                table: "PaymentTypes",
                column: "OrderDetailId",
                principalTable: "OrderDetail",
                principalColumn: "OrderDetailsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
