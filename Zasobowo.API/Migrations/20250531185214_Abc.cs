using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Zasobowo.API.Migrations
{
    /// <inheritdoc />
    public partial class Abc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    AssignedUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "ola@bitpol.pl", "Ola", "Jaskólska", "1234", "User", "ola.dev" },
                    { 2, "kamil@bitpol.pl", "Kamil", "Sys", "1234", "User", "kamil.sys" },
                    { 3, "ania@bitpol.pl", "Ania", "UI", "1234", "User", "ania.ui" },
                    { 4, "mario@bitpol.pl", "Mario", "Q", "1234", "User", "mario.q" },
                    { 5, "ewelina@bitpol.pl", "Ewelina", "PMO", "1234", "User", "ewelina.pmo" },
                    { 6, "dawid@bitpol.pl", "Dawid", "Admin", "1234", "Admin", "dawid.admin" },
                    { 7, "szymon@bitpol.pl", "Szymon", "Fullstack", "1234", "User", "szymon.fullstack" },
                    { 8, "gosia@bitpol.pl", "Gosia", "Testerka", "1234", "User", "gosia.test" },
                    { 9, "adam@bitpol.pl", "Adam", "Security", "1234", "Admin", "adam.secu" },
                    { 10, "karolina@bitpol.pl", "Karolina", "UX", "1234", "User", "karolina.ux" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AssignedUserId",
                table: "Devices",
                column: "AssignedUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
