using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGuardiansEyesData.Migrations
{
    /// <inheritdoc />
    public partial class AddImpacto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Impactos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ImpactoClassificacaoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impactos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Impactos_ImpactosClassificacao_ImpactoClassificacaoId",
                        column: x => x.ImpactoClassificacaoId,
                        principalTable: "ImpactosClassificacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Impactos_ImpactoClassificacaoId",
                table: "Impactos",
                column: "ImpactoClassificacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Impactos");
        }
    }
}
