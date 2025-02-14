using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class abc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nhanViens_Accounts_AccountIdAcc",
                table: "nhanViens");

            migrationBuilder.AlterColumn<int>(
                name: "AccountIdAcc",
                table: "nhanViens",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_nhanViens_Accounts_AccountIdAcc",
                table: "nhanViens",
                column: "AccountIdAcc",
                principalTable: "Accounts",
                principalColumn: "IdAcc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nhanViens_Accounts_AccountIdAcc",
                table: "nhanViens");

            migrationBuilder.AlterColumn<int>(
                name: "AccountIdAcc",
                table: "nhanViens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_nhanViens_Accounts_AccountIdAcc",
                table: "nhanViens",
                column: "AccountIdAcc",
                principalTable: "Accounts",
                principalColumn: "IdAcc",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
