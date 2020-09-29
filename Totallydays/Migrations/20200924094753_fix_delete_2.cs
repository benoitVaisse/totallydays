using Microsoft.EntityFrameworkCore.Migrations;

namespace Totallydays.Migrations
{
    public partial class fix_delete_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bedroom_Beds_Beds_BedBed_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedroom_Beds_Bedrooms_BedroomBedroom_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedrooms_Hostings_Hosting_id",
                table: "Bedrooms");

            migrationBuilder.AddForeignKey(
                name: "FK_Bedroom_Beds_Beds_BedBed_id",
                table: "Bedroom_Beds",
                column: "BedBed_id",
                principalTable: "Beds",
                principalColumn: "Bed_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bedroom_Beds_Bedrooms_BedroomBedroom_id",
                table: "Bedroom_Beds",
                column: "BedroomBedroom_id",
                principalTable: "Bedrooms",
                principalColumn: "Bedroom_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bedrooms_Hostings_Hosting_id",
                table: "Bedrooms",
                column: "Hosting_id",
                principalTable: "Hostings",
                principalColumn: "Hosting_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bedroom_Beds_Beds_BedBed_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedroom_Beds_Bedrooms_BedroomBedroom_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedrooms_Hostings_Hosting_id",
                table: "Bedrooms");

            migrationBuilder.AddForeignKey(
                name: "FK_Bedroom_Beds_Beds_BedBed_id",
                table: "Bedroom_Beds",
                column: "BedBed_id",
                principalTable: "Beds",
                principalColumn: "Bed_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedroom_Beds_Bedrooms_BedroomBedroom_id",
                table: "Bedroom_Beds",
                column: "BedroomBedroom_id",
                principalTable: "Bedrooms",
                principalColumn: "Bedroom_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedrooms_Hostings_Hosting_id",
                table: "Bedrooms",
                column: "Hosting_id",
                principalTable: "Hostings",
                principalColumn: "Hosting_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
