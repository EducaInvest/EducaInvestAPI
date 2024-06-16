using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducaInvestAPI.Migrations
{
    /// <inheritdoc />
    public partial class Mensagens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LinkSocial",
                table: "TB_USUARIOS",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "TB_USUARIOS",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subtitulo",
                table: "TB_PROJETOS",
                type: "nvarchar(65)",
                maxLength: 65,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(65)",
                oldMaxLength: 65,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoProjeto",
                table: "TB_PROJETOS",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoAtividade",
                table: "TB_ATIVIDADES",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAcesso", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 6, 16, 3, 13, 29, 949, DateTimeKind.Local).AddTicks(4751), new byte[] { 75, 100, 38, 183, 51, 196, 95, 140, 125, 120, 42, 119, 123, 254, 66, 77, 208, 183, 241, 38, 60, 232, 52, 237, 87, 162, 46, 77, 87, 193, 188, 48, 52, 132, 110, 213, 223, 93, 80, 54, 116, 22, 105, 116, 188, 59, 206, 59, 169, 195, 94, 126, 128, 84, 189, 153, 194, 89, 238, 175, 76, 130, 180, 51 }, new byte[] { 195, 4, 121, 74, 71, 90, 89, 106, 235, 36, 237, 70, 27, 178, 151, 1, 129, 138, 202, 29, 149, 185, 189, 163, 157, 35, 134, 99, 196, 248, 229, 251, 111, 141, 85, 11, 45, 91, 93, 112, 10, 82, 1, 197, 218, 153, 113, 212, 115, 118, 23, 106, 184, 57, 13, 175, 232, 115, 70, 110, 152, 216, 116, 42, 223, 92, 101, 14, 29, 143, 67, 254, 215, 29, 148, 110, 211, 251, 248, 41, 128, 84, 218, 34, 90, 105, 166, 172, 50, 78, 229, 129, 16, 236, 208, 35, 42, 234, 96, 181, 115, 131, 232, 134, 46, 87, 72, 243, 95, 61, 94, 38, 184, 28, 124, 105, 180, 253, 85, 231, 16, 57, 230, 242, 37, 81, 172, 152 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LinkSocial",
                table: "TB_USUARIOS",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "TB_USUARIOS",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Subtitulo",
                table: "TB_PROJETOS",
                type: "nvarchar(65)",
                maxLength: 65,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(65)",
                oldMaxLength: 65);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoProjeto",
                table: "TB_PROJETOS",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoAtividade",
                table: "TB_ATIVIDADES",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAcesso", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 6, 14, 1, 47, 4, 870, DateTimeKind.Local).AddTicks(2605), new byte[] { 32, 51, 251, 195, 246, 87, 36, 93, 6, 93, 185, 18, 203, 37, 172, 86, 203, 158, 96, 96, 140, 176, 110, 145, 128, 231, 158, 204, 134, 31, 246, 57, 90, 212, 178, 107, 8, 0, 234, 182, 66, 95, 16, 93, 4, 140, 84, 176, 12, 110, 209, 71, 33, 205, 21, 36, 12, 197, 78, 61, 76, 56, 27, 168 }, new byte[] { 125, 124, 128, 77, 245, 254, 220, 12, 225, 49, 207, 207, 190, 243, 101, 9, 200, 230, 1, 10, 183, 16, 124, 27, 42, 133, 99, 119, 224, 27, 174, 170, 160, 10, 187, 96, 226, 192, 193, 3, 188, 124, 253, 158, 131, 81, 34, 119, 199, 232, 220, 92, 93, 45, 43, 107, 55, 210, 122, 8, 243, 94, 184, 184, 131, 182, 146, 215, 238, 158, 136, 23, 103, 159, 133, 235, 138, 229, 158, 39, 40, 43, 132, 101, 31, 209, 191, 170, 180, 43, 130, 177, 137, 143, 152, 19, 198, 157, 223, 24, 10, 250, 66, 179, 92, 26, 74, 246, 43, 39, 108, 39, 134, 192, 72, 77, 77, 130, 142, 2, 200, 87, 244, 185, 178, 137, 131, 125 } });
        }
    }
}
