using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheGuardiansEyesData.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Cep = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Municipio = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Numero = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
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
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Sobrenome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cargo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Funcao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubGrupoDesastre",
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
                    table.PrimaryKey("PK_SubGrupoDesastre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubGrupoDesastre_GruposDesastre_GrupoDesastreId",
                        column: x => x.GrupoDesastreId,
                        principalTable: "GruposDesastre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Desastres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdLocal = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Impacto = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdGrupoDesastre = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Cobrade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataOcorrencia = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TerrenoGeograficoModelId = table.Column<int>(type: "NUMBER(10)", nullable: true)
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
                        name: "FK_Desastres_Impactos_Impacto",
                        column: x => x.Impacto,
                        principalTable: "Impactos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desastres_Locais_IdLocal",
                        column: x => x.IdLocal,
                        principalTable: "Locais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desastres_TerrenosGeograficos_TerrenoGeograficoModelId",
                        column: x => x.TerrenoGeograficoModelId,
                        principalTable: "TerrenosGeograficos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Desastres_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagensCapturadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Hospedagem = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Tamanho = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IdLocal = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdImpactoClassificacao = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    IdDrone = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdDesastre = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DroneModelId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagensCapturadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagensCapturadas_Desastres_IdDesastre",
                        column: x => x.IdDesastre,
                        principalTable: "Desastres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagensCapturadas_Drones_DroneModelId",
                        column: x => x.DroneModelId,
                        principalTable: "Drones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImagensCapturadas_Drones_IdDrone",
                        column: x => x.IdDrone,
                        principalTable: "Drones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagensCapturadas_ImpactosClassificacao_IdImpactoClassificacao",
                        column: x => x.IdImpactoClassificacao,
                        principalTable: "ImpactosClassificacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImagensCapturadas_Locais_IdLocal",
                        column: x => x.IdLocal,
                        principalTable: "Locais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Desastres_IdGrupoDesastre",
                table: "Desastres",
                column: "IdGrupoDesastre");

            migrationBuilder.CreateIndex(
                name: "IX_Desastres_IdLocal",
                table: "Desastres",
                column: "IdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Desastres_IdUsuario",
                table: "Desastres",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Desastres_Impacto",
                table: "Desastres",
                column: "Impacto");

            migrationBuilder.CreateIndex(
                name: "IX_Desastres_TerrenoGeograficoModelId",
                table: "Desastres",
                column: "TerrenoGeograficoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensCapturadas_DroneModelId",
                table: "ImagensCapturadas",
                column: "DroneModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensCapturadas_IdDesastre",
                table: "ImagensCapturadas",
                column: "IdDesastre");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensCapturadas_IdDrone",
                table: "ImagensCapturadas",
                column: "IdDrone");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensCapturadas_IdImpactoClassificacao",
                table: "ImagensCapturadas",
                column: "IdImpactoClassificacao");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensCapturadas_IdLocal",
                table: "ImagensCapturadas",
                column: "IdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Impactos_ImpactoClassificacaoId",
                table: "Impactos",
                column: "ImpactoClassificacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubGrupoDesastre_GrupoDesastreId",
                table: "SubGrupoDesastre",
                column: "GrupoDesastreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagensCapturadas");

            migrationBuilder.DropTable(
                name: "Sensores");

            migrationBuilder.DropTable(
                name: "SubGrupoDesastre");

            migrationBuilder.DropTable(
                name: "Desastres");

            migrationBuilder.DropTable(
                name: "Drones");

            migrationBuilder.DropTable(
                name: "GruposDesastre");

            migrationBuilder.DropTable(
                name: "Impactos");

            migrationBuilder.DropTable(
                name: "Locais");

            migrationBuilder.DropTable(
                name: "TerrenosGeograficos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "ImpactosClassificacao");
        }
    }
}
