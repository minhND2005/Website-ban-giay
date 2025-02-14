using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class minh04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangCT_gioHangs_GioHangGHID",
                table: "gioHangCT");

            migrationBuilder.DropTable(
                name: "gioHangs");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "gioHangs",
                columns: table => new
                {
                    GHID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountIdAcc = table.Column<int>(type: "int", nullable: true),
                    IdAcct = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gioHangs", x => x.GHID);
                    table.ForeignKey(
                        name: "FK_gioHangs_Accounts_AccountIdAcc",
                        column: x => x.AccountIdAcc,
                        principalTable: "Accounts",
                        principalColumn: "IdAcc");
                });

            migrationBuilder.CreateIndex(
                name: "IX_gioHangCT_GioHangGHID",
                table: "gioHangCT",
                column: "GioHangGHID");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangs_AccountIdAcc",
                table: "gioHangs",
                column: "AccountIdAcc");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangCT_gioHangs_GioHangGHID",
                table: "gioHangCT",
                column: "GioHangGHID",
                principalTable: "gioHangs",
                principalColumn: "GHID");
        }
    }
}
