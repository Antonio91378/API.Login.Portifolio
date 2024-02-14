using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Login.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UserTableCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "varchar", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "varchar", maxLength: 200, nullable: false),
                    PassWord = table.Column<string>(type: "varchar", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
