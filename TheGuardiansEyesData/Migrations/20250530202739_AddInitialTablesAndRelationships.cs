using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheGuardiansEyesData.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialTablesAndRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sobrenome",
                table: "Usuarios",
                newName: "Sobrenome");

            migrationBuilder.RenameColumn(
                name: "senha",
                table: "Usuarios",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Usuarios",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "funcao",
                table: "Usuarios",
                newName: "Funcao");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "cpf",
                table: "Usuarios",
                newName: "Cpf");

            migrationBuilder.RenameColumn(
                name: "cargo",
                table: "Usuarios",
                newName: "Cargo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Drones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Fabricante = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Modelo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TempoVoo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DistanciaMaxima = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    VelocidadeMaxima = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Camera = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Peso = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GruposDesastre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeGrupo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposDesastre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImpactosClassificacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nivel = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DescNivel = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpactosClassificacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Cep = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Municipio = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Numero = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sensores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Chip = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Modelo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Interface = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Utilidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Fabricante = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Estado = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Saida = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TipoSaida = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MediaTensaoRegistrada = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TerrenosGeograficos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeTerreno = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerrenosGeograficos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImagensCapturadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Hospedagem = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Tamanho = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IdDrone = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagensCapturadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagensCapturadas_Drones_IdDrone",
                        column: x => x.IdDrone,
                        principalTable: "Drones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubGruposDesastre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Subgrupo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SubTipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    GrupoDesastreId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGruposDesastre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubGruposDesastre_GruposDesastre_GrupoDesastreId",
                        column: x => x.GrupoDesastreId,
                        principalTable: "GruposDesastre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Desastres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdLocal = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdImpactoClassificacao = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdGrupoDesastre = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Cobrade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataOcorrencia = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desastres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Desastres_GruposDesastre_IdGrupoDesastre",
                        column: x => x.IdGrupoDesastre,
                        principalTable: "GruposDesastre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desastres_ImpactosClassificacao_IdImpactoClassificacao",
                        column: x => x.IdImpactoClassificacao,
                        principalTable: "ImpactosClassificacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desastres_Locais_IdLocal",
                        column: x => x.IdLocal,
                        principalTable: "Locais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desastres_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ImpactosClassificacao",
                columns: new[] { "Id", "DescNivel", "Nivel" },
                values: new object[,]
                {
                    { 1, "Leve", 1 },
                    { 2, "Moderado", 2 },
                    { 3, "Grave", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desastres_IdGrupoDesastre",
                table: "Desastres",
                column: "IdGrupoDesastre");

            migrationBuilder.CreateIndex(
                name: "IX_Desastres_IdImpactoClassificacao",
                table: "Desastres",
                column: "IdImpactoClassificacao");

            migrationBuilder.CreateIndex(
                name: "IX_Desastres_IdLocal",
                table: "Desastres",
                column: "IdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Desastres_IdUsuario",
                table: "Desastres",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensCapturadas_IdDrone",
                table: "ImagensCapturadas",
                column: "IdDrone");

            migrationBuilder.CreateIndex(
                name: "IX_SubGruposDesastre_GrupoDesastreId",
                table: "SubGruposDesastre",
                column: "GrupoDesastreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Desastres");

            migrationBuilder.DropTable(
                name: "ImagensCapturadas");

            migrationBuilder.DropTable(
                name: "Sensores");

            migrationBuilder.DropTable(
                name: "SubGruposDesastre");

            migrationBuilder.DropTable(
                name: "TerrenosGeograficos");

            migrationBuilder.DropTable(
                name: "ImpactosClassificacao");

            migrationBuilder.DropTable(
                name: "Locais");

            migrationBuilder.DropTable(
                name: "Drones");

            migrationBuilder.DropTable(
                name: "GruposDesastre");

            migrationBuilder.RenameColumn(
                name: "Sobrenome",
                table: "Usuarios",
                newName: "sobrenome");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Usuarios",
                newName: "senha");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Usuarios",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Funcao",
                table: "Usuarios",
                newName: "funcao");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuarios",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Usuarios",
                newName: "cpf");

            migrationBuilder.RenameColumn(
                name: "Cargo",
                table: "Usuarios",
                newName: "cargo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuarios",
                newName: "id");
        }
    }
}
