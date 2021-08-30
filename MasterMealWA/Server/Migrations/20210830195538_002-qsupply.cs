using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MasterMealWA.Server.Migrations
{
    public partial class _002qsupply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeSupply");

            migrationBuilder.CreateTable(
                name: "QSupply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplyId = table.Column<int>(type: "integer", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QSupply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QSupply_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QSupply_Supply_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Supply",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QSupply_RecipeId",
                table: "QSupply",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_QSupply_SupplyId",
                table: "QSupply",
                column: "SupplyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QSupply");

            migrationBuilder.CreateTable(
                name: "RecipeSupply",
                columns: table => new
                {
                    RecipesId = table.Column<int>(type: "integer", nullable: false),
                    SuppliesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeSupply", x => new { x.RecipesId, x.SuppliesId });
                    table.ForeignKey(
                        name: "FK_RecipeSupply_Recipe_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeSupply_Supply_SuppliesId",
                        column: x => x.SuppliesId,
                        principalTable: "Supply",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSupply_SuppliesId",
                table: "RecipeSupply",
                column: "SuppliesId");
        }
    }
}
