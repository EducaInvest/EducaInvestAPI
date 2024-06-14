using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducaInvestAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixFotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FileBytes",
                table: "TB_USUARIOS",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileBytes",
                table: "TB_PROJETOS",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAcesso", "FileBytes", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 6, 14, 1, 47, 4, 870, DateTimeKind.Local).AddTicks(2605), null, new byte[] { 32, 51, 251, 195, 246, 87, 36, 93, 6, 93, 185, 18, 203, 37, 172, 86, 203, 158, 96, 96, 140, 176, 110, 145, 128, 231, 158, 204, 134, 31, 246, 57, 90, 212, 178, 107, 8, 0, 234, 182, 66, 95, 16, 93, 4, 140, 84, 176, 12, 110, 209, 71, 33, 205, 21, 36, 12, 197, 78, 61, 76, 56, 27, 168 }, new byte[] { 125, 124, 128, 77, 245, 254, 220, 12, 225, 49, 207, 207, 190, 243, 101, 9, 200, 230, 1, 10, 183, 16, 124, 27, 42, 133, 99, 119, 224, 27, 174, 170, 160, 10, 187, 96, 226, 192, 193, 3, 188, 124, 253, 158, 131, 81, 34, 119, 199, 232, 220, 92, 93, 45, 43, 107, 55, 210, 122, 8, 243, 94, 184, 184, 131, 182, 146, 215, 238, 158, 136, 23, 103, 159, 133, 235, 138, 229, 158, 39, 40, 43, 132, 101, 31, 209, 191, 170, 180, 43, 130, 177, 137, 143, 152, 19, 198, 157, 223, 24, 10, 250, 66, 179, 92, 26, 74, 246, 43, 39, 108, 39, 134, 192, 72, 77, 77, 130, 142, 2, 200, 87, 244, 185, 178, 137, 131, 125 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "FileBytes",
                table: "TB_USUARIOS",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "FileBytes",
                table: "TB_PROJETOS",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAcesso", "FileBytes", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 6, 14, 1, 27, 18, 355, DateTimeKind.Local).AddTicks(5103), null, new byte[] { 165, 208, 28, 57, 63, 87, 174, 53, 80, 83, 240, 236, 162, 66, 14, 99, 253, 102, 29, 0, 247, 23, 170, 179, 216, 0, 78, 200, 146, 95, 107, 38, 35, 136, 117, 189, 67, 163, 151, 159, 233, 252, 126, 211, 160, 10, 149, 69, 82, 248, 74, 164, 114, 132, 228, 136, 26, 73, 243, 114, 67, 226, 138, 160 }, new byte[] { 187, 163, 243, 72, 36, 135, 218, 164, 114, 159, 2, 202, 69, 144, 143, 33, 57, 44, 172, 247, 207, 140, 120, 190, 69, 194, 79, 231, 89, 149, 33, 124, 110, 39, 115, 236, 157, 105, 98, 35, 98, 235, 43, 178, 94, 94, 215, 168, 222, 185, 191, 189, 218, 133, 45, 225, 199, 80, 1, 227, 204, 156, 176, 79, 74, 40, 88, 68, 80, 95, 158, 87, 116, 246, 212, 190, 210, 167, 182, 120, 192, 97, 66, 248, 190, 49, 219, 14, 51, 105, 204, 93, 216, 236, 207, 54, 204, 48, 100, 226, 183, 190, 210, 15, 77, 220, 154, 168, 177, 248, 35, 89, 35, 251, 172, 2, 122, 64, 89, 251, 253, 243, 52, 8, 148, 143, 124, 119 } });
        }
    }
}
