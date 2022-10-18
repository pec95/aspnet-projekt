using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt.DAL.Migrations
{
    public partial class UpdejtSlike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Klubovi",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Klubovi",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Klubovi",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Klubovi",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Klubovi",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Klubovi",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Klubovi",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Klubovi",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Klubovi",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Klubovi",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AddColumn<int>(
                name: "idSlike",
                table: "Klubovi",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KlubId",
                table: "Igraci",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Slika",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivSlike = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PodaciSlike = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slika", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Klubovi_idSlike",
                table: "Klubovi",
                column: "idSlike");

            migrationBuilder.CreateIndex(
                name: "IX_Igraci_KlubId",
                table: "Igraci",
                column: "KlubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Igraci_Klubovi_KlubId",
                table: "Igraci",
                column: "KlubId",
                principalTable: "Klubovi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Klubovi_Slika_idSlike",
                table: "Klubovi",
                column: "idSlike",
                principalTable: "Slika",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Igraci_Klubovi_KlubId",
                table: "Igraci");

            migrationBuilder.DropForeignKey(
                name: "FK_Klubovi_Slika_idSlike",
                table: "Klubovi");

            migrationBuilder.DropTable(
                name: "Slika");

            migrationBuilder.DropIndex(
                name: "IX_Klubovi_idSlike",
                table: "Klubovi");

            migrationBuilder.DropIndex(
                name: "IX_Igraci_KlubId",
                table: "Igraci");

            migrationBuilder.DropColumn(
                name: "idSlike",
                table: "Klubovi");

            migrationBuilder.DropColumn(
                name: "KlubId",
                table: "Igraci");

            migrationBuilder.InsertData(
                table: "Klubovi",
                columns: new[] { "Id", "BrojBodova", "BrojTrofeja", "GodinaOsnutka", "GolRazlika", "ImeKluba", "Stadion", "Trener" },
                values: new object[,]
                {
                    { 1, 85, 38, 1911, 56, "GNK Dinamo Zagreb", "Maksimir", "Damir Krznar" },
                    { 2, 77, 1, 1947, 34, "NK Osijek", "Gradski Vrt", "Nenad Bjelica" },
                    { 3, 61, 7, 1946, 5, "HNK Rijeka", "Rujevica", "Goran Tomić" },
                    { 4, 60, 14, 1911, 11, "HNK Hajduk Split", "Poljud", "Jens Gustafsson" },
                    { 5, 59, 0, 2009, 13, "HNK Gorica", "SRC Velika Gorica", "Krunoslav Rendulić" },
                    { 6, 35, 0, 1932, -15, "HNK Šibenik", "Šubićevac", "Ivan Močinić" },
                    { 7, 34, 0, 1907, -17, "NK Slaven Belupo Koprivnica", "Gradski stadion Ivan Kušek-Apaš", "Tomislav Stipić" },
                    { 8, 30, 0, 1914, -31, "NK Lokomotiva Zagreb", "Kranjčevićeva", "" },
                    { 9, 29, 0, 1948, -25, "NK Istra 1961", "Aldo Drosina", "Danijel Jumić" },
                    { 10, 28, 0, 2012, -31, "NK Varaždin", "Stadion Varteks", "" }
                });
        }
    }
}
