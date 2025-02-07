using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_gioHangCT_GioHangCTIdGHCT",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_GioHangCTIdGHCT",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "GioHangCTIdGHCT",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "IdAcc",
                table: "gioHangCT",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_gioHangCT_IdAcc",
                table: "gioHangCT",
                column: "IdAcc",
                unique: true,
                filter: "[IdAcc] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangCT_Accounts_IdAcc",
                table: "gioHangCT",
                column: "IdAcc",
                principalTable: "Accounts",
                principalColumn: "IdAcc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangCT_Accounts_IdAcc",
                table: "gioHangCT");

            migrationBuilder.DropIndex(
                name: "IX_gioHangCT_IdAcc",
                table: "gioHangCT");

            migrationBuilder.DropColumn(
                name: "IdAcc",
                table: "gioHangCT");

            migrationBuilder.AddColumn<int>(
                name: "GioHangCTIdGHCT",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAcc",
                keyValue: 1,
                column: "GioHangCTIdGHCT",
                value: null);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "IdAcc",
                keyValue: 2,
                column: "GioHangCTIdGHCT",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_GioHangCTIdGHCT",
                table: "Accounts",
                column: "GioHangCTIdGHCT");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_gioHangCT_GioHangCTIdGHCT",
                table: "Accounts",
                column: "GioHangCTIdGHCT",
                principalTable: "gioHangCT",
                principalColumn: "IdGHCT");
        }
    }
}
