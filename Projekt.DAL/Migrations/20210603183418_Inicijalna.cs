using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt.DAL.Migrations
{
    public partial class Inicijalna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Igraci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pozicija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nacionalnost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferiranaNoga = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    VrijednostUMilijunima = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igraci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klubovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeKluba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojBodova = table.Column<int>(type: "int", nullable: false),
                    GolRazlika = table.Column<int>(type: "int", nullable: false),
                    Trener = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stadion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojTrofeja = table.Column<int>(type: "int", nullable: false),
                    GodinaOsnutka = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klubovi", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Igraci");

            migrationBuilder.DropTable(
                name: "Klubovi");
        }
    }
}
