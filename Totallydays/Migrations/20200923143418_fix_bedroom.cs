using Microsoft.EntityFrameworkCore.Migrations;

namespace Totallydays.Migrations
{
    public partial class fix_bedroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bedroom_Beds_Beds_Bed_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedroom_Beds_Bedrooms_Bedroom_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropIndex(
                name: "IX_Bedroom_Beds_Bed_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropIndex(
                name: "IX_Bedroom_Beds_Bedroom_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropColumn(
                name: "Bed_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropColumn(
                name: "Bedroom_id",
                table: "Bedroom_Beds");

            migrationBuilder.AddColumn<int>(
                name: "BedBed_id",
                table: "Bedroom_Beds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BedroomBedroom_id",
                table: "Bedroom_Beds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bedroom_Beds_BedBed_id",
                table: "Bedroom_Beds",
                column: "BedBed_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bedroom_Beds_BedroomBedroom_id",
                table: "Bedroom_Beds",
                column: "BedroomBedroom_id");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bedroom_Beds_Beds_BedBed_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedroom_Beds_Bedrooms_BedroomBedroom_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropIndex(
                name: "IX_Bedroom_Beds_BedBed_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropIndex(
                name: "IX_Bedroom_Beds_BedroomBedroom_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropColumn(
                name: "BedBed_id",
                table: "Bedroom_Beds");

            migrationBuilder.DropColumn(
                name: "BedroomBedroom_id",
                table: "Bedroom_Beds");

            migrationBuilder.AddColumn<int>(
                name: "Bed_id",
                table: "Bedroom_Beds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Bedroom_id",
                table: "Bedroom_Beds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bedroom_Beds_Bed_id",
                table: "Bedroom_Beds",
                column: "Bed_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bedroom_Beds_Bedroom_id",
                table: "Bedroom_Beds",
                column: "Bedroom_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bedroom_Beds_Beds_Bed_id",
                table: "Bedroom_Beds",
                column: "Bed_id",
                principalTable: "Beds",
                principalColumn: "Bed_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedroom_Beds_Bedrooms_Bedroom_id",
                table: "Bedroom_Beds",
                column: "Bedroom_id",
                principalTable: "Bedrooms",
                principalColumn: "Bedroom_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
