using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectName.Sso.Application.Database.Migrations.Database
{
    public partial class InitialDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessRoles",
                columns: table => new
                {
                    AccessRoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRoles", x => x.AccessRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<string>(maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(maxLength: 256, nullable: false),
                    LastName = table.Column<string>(maxLength: 256, nullable: false),
                    Username = table.Column<string>(maxLength: 256, nullable: false),
                    Password = table.Column<string>(maxLength: 1024, nullable: false),
                    PasswordSalt = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserExternalProviders",
                columns: table => new
                {
                    UserExternalProviderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provider = table.Column<string>(maxLength: 1024, nullable: false),
                    ProviderUserId = table.Column<string>(maxLength: 1024, nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExternalProviders", x => x.UserExternalProviderId);
                    table.ForeignKey(
                        name: "FK_UserExternalProviders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    AccessRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_AccessRoles_AccessRoleId",
                        column: x => x.AccessRoleId,
                        principalTable: "AccessRoles",
                        principalColumn: "AccessRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessRoles_Name",
                table: "AccessRoles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserExternalProviders_UserId",
                table: "UserExternalProviders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_AccessRoleId",
                table: "UserRoles",
                column: "AccessRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Guid",
                table: "Users",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserExternalProviders");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "AccessRoles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
