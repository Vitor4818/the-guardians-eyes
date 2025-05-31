using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGuardiansEyesData.Migrations
{
    /// <inheritdoc />
    public partial class FixDesastreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desastres_ImpactosClassificacao_IdImpactoClassificacao",
                table: "Desastres");

            migrationBuilder.RenameColumn(
                name: "IdImpactoClassificacao",
                table: "Desastres",
                newName: "IdImpacto");

            migrationBuilder.RenameIndex(
                name: "IX_Desastres_IdImpactoClassificacao",
                table: "Desastres",
                newName: "IX_Desastres_IdImpacto");

            migrationBuilder.AddForeignKey(
                name: "FK_Desastres_Impactos_IdImpacto",
                table: "Desastres",
                column: "IdImpacto",
                principalTable: "Impactos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desastres_Impactos_IdImpacto",
                table: "Desastres");

            migrationBuilder.RenameColumn(
                name: "IdImpacto",
                table: "Desastres",
                newName: "IdImpactoClassificacao");

            migrationBuilder.RenameIndex(
                name: "IX_Desastres_IdImpacto",
                table: "Desastres",
                newName: "IX_Desastres_IdImpactoClassificacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Desastres_ImpactosClassificacao_IdImpactoClassificacao",
                table: "Desastres",
                column: "IdImpactoClassificacao",
                principalTable: "ImpactosClassificacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
