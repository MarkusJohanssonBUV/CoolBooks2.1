using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolBooks.Migrations
{
    public partial class QuoteBookIdNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Books_BookID",
                table: "Quotes");

            migrationBuilder.AlterColumn<int>(
                name: "BookID",
                table: "Quotes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Books_BookID",
                table: "Quotes",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BooksID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Books_BookID",
                table: "Quotes");

            migrationBuilder.AlterColumn<int>(
                name: "BookID",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Books_BookID",
                table: "Quotes",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BooksID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
