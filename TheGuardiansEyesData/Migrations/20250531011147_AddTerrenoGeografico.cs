using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheGuardiansEyesData.Migrations
{
    /// <inheritdoc />
    public partial class AddTerrenoGeografico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TerrenoGeograficoModelId",
                table: "Desastres",
                type: "NUMBER(10)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "TerrenosGeograficos",
                columns: new[] { "Id", "NomeTerreno" },
                values: new object[,]
                {
                    { 1, "Montanha" },
                    { 2, "Planície" },
                    { 3, "Floresta" },
                    { 4, "Área Urbana" },
                    { 5, "Deserto" },
                    { 6, "Pantanal" },
                    { 7, "Litoral" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desastres_TerrenoGeograficoModelId",
                table: "Desastres",
                column: "TerrenoGeograficoModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Desastres_TerrenosGeograficos_TerrenoGeograficoModelId",
                table: "Desastres",
                column: "TerrenoGeograficoModelId",
                principalTable: "TerrenosGeograficos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desastres_TerrenosGeograficos_TerrenoGeograficoModelId",
                table: "Desastres");

            migrationBuilder.DropIndex(
                name: "IX_Desastres_TerrenoGeograficoModelId",
                table: "Desastres");

            migrationBuilder.DeleteData(
                table: "TerrenosGeograficos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TerrenosGeograficos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TerrenosGeograficos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TerrenosGeograficos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TerrenosGeograficos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TerrenosGeograficos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TerrenosGeograficos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "TerrenoGeograficoModelId",
                table: "Desastres");
        }
    }
}
