using Microsoft.EntityFrameworkCore.Migrations;

namespace Totallydays.Migrations
{
    public partial class fix_unavailable_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unavailable_dates_Hostings_Hosting_id",
                table: "Unavailable_dates");

            migrationBuilder.RenameColumn(
                name: "Hosting_id",
                table: "Unavailable_dates",
                newName: "hosting_id");

            migrationBuilder.RenameIndex(
                name: "IX_Unavailable_dates_Hosting_id",
                table: "Unavailable_dates",
                newName: "IX_Unavailable_dates_hosting_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Unavailable_dates_Hostings_hosting_id",
                table: "Unavailable_dates",
                column: "hosting_id",
                principalTable: "Hostings",
                principalColumn: "Hosting_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unavailable_dates_Hostings_hosting_id",
                table: "Unavailable_dates");

            migrationBuilder.RenameColumn(
                name: "hosting_id",
                table: "Unavailable_dates",
                newName: "Hosting_id");

            migrationBuilder.RenameIndex(
                name: "IX_Unavailable_dates_hosting_id",
                table: "Unavailable_dates",
                newName: "IX_Unavailable_dates_Hosting_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Unavailable_dates_Hostings_Hosting_id",
                table: "Unavailable_dates",
                column: "Hosting_id",
                principalTable: "Hostings",
                principalColumn: "Hosting_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
