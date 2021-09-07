using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMealWA.Server.Migrations
{
    public partial class _009tableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeRecipeTag_RecipeType_TagsId",
                table: "RecipeRecipeTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeType",
                table: "RecipeType");

            migrationBuilder.RenameTable(
                name: "RecipeType",
                newName: "RecipeTag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeTag",
                table: "RecipeTag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeRecipeTag_RecipeTag_TagsId",
                table: "RecipeRecipeTag",
                column: "TagsId",
                principalTable: "RecipeTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeRecipeTag_RecipeTag_TagsId",
                table: "RecipeRecipeTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeTag",
                table: "RecipeTag");

            migrationBuilder.RenameTable(
                name: "RecipeTag",
                newName: "RecipeType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeType",
                table: "RecipeType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeRecipeTag_RecipeType_TagsId",
                table: "RecipeRecipeTag",
                column: "TagsId",
                principalTable: "RecipeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
