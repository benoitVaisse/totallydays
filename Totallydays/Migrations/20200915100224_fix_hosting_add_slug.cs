using Microsoft.EntityFrameworkCore.Migrations;

namespace Totallydays.Migrations
{
    public partial class fix_hosting_add_slug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "Modified",
               table: "Hostings");

            migrationBuilder.AddColumn<bool>(
                name: "Modified",
                table: "Hostings",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Hostings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Hostings");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Hostings");
        }
    }
}
