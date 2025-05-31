using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGuardiansEyesData.Migrations
{
    /// <inheritdoc />
    public partial class FixLocalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Locais",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "Municipio",
                table: "Locais",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "Endereco",
                table: "Locais",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "Locais",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Locais",
                type: "BINARY_DOUBLE",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Locais",
                type: "BINARY_DOUBLE",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "DroneModelId",
                table: "ImagensCapturadas",
                type: "NUMBER(10)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdImpactoClassificacao",
                table: "ImagensCapturadas",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdLocal",
                table: "ImagensCapturadas",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ImagensCapturadas_DroneModelId",
                table: "ImagensCapturadas",
                column: "DroneModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensCapturadas_IdImpactoClassificacao",
                table: "ImagensCapturadas",
                column: "IdImpactoClassificacao");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensCapturadas_IdLocal",
                table: "ImagensCapturadas",
                column: "IdLocal");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensCapturadas_Drones_DroneModelId",
                table: "ImagensCapturadas",
                column: "DroneModelId",
                principalTable: "Drones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensCapturadas_Impactos_IdImpactoClassificacao",
                table: "ImagensCapturadas",
                column: "IdImpactoClassificacao",
                principalTable: "Impactos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensCapturadas_Locais_IdLocal",
                table: "ImagensCapturadas",
                column: "IdLocal",
                principalTable: "Locais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensCapturadas_Drones_DroneModelId",
                table: "ImagensCapturadas");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagensCapturadas_Impactos_IdImpactoClassificacao",
                table: "ImagensCapturadas");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagensCapturadas_Locais_IdLocal",
                table: "ImagensCapturadas");

            migrationBuilder.DropIndex(
                name: "IX_ImagensCapturadas_DroneModelId",
                table: "ImagensCapturadas");

            migrationBuilder.DropIndex(
                name: "IX_ImagensCapturadas_IdImpactoClassificacao",
                table: "ImagensCapturadas");

            migrationBuilder.DropIndex(
                name: "IX_ImagensCapturadas_IdLocal",
                table: "ImagensCapturadas");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Locais");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Locais");

            migrationBuilder.DropColumn(
                name: "DroneModelId",
                table: "ImagensCapturadas");

            migrationBuilder.DropColumn(
                name: "IdImpactoClassificacao",
                table: "ImagensCapturadas");

            migrationBuilder.DropColumn(
                name: "IdLocal",
                table: "ImagensCapturadas");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Locais",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Municipio",
                table: "Locais",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco",
                table: "Locais",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "Locais",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);
        }
    }
}
