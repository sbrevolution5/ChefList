using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMealWA.Server.Migrations
{
    public partial class _003shoppingIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Measurement",
                table: "ShoppingIngredient",
                newName: "QuantityString");

            migrationBuilder.AddColumn<bool>(
                name: "InCart",
                table: "ShoppingIngredient",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ShoppingIngredient",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingIngredient_IngredientId",
                table: "ShoppingIngredient",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingIngredient_Ingredient_IngredientId",
                table: "ShoppingIngredient",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingIngredient_Ingredient_IngredientId",
                table: "ShoppingIngredient");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingIngredient_IngredientId",
                table: "ShoppingIngredient");

            migrationBuilder.DropColumn(
                name: "InCart",
                table: "ShoppingIngredient");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ShoppingIngredient");

            migrationBuilder.RenameColumn(
                name: "QuantityString",
                table: "ShoppingIngredient",
                newName: "Measurement");
        }
    }
}
