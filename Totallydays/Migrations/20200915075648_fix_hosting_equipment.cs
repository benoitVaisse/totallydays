using Microsoft.EntityFrameworkCore.Migrations;

namespace Totallydays.Migrations
{
    public partial class fix_hosting_equipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
               name: "EquipmentEquipment_id",
               table: "Hosting_Equipment",
               nullable: false,
               defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostingHosting_id",
                table: "Hosting_Equipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropForeignKey(
                name: "FK_Hosting_Equipment_Equipments_Equipment_id1",
                table: "Hosting_Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Hosting_Equipment_Hostings_Hosting_id1",
                table: "Hosting_Equipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hosting_Equipment",
                table: "Hosting_Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Hosting_Equipment_Equipment_id1",
                table: "Hosting_Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Hosting_Equipment_Hosting_id1",
                table: "Hosting_Equipment");

            migrationBuilder.DropColumn(
                name: "Equipment_id",
                table: "Hosting_Equipment");

            migrationBuilder.DropColumn(
                name: "Hosting_id",
                table: "Hosting_Equipment");

            migrationBuilder.DropColumn(
                name: "Equipment_id1",
                table: "Hosting_Equipment");

            migrationBuilder.DropColumn(
                name: "Hosting_id1",
                table: "Hosting_Equipment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hosting_Equipment",
                table: "Hosting_Equipment",
                columns: new[] { "EquipmentEquipment_id", "HostingHosting_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Hosting_Equipment_HostingHosting_id",
                table: "Hosting_Equipment",
                column: "HostingHosting_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hosting_Equipment_Equipments_EquipmentEquipment_id",
                table: "Hosting_Equipment",
                column: "EquipmentEquipment_id",
                principalTable: "Equipments",
                principalColumn: "Equipment_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hosting_Equipment_Hostings_HostingHosting_id",
                table: "Hosting_Equipment",
                column: "HostingHosting_id",
                principalTable: "Hostings",
                principalColumn: "Hosting_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hosting_Equipment_Equipments_EquipmentEquipment_id",
                table: "Hosting_Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Hosting_Equipment_Hostings_HostingHosting_id",
                table: "Hosting_Equipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hosting_Equipment",
                table: "Hosting_Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Hosting_Equipment_HostingHosting_id",
                table: "Hosting_Equipment");

            migrationBuilder.DropColumn(
                name: "EquipmentEquipment_id",
                table: "Hosting_Equipment");

            migrationBuilder.DropColumn(
                name: "HostingHosting_id",
                table: "Hosting_Equipment");

            migrationBuilder.AddColumn<int>(
                name: "Equipment_id",
                table: "Hosting_Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hosting_id",
                table: "Hosting_Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Equipment_id1",
                table: "Hosting_Equipment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hosting_id1",
                table: "Hosting_Equipment",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hosting_Equipment",
                table: "Hosting_Equipment",
                columns: new[] { "Equipment_id", "Hosting_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Hosting_Equipment_Equipment_id1",
                table: "Hosting_Equipment",
                column: "Equipment_id1");

            migrationBuilder.CreateIndex(
                name: "IX_Hosting_Equipment_Hosting_id1",
                table: "Hosting_Equipment",
                column: "Hosting_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Hosting_Equipment_Equipments_Equipment_id1",
                table: "Hosting_Equipment",
                column: "Equipment_id1",
                principalTable: "Equipments",
                principalColumn: "Equipment_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hosting_Equipment_Hostings_Hosting_id1",
                table: "Hosting_Equipment",
                column: "Hosting_id1",
                principalTable: "Hostings",
                principalColumn: "Hosting_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
