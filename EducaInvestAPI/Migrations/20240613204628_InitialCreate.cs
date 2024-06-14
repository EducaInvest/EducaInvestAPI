using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducaInvestAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_USUARIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Perfil = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    LinkSocial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UF = table.Column<int>(type: "int", nullable: false),
                    DataAcesso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileBytes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PROJETOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusProjeto = table.Column<int>(type: "int", nullable: false),
                    NomeProjeto = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Subtitulo = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    DescricaoProjeto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CustoProjeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Investido = table.Column<bool>(type: "bit", nullable: false),
                    DataPublicacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    CronogramaId = table.Column<int>(type: "int", nullable: false),
                    FileBytes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PROJETOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_PROJETOS_TB_USUARIOS_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "TB_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_CRONOGRAMAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjetoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CRONOGRAMAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_CRONOGRAMAS_TB_PROJETOS_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "TB_PROJETOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ATIVIDADES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoAtividade = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StatusAtividade = table.Column<int>(type: "int", nullable: false),
                    DataInicioAtividade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataTerminoAtividade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Percentual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CronogramaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ATIVIDADES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ATIVIDADES_TB_CRONOGRAMAS_CronogramaId",
                        column: x => x.CronogramaId,
                        principalTable: "TB_CRONOGRAMAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TB_USUARIOS",
                columns: new[] { "Id", "CPF", "Cidade", "DataAcesso", "Email", "FileBytes", "LinkSocial", "Nome", "PasswordHash", "PasswordSalt", "Perfil", "Sobrenome", "Telefone", "UF" },
                values: new object[] { 1, "", "", new DateTime(2024, 6, 13, 17, 46, 27, 260, DateTimeKind.Local).AddTicks(5055), "educainvest.co@gmail.com", null, "", "", new byte[] { 15, 151, 177, 164, 192, 229, 252, 225, 14, 109, 28, 232, 222, 242, 193, 59, 39, 6, 38, 15, 143, 57, 65, 55, 190, 143, 138, 139, 149, 139, 210, 137, 174, 30, 213, 230, 238, 47, 248, 183, 250, 168, 50, 242, 7, 129, 229, 51, 146, 144, 69, 174, 5, 236, 142, 17, 63, 33, 233, 114, 179, 150, 76, 78 }, new byte[] { 42, 111, 5, 120, 165, 4, 194, 104, 109, 145, 123, 186, 108, 227, 145, 153, 103, 201, 80, 241, 86, 51, 145, 205, 88, 199, 92, 209, 198, 168, 209, 3, 113, 119, 45, 237, 168, 189, 141, 7, 88, 118, 62, 117, 36, 24, 5, 205, 19, 152, 71, 237, 194, 147, 125, 82, 188, 50, 198, 61, 138, 222, 231, 189, 118, 152, 155, 175, 165, 243, 120, 98, 32, 53, 22, 240, 172, 72, 246, 60, 48, 141, 162, 206, 153, 22, 213, 89, 75, 103, 26, 5, 100, 64, 148, 211, 141, 100, 63, 37, 200, 177, 37, 196, 167, 169, 119, 170, 186, 210, 153, 160, 220, 97, 10, 109, 44, 123, 15, 122, 184, 190, 160, 254, 149, 154, 140, 225 }, 1, "", "", 6 });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ATIVIDADES_CronogramaId",
                table: "TB_ATIVIDADES",
                column: "CronogramaId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CRONOGRAMAS_ProjetoId",
                table: "TB_CRONOGRAMAS",
                column: "ProjetoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_PROJETOS_UsuarioId",
                table: "TB_PROJETOS",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ATIVIDADES");

            migrationBuilder.DropTable(
                name: "TB_CRONOGRAMAS");

            migrationBuilder.DropTable(
                name: "TB_PROJETOS");

            migrationBuilder.DropTable(
                name: "TB_USUARIOS");
        }
    }
}
