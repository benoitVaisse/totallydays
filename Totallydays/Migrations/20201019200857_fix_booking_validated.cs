using Microsoft.EntityFrameworkCore.Migrations;

namespace Totallydays.Migrations
{
    public partial class fix_booking_validated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Validated",
                table: "Bookings",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Validated",
                table: "Bookings",
                type: "bit",
                nullable: true,
                oldClrType: typeof(byte));
        }
    }
}
