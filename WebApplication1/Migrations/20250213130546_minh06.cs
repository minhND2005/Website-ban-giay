using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class minh06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountIdAcc",
                table: "hoaDons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdAcc",
                table: "hoaDons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_AccountIdAcc",
                table: "hoaDons",
                column: "AccountIdAcc");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDons_Accounts_AccountIdAcc",
                table: "hoaDons",
                column: "AccountIdAcc",
                principalTable: "Accounts",
                principalColumn: "IdAcc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDons_Accounts_AccountIdAcc",
                table: "hoaDons");

            migrationBuilder.DropIndex(
                name: "IX_hoaDons_AccountIdAcc",
                table: "hoaDons");

            migrationBuilder.DropColumn(
                name: "AccountIdAcc",
                table: "hoaDons");

            migrationBuilder.DropColumn(
                name: "IdAcc",
                table: "hoaDons");
        }
    }
}
