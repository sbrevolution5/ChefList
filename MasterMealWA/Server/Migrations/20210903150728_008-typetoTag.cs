 musing Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMealWA.Server.Migrations
{
    public partial class _008typetoTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_RecipeType_TypeId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_TypeId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Recipe");

            migrationBuilder.CreateTable(
                name: "RecipeRecipeTag",
                columns: table => new
                {
                    RecipesId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeRecipeTag", x => new { x.RecipesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_RecipeRecipeTag_Recipe_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeRecipeTag_RecipeType_TagsId",
                        column: x => x.TagsId,
                        principalTable: "RecipeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRecipeTag_TagsId",
                table: "RecipeRecipeTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeRecipeTag");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Recipe",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_TypeId",
                table: "Recipe",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_RecipeType_TypeId",
                table: "Recipe",
                column: "TypeId",
                principalTable: "RecipeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
