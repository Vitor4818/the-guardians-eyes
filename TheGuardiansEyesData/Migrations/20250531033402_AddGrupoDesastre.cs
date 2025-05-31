using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGuardiansEyesData.Migrations
{
    /// <inheritdoc />
    public partial class AddGrupoDesastre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubGruposDesastre_GruposDesastre_GrupoDesastreId",
                table: "SubGruposDesastre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubGruposDesastre",
                table: "SubGruposDesastre");

            migrationBuilder.RenameTable(
                name: "SubGruposDesastre",
                newName: "SubGrupoDesastre");

            migrationBuilder.RenameIndex(
                name: "IX_SubGruposDesastre_GrupoDesastreId",
                table: "SubGrupoDesastre",
                newName: "IX_SubGrupoDesastre_GrupoDesastreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubGrupoDesastre",
                table: "SubGrupoDesastre",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubGrupoDesastre_GruposDesastre_GrupoDesastreId",
                table: "SubGrupoDesastre",
                column: "GrupoDesastreId",
                principalTable: "GruposDesastre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubGrupoDesastre_GruposDesastre_GrupoDesastreId",
                table: "SubGrupoDesastre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubGrupoDesastre",
                table: "SubGrupoDesastre");

            migrationBuilder.RenameTable(
                name: "SubGrupoDesastre",
                newName: "SubGruposDesastre");

            migrationBuilder.RenameIndex(
                name: "IX_SubGrupoDesastre_GrupoDesastreId",
                table: "SubGruposDesastre",
                newName: "IX_SubGruposDesastre_GrupoDesastreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubGruposDesastre",
                table: "SubGruposDesastre",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubGruposDesastre_GruposDesastre_GrupoDesastreId",
                table: "SubGruposDesastre",
                column: "GrupoDesastreId",
                principalTable: "GruposDesastre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
