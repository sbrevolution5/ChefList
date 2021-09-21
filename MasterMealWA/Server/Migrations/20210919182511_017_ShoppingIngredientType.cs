using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMealWA.Server.Migrations
{
    public partial class _017_ShoppingIngredientType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingIngredient_Ingredient_IngredientId",
                table: "ShoppingIngredient");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "ShoppingIngredient",
                newName: "IngredientTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingIngredient_IngredientId",
                table: "ShoppingIngredient",
                newName: "IX_ShoppingIngredient_IngredientTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingIngredient_IngredientType_IngredientTypeId",
                table: "ShoppingIngredient",
                column: "IngredientTypeId",
                principalTable: "IngredientType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingIngredient_IngredientType_IngredientTypeId",
                table: "ShoppingIngredient");

            migrationBuilder.RenameColumn(
                name: "IngredientTypeId",
                table: "ShoppingIngredient",
                newName: "IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingIngredient_IngredientTypeId",
                table: "ShoppingIngredient",
                newName: "IX_ShoppingIngredient_IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingIngredient_Ingredient_IngredientId",
                table: "ShoppingIngredient",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
