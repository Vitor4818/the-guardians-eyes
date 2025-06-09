using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGuardiansEyesData.Migrations
{
    /// <inheritdoc />
    public partial class FixWarnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desastres_Impactos_Impacto",
                table: "Desastres");

            migrationBuilder.RenameColumn(
                name: "Impacto",
                table: "Desastres",
                newName: "IdImpactoClassificacao");

            migrationBuilder.RenameIndex(
                name: "IX_Desastres_Impacto",
                table: "Desastres",
                newName: "IX_Desastres_IdImpactoClassificacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Desastres_Impactos_IdImpactoClassificacao",
                table: "Desastres",
                column: "IdImpactoClassificacao",
                principalTable: "Impactos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desastres_Impactos_IdImpactoClassificacao",
                table: "Desastres");

            migrationBuilder.RenameColumn(
                name: "IdImpactoClassificacao",
                table: "Desastres",
                newName: "Impacto");

            migrationBuilder.RenameIndex(
                name: "IX_Desastres_IdImpactoClassificacao",
                table: "Desastres",
                newName: "IX_Desastres_Impacto");

            migrationBuilder.AddForeignKey(
                name: "FK_Desastres_Impactos_Impacto",
                table: "Desastres",
                column: "Impacto",
                principalTable: "Impactos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
