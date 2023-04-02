using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAuthentication.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roleDB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roleDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userDB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    thisDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passwordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userRoleDB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRoleDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userRoleDB_roleDB_RoleID",
                        column: x => x.RoleID,
                        principalTable: "roleDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userRoleDB_userDB_UserID",
                        column: x => x.UserID,
                        principalTable: "userDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userRoleDB_RoleID",
                table: "userRoleDB",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_userRoleDB_UserID",
                table: "userRoleDB",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userRoleDB");

            migrationBuilder.DropTable(
                name: "roleDB");

            migrationBuilder.DropTable(
                name: "userDB");
        }
    }
}
