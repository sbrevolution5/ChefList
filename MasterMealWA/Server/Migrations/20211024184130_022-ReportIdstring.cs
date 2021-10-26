using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMealWA.Server.Migrations
{
    public partial class _022ReportIdstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_AspNetUsers_SubmitterId1",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_SubmitterId1",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "SubmitterId1",
                table: "Report");

            migrationBuilder.AlterColumn<string>(
                name: "SubmitterId",
                table: "Report",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Report_SubmitterId",
                table: "Report",
                column: "SubmitterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_AspNetUsers_SubmitterId",
                table: "Report",
                column: "SubmitterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_AspNetUsers_SubmitterId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_SubmitterId",
                table: "Report");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubmitterId",
                table: "Report",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmitterId1",
                table: "Report",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_SubmitterId1",
                table: "Report",
                column: "SubmitterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_AspNetUsers_SubmitterId1",
                table: "Report",
                column: "SubmitterId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
