using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt.DAL.Migrations
{
    public partial class UpdejtSlike2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klubovi_Slika_idSlike",
                table: "Klubovi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slika",
                table: "Slika");

            migrationBuilder.RenameTable(
                name: "Slika",
                newName: "Slike");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slike",
                table: "Slike",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Klubovi_Slike_idSlike",
                table: "Klubovi",
                column: "idSlike",
                principalTable: "Slike",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klubovi_Slike_idSlike",
                table: "Klubovi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slike",
                table: "Slike");

            migrationBuilder.RenameTable(
                name: "Slike",
                newName: "Slika");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slika",
                table: "Slika",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Klubovi_Slika_idSlike",
                table: "Klubovi",
                column: "idSlike",
                principalTable: "Slika",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
