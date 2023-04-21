using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekt.Migrations
{
    /// <inheritdoc />
    public partial class Migra1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Firma",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NIP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Konto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Haslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypKonta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PracownikId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Magazyn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Miasto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Powierzchnia = table.Column<double>(type: "float", nullable: false),
                    PojemnoscPaczek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magazyn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pracownik",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KontoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pracownik_Konto_KontoId",
                        column: x => x.KontoId,
                        principalTable: "Konto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WozekWidlowy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataOstatniegoSerwisu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MagazynId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WozekWidlowy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WozekWidlowy_Magazyn_MagazynId",
                        column: x => x.MagazynId,
                        principalTable: "Magazyn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Praca",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CzasRozpoczecia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CzasZakonczenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WozekWidlowyId = table.Column<long>(type: "bigint", nullable: false),
                    KontoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Praca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Praca_Konto_KontoId",
                        column: x => x.KontoId,
                        principalTable: "Konto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Praca_WozekWidlowy_WozekWidlowyId",
                        column: x => x.WozekWidlowyId,
                        principalTable: "WozekWidlowy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paczka",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Wlasciciel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirmaId = table.Column<long>(type: "bigint", nullable: false),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MagazynId = table.Column<long>(type: "bigint", nullable: false),
                    PracaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paczka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paczka_Firma_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paczka_Magazyn_MagazynId",
                        column: x => x.MagazynId,
                        principalTable: "Magazyn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paczka_Praca_PracaId",
                        column: x => x.PracaId,
                        principalTable: "Praca",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paczka_FirmaId",
                table: "Paczka",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Paczka_MagazynId",
                table: "Paczka",
                column: "MagazynId");

            migrationBuilder.CreateIndex(
                name: "IX_Paczka_PracaId",
                table: "Paczka",
                column: "PracaId");

            migrationBuilder.CreateIndex(
                name: "IX_Praca_KontoId",
                table: "Praca",
                column: "KontoId");

            migrationBuilder.CreateIndex(
                name: "IX_Praca_WozekWidlowyId",
                table: "Praca",
                column: "WozekWidlowyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownik_KontoId",
                table: "Pracownik",
                column: "KontoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WozekWidlowy_MagazynId",
                table: "WozekWidlowy",
                column: "MagazynId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paczka");

            migrationBuilder.DropTable(
                name: "Pracownik");

            migrationBuilder.DropTable(
                name: "Firma");

            migrationBuilder.DropTable(
                name: "Praca");

            migrationBuilder.DropTable(
                name: "Konto");

            migrationBuilder.DropTable(
                name: "WozekWidlowy");

            migrationBuilder.DropTable(
                name: "Magazyn");
        }
    }
}
