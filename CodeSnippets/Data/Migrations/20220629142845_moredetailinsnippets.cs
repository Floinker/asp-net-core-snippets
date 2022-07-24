using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSnippets.Data.Migrations
{
    public partial class moredetailinsnippets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Snippet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Snippet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Snippet");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Snippet");
        }
    }
}
