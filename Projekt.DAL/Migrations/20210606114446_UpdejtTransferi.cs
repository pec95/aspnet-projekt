using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt.DAL.Migrations
{
    public partial class UpdejtTransferi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transferi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMaticnogKluba = table.Column<int>(type: "int", nullable: true),
                    IdDolaznogKluba = table.Column<int>(type: "int", nullable: true),
                    IdIgraca = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transferi_Igraci_IdIgraca",
                        column: x => x.IdIgraca,
                        principalTable: "Igraci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transferi_Klubovi_IdDolaznogKluba",
                        column: x => x.IdDolaznogKluba,
                        principalTable: "Klubovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transferi_Klubovi_IdMaticnogKluba",
                        column: x => x.IdMaticnogKluba,
                        principalTable: "Klubovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transferi_IdDolaznogKluba",
                table: "Transferi",
                column: "IdDolaznogKluba");

            migrationBuilder.CreateIndex(
                name: "IX_Transferi_IdIgraca",
                table: "Transferi",
                column: "IdIgraca");

            migrationBuilder.CreateIndex(
                name: "IX_Transferi_IdMaticnogKluba",
                table: "Transferi",
                column: "IdMaticnogKluba");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transferi");
        }
    }
}
