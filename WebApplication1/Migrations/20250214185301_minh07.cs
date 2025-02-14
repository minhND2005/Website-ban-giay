using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class minh07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_gioHangCT_IdAcc_IdSPCT",
                table: "gioHangCT",
                columns: new[] { "IdAcc", "IdSPCT" },
                unique: true,
                filter: "[IdAcc] IS NOT NULL AND [IdSPCT] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_gioHangCT_IdAcc_IdSPCT",
                table: "gioHangCT");
        }
    }
}
