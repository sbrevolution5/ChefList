using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMealWA.Server.Migrations
{
    public partial class _023shoppingIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Fraction",
                table: "ShoppingIngredient",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "ShoppingIngredient",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MassMeasurementUnit",
                table: "ShoppingIngredient",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeasurementType",
                table: "ShoppingIngredient",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityNumber",
                table: "ShoppingIngredient",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VolumeMeasurementUnit",
                table: "ShoppingIngredient",
                type: "integer",
                nullable: true);

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
                onDelete: ReferentialAction.Restrict);
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
                name: "Fraction",
                table: "ShoppingIngredient");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "ShoppingIngredient");

            migrationBuilder.DropColumn(
                name: "MassMeasurementUnit",
                table: "ShoppingIngredient");

            migrationBuilder.DropColumn(
                name: "MeasurementType",
                table: "ShoppingIngredient");

            migrationBuilder.DropColumn(
                name: "QuantityNumber",
                table: "ShoppingIngredient");

            migrationBuilder.DropColumn(
                name: "VolumeMeasurementUnit",
                table: "ShoppingIngredient");
        }
    }
}
