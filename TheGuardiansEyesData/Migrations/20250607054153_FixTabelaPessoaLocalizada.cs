using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGuardiansEyesData.Migrations
{
    /// <inheritdoc />
    public partial class FixTabelaPessoaLocalizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataHoraLocalizacao",
                table: "PessoasLocalizadas",
                newName: "DataHora");

            migrationBuilder.AddColumn<int>(
                name: "IdImpactoClassificacao",
                table: "PessoasLocalizadas",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PessoasLocalizadas_IdImpactoClassificacao",
                table: "PessoasLocalizadas",
                column: "IdImpactoClassificacao");

            migrationBuilder.AddForeignKey(
                name: "FK_PessoasLocalizadas_ImpactosClassificacao_IdImpactoClassificacao",
                table: "PessoasLocalizadas",
                column: "IdImpactoClassificacao",
                principalTable: "ImpactosClassificacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PessoasLocalizadas_ImpactosClassificacao_IdImpactoClassificacao",
                table: "PessoasLocalizadas");

            migrationBuilder.DropIndex(
                name: "IX_PessoasLocalizadas_IdImpactoClassificacao",
                table: "PessoasLocalizadas");

            migrationBuilder.DropColumn(
                name: "IdImpactoClassificacao",
                table: "PessoasLocalizadas");

            migrationBuilder.RenameColumn(
                name: "DataHora",
                table: "PessoasLocalizadas",
                newName: "DataHoraLocalizacao");
        }
    }
}
