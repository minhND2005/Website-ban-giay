using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class minh03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHang_Accounts_AccountIdAcc",
                table: "GioHang");

            migrationBuilder.DropForeignKey(
                name: "FK_gioHangCT_GioHang_GioHangGHID",
                table: "gioHangCT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang");

            migrationBuilder.RenameTable(
                name: "GioHang",
                newName: "gioHangs");

            migrationBuilder.RenameIndex(
                name: "IX_GioHang_AccountIdAcc",
                table: "gioHangs",
                newName: "IX_gioHangs_AccountIdAcc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gioHangs",
                table: "gioHangs",
                column: "GHID");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangCT_gioHangs_GioHangGHID",
                table: "gioHangCT",
                column: "GioHangGHID",
                principalTable: "gioHangs",
                principalColumn: "GHID");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangs_Accounts_AccountIdAcc",
                table: "gioHangs",
                column: "AccountIdAcc",
                principalTable: "Accounts",
                principalColumn: "IdAcc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangCT_gioHangs_GioHangGHID",
                table: "gioHangCT");

            migrationBuilder.DropForeignKey(
                name: "FK_gioHangs_Accounts_AccountIdAcc",
                table: "gioHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gioHangs",
                table: "gioHangs");

            migrationBuilder.RenameTable(
                name: "gioHangs",
                newName: "GioHang");

            migrationBuilder.RenameIndex(
                name: "IX_gioHangs_AccountIdAcc",
                table: "GioHang",
                newName: "IX_GioHang_AccountIdAcc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang",
                column: "GHID");

            migrationBuilder.AddForeignKey(
                name: "FK_GioHang_Accounts_AccountIdAcc",
                table: "GioHang",
                column: "AccountIdAcc",
                principalTable: "Accounts",
                principalColumn: "IdAcc");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangCT_GioHang_GioHangGHID",
                table: "gioHangCT",
                column: "GioHangGHID",
                principalTable: "GioHang",
                principalColumn: "GHID");
        }
    }
}
