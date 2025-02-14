using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class minh02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GioHangGHID",
                table: "gioHangCT",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdGH",
                table: "gioHangCT",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    GHID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountIdAcc = table.Column<int>(type: "int", nullable: true),
                    IdAcct = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.GHID);
                    table.ForeignKey(
                        name: "FK_GioHang_Accounts_AccountIdAcc",
                        column: x => x.AccountIdAcc,
                        principalTable: "Accounts",
                        principalColumn: "IdAcc");
                });

            migrationBuilder.CreateIndex(
                name: "IX_gioHangCT_GioHangGHID",
                table: "gioHangCT",
                column: "GioHangGHID");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_AccountIdAcc",
                table: "GioHang",
                column: "AccountIdAcc");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangCT_GioHang_GioHangGHID",
                table: "gioHangCT",
                column: "GioHangGHID",
                principalTable: "GioHang",
                principalColumn: "GHID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangCT_GioHang_GioHangGHID",
                table: "gioHangCT");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropIndex(
                name: "IX_gioHangCT_GioHangGHID",
                table: "gioHangCT");

            migrationBuilder.DropColumn(
                name: "GioHangGHID",
                table: "gioHangCT");

            migrationBuilder.DropColumn(
                name: "IdGH",
                table: "gioHangCT");
        }
    }
}
