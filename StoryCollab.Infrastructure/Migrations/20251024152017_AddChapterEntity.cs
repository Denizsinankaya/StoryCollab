using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryCollab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChapterEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Chapters",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublished",
                table: "Chapters",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VersionNumber",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_AuthorId",
                table: "Chapters",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Users_AuthorId",
                table: "Chapters",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Users_AuthorId",
                table: "Chapters");

            migrationBuilder.DropIndex(
                name: "IX_Chapters_AuthorId",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "VersionNumber",
                table: "Chapters");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Chapters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublished",
                table: "Chapters",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
