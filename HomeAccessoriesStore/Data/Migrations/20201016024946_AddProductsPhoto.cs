using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAccessoriesStore.Data.Migrations
{
    public partial class AddProductsPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Product");
        }
    }
}
