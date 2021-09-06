using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMealWA.Server.Migrations
{
    public partial class _010removedisplayName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
