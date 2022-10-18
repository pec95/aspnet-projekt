using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt.DAL.Migrations
{
    public partial class UpdejtAuth2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NajKlub",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "KlubIdNajdrazi",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_KlubIdNajdrazi",
                table: "AspNetUsers",
                column: "KlubIdNajdrazi");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Klubovi_KlubIdNajdrazi",
                table: "AspNetUsers",
                column: "KlubIdNajdrazi",
                principalTable: "Klubovi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Klubovi_KlubIdNajdrazi",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_KlubIdNajdrazi",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KlubIdNajdrazi",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "NajKlub",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
