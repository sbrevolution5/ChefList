using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMealWA.Server.Migrations
{
    public partial class _019chefssupppliesMany2Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChefSupply",
                columns: table => new
                {
                    ChefSuppliesId = table.Column<int>(type: "integer", nullable: false),
                    ChefsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChefSupply", x => new { x.ChefSuppliesId, x.ChefsId });
                    table.ForeignKey(
                        name: "FK_ChefSupply_AspNetUsers_ChefsId",
                        column: x => x.ChefsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChefSupply_Supply_ChefSuppliesId",
                        column: x => x.ChefSuppliesId,
                        principalTable: "Supply",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChefSupply_ChefsId",
                table: "ChefSupply",
                column: "ChefsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChefSupply");
        }
    }
}
