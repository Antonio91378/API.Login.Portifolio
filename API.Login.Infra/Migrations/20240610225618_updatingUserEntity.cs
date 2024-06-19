using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Login.Infra.Migrations
{
    /// <inheritdoc />
    public partial class updatingUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassWord",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Ativo",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmailHash",
                table: "User",
                type: "varchar",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Logado",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "PassWordHash",
                table: "User",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PassWordSalt",
                table: "User",
                type: "BLOB",
                maxLength: 20,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "SessionJwt",
                table: "User",
                type: "varchar",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EmailHash",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Logado",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PassWordHash",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PassWordSalt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SessionJwt",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "PassWord",
                table: "User",
                type: "varchar",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
