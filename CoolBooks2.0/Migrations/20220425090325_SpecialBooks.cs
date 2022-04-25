using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolBooks.Migrations
{
    public partial class SpecialBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MostCommetedBook",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MostDislikedBook",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MostLikedBook",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NewestBook",
                table: "Books",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MostCommetedBook",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "MostDislikedBook",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "MostLikedBook",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "NewestBook",
                table: "Books");
        }
    }
}
