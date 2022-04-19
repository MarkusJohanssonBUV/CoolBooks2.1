using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolBooks.Migrations
{
    public partial class Addcomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBookOfTheWeek",
                table: "Books",
                type: "bit",
                nullable: true,
                defaultValue: false);

            //migrationBuilder.AlterColumn<string>(
            //    name: "LastName",
            //    table: "AspNetUsers",
            //    type: "nvarchar(255)",
            //    maxLength: 255,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(255)",
            //    oldMaxLength: 255);

            //migrationBuilder.AlterColumn<string>(
            //    name: "FirstName",
            //    table: "AspNetUsers",
            //    type: "nvarchar(255)",
            //    maxLength: 255,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(255)",
            //    oldMaxLength: 255);

            migrationBuilder.CreateTable(
                name: "ReviewComents",
                columns: table => new
                {
                    ReviewComentsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    React = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewsID = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewComents", x => x.ReviewComentsID);
                    table.ForeignKey(
                        name: "FK_ReviewComents_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewComents_Reviews_ReviewsID",
                        column: x => x.ReviewsID,
                        principalTable: "Reviews",
                        principalColumn: "ReviewsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComents_ClientId",
                table: "ReviewComents",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComents_ReviewsID",
                table: "ReviewComents",
                column: "ReviewsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewComents");

            migrationBuilder.DropColumn(
                name: "IsBookOfTheWeek",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
