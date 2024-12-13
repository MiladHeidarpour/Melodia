using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proj.Infrastructure.Migrations
{
    public partial class CreateUserAndRoleRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Music");

            migrationBuilder.EnsureSchema(
                name: "Role");

            migrationBuilder.EnsureSchema(
                name: "User");

            migrationBuilder.RenameTable(
                name: "Tokens",
                schema: "user",
                newName: "Tokens",
                newSchema: "User");

            migrationBuilder.RenameTable(
                name: "Permissions",
                schema: "role",
                newName: "Permissions",
                newSchema: "Role");

            migrationBuilder.RenameTable(
                name: "ArtistMusics",
                schema: "Musics",
                newName: "ArtistMusics",
                newSchema: "Music");

            migrationBuilder.Sql(@"
                ALTER TABLE [dbo].Users
                ADD CONSTRAINT FK_User_Roles_RoleId
                FOREIGN KEY (RoleId) REFERENCES [dbo].Roles(Id);");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Musics");

            migrationBuilder.EnsureSchema(
                name: "role");

            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.RenameTable(
                name: "Tokens",
                schema: "User",
                newName: "Tokens",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "Permissions",
                schema: "Role",
                newName: "Permissions",
                newSchema: "role");

            migrationBuilder.RenameTable(
                name: "ArtistMusics",
                schema: "Music",
                newName: "ArtistMusics",
                newSchema: "Musics");
        }
    }
}
