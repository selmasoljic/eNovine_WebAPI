using Microsoft.EntityFrameworkCore.Migrations;

namespace MyApp.Migrations
{
    public partial class izmjena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "Vijest",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Korisnik",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vijest_KorisnikId",
                table: "Vijest",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vijest_Korisnik_KorisnikId",
                table: "Vijest",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vijest_Korisnik_KorisnikId",
                table: "Vijest");

            migrationBuilder.DropIndex(
                name: "IX_Vijest_KorisnikId",
                table: "Vijest");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Vijest");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Korisnik");
        }
    }
}
