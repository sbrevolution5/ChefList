using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMealWA.Server.Migrations
{
    public partial class _024ShoppingOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShoppingOnly",
                table: "IngredientType",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShoppingOnly",
                table: "IngredientType");
        }
    }
}
