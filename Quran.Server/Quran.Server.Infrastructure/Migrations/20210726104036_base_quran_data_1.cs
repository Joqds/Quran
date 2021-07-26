using Microsoft.EntityFrameworkCore.Migrations;

namespace Quran.Server.Infrastructure.Migrations
{
    public partial class base_quran_data_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sovar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Page = table.Column<int>(nullable: false),
                    PlaceOfRevelationType = table.Column<int>(nullable: false),
                    RevelationSequenceNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sovar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ayat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    SurahId = table.Column<int>(nullable: false),
                    RubId = table.Column<int>(nullable: false),
                    AyahInSurah = table.Column<int>(nullable: false),
                    AyahInRub = table.Column<int>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    WordCount = table.Column<int>(nullable: false),
                    LetterCount = table.Column<int>(nullable: false),
                    SajdahType = table.Column<int>(nullable: false),
                    SajdahReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ayat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ayat_Sovar_SurahId",
                        column: x => x.SurahId,
                        principalTable: "Sovar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Arba",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Joz = table.Column<int>(nullable: false),
                    RubInJoz = table.Column<int>(nullable: false),
                    FirstAyahId = table.Column<int>(nullable: false),
                    LastAyahId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arba", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arba_Ayat_FirstAyahId",
                        column: x => x.FirstAyahId,
                        principalTable: "Ayat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arba_Ayat_LastAyahId",
                        column: x => x.LastAyahId,
                        principalTable: "Ayat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arba_FirstAyahId",
                table: "Arba",
                column: "FirstAyahId");

            migrationBuilder.CreateIndex(
                name: "IX_Arba_LastAyahId",
                table: "Arba",
                column: "LastAyahId");

            migrationBuilder.CreateIndex(
                name: "IX_Ayat_RubId",
                table: "Ayat",
                column: "RubId");

            migrationBuilder.CreateIndex(
                name: "IX_Ayat_SurahId",
                table: "Ayat",
                column: "SurahId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ayah_Rub",
                table: "Ayat",
                column: "RubId",
                principalTable: "Arba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arba_Ayat_FirstAyahId",
                table: "Arba");

            migrationBuilder.DropForeignKey(
                name: "FK_Arba_Ayat_LastAyahId",
                table: "Arba");

            migrationBuilder.DropTable(
                name: "Ayat");

            migrationBuilder.DropTable(
                name: "Arba");

            migrationBuilder.DropTable(
                name: "Sovar");
        }
    }
}
