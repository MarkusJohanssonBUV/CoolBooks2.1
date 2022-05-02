using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolBooks.Migrations
{
    public partial class Quotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    QuoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsQuoteModerated = table.Column<bool>(type: "bit", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.QuoteId);
                    table.ForeignKey(
                        name: "FK_Quotes_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotes_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BooksID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_AuthorID",
                table: "Quotes",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_BookID",
                table: "Quotes",
                column: "BookID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");
        }
    }
}
