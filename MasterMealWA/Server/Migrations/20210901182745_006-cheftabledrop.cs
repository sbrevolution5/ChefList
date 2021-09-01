using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMealWA.Server.Migrations
{
    public partial class _006cheftabledrop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Chef_ChefId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Chef_ChefId",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Chef_ChefId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_AspNetUsers_ApplicationUserId",
                table: "Recipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Chef_AuthorId",
                table: "Recipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingList_Chef_ChefId",
                table: "ShoppingList");

            migrationBuilder.DropTable(
                name: "Chef");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_ApplicationUserId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Recipe");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_ChefId",
                table: "Comment",
                column: "ChefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_AspNetUsers_ChefId",
                table: "Meal",
                column: "ChefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_ChefId",
                table: "Rating",
                column: "ChefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_AspNetUsers_AuthorId",
                table: "Recipe",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingList_AspNetUsers_ChefId",
                table: "ShoppingList",
                column: "ChefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_ChefId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_AspNetUsers_ChefId",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_ChefId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_AspNetUsers_AuthorId",
                table: "Recipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingList_AspNetUsers_ChefId",
                table: "ShoppingList");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Recipe",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chef",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    ImageId = table.Column<int>(type: "integer", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    ScreenName = table.Column<string>(type: "text", nullable: false),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ShowFullName = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chef", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chef_DBImage_ImageId",
                        column: x => x.ImageId,
                        principalTable: "DBImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_ApplicationUserId",
                table: "Recipe",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chef_ImageId",
                table: "Chef",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Chef_ChefId",
                table: "Comment",
                column: "ChefId",
                principalTable: "Chef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Chef_ChefId",
                table: "Meal",
                column: "ChefId",
                principalTable: "Chef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Chef_ChefId",
                table: "Rating",
                column: "ChefId",
                principalTable: "Chef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_AspNetUsers_ApplicationUserId",
                table: "Recipe",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Chef_AuthorId",
                table: "Recipe",
                column: "AuthorId",
                principalTable: "Chef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingList_Chef_ChefId",
                table: "ShoppingList",
                column: "ChefId",
                principalTable: "Chef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
