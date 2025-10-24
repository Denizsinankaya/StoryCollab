using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryCollab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStoryEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Stories");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "StoryContributors",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "Contributor");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Stories",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Stories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stories",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stories_OwnerId",
                table: "Stories",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Users_OwnerId",
                table: "Stories",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Users_OwnerId",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Stories_OwnerId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "StoryContributors");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Stories");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Stories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
