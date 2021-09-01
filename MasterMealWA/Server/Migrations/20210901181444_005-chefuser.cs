using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMealWA.Server.Migrations
{
    public partial class _005chefuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Chef_ChefId1",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Chef_ChefId1",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Chef_ChefId1",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Chef_AuthorId1",
                table: "Recipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingList_Chef_ChefId1",
                table: "ShoppingList");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingList_ChefId1",
                table: "ShoppingList");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_AuthorId1",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Rating_ChefId1",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Meal_ChefId1",
                table: "Meal");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ChefId1",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ChefId1",
                table: "ShoppingList");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "ChefId1",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "ChefId1",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "ChefId1",
                table: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chef",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Chef",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Chef",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Chef",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Chef",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Chef",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Chef",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Chef",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Chef",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Chef",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Chef",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Chef",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Chef",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Chef",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Chef",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingList_ChefId",
                table: "ShoppingList",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_AuthorId",
                table: "Recipe",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ChefId",
                table: "Rating",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_ChefId",
                table: "Meal",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ChefId",
                table: "Comment",
                column: "ChefId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Recipe_Chef_AuthorId",
                table: "Recipe");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingList_Chef_ChefId",
                table: "ShoppingList");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingList_ChefId",
                table: "ShoppingList");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_AuthorId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Rating_ChefId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Meal_ChefId",
                table: "Meal");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ChefId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Chef");

            migrationBuilder.AddColumn<Guid>(
                name: "ChefId1",
                table: "ShoppingList",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId1",
                table: "Recipe",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChefId1",
                table: "Rating",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChefId1",
                table: "Meal",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChefId1",
                table: "Comment",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Chef",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingList_ChefId1",
                table: "ShoppingList",
                column: "ChefId1");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_AuthorId1",
                table: "Recipe",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ChefId1",
                table: "Rating",
                column: "ChefId1");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_ChefId1",
                table: "Meal",
                column: "ChefId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ChefId1",
                table: "Comment",
                column: "ChefId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Chef_ChefId1",
                table: "Comment",
                column: "ChefId1",
                principalTable: "Chef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Chef_ChefId1",
                table: "Meal",
                column: "ChefId1",
                principalTable: "Chef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Chef_ChefId1",
                table: "Rating",
                column: "ChefId1",
                principalTable: "Chef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Chef_AuthorId1",
                table: "Recipe",
                column: "AuthorId1",
                principalTable: "Chef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingList_Chef_ChefId1",
                table: "ShoppingList",
                column: "ChefId1",
                principalTable: "Chef",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
