using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gravimetry_api.Data.Migrations
{
    public partial class uppercasestuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Team",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "isPublic",
                table: "Team",
                newName: "IsPublic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Team",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "IsPublic",
                table: "Team",
                newName: "isPublic");
        }
    }
}
