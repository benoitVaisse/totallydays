using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Totallydays.Migrations
{
    public partial class initialisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Firstname = table.Column<string>(nullable: false),
                    Lastname = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Picture = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beds",
                columns: table => new
                {
                    Bed_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beds", x => x.Bed_id);
                });

            migrationBuilder.CreateTable(
                name: "Equipment_types",
                columns: table => new
                {
                    Equipment_type_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment_types", x => x.Equipment_type_id);
                });

            migrationBuilder.CreateTable(
                name: "Hosting_Types",
                columns: table => new
                {
                    Hosting_type_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosting_Types", x => x.Hosting_type_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Equipment_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Equipement_typeEquipment_type_id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Equipment_id);
                    table.ForeignKey(
                        name: "FK_Equipments_Equipment_types_Equipement_typeEquipment_type_id",
                        column: x => x.Equipement_typeEquipment_type_id,
                        principalTable: "Equipment_types",
                        principalColumn: "Equipment_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hostings",
                columns: table => new
                {
                    Hosting_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hosting_type_id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Resume = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Cover_image = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Post_code = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Lng = table.Column<float>(nullable: false),
                    Lat = table.Column<float>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hostings", x => x.Hosting_id);
                    table.ForeignKey(
                        name: "FK_Hostings_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hostings_Hosting_Types_Hosting_type_id",
                        column: x => x.Hosting_type_id,
                        principalTable: "Hosting_Types",
                        principalColumn: "Hosting_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bedrooms",
                columns: table => new
                {
                    Bedroom_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hosting_id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedrooms", x => x.Bedroom_id);
                    table.ForeignKey(
                        name: "FK_Bedrooms_Hostings_Hosting_id",
                        column: x => x.Hosting_id,
                        principalTable: "Hostings",
                        principalColumn: "Hosting_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Booking_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hosting_id = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Start_date = table.Column<DateTime>(nullable: false),
                    End_date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<float>(nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Validated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Booking_id);
                    table.ForeignKey(
                        name: "FK_Bookings_Hostings_Hosting_id",
                        column: x => x.Hosting_id,
                        principalTable: "Hostings",
                        principalColumn: "Hosting_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Comment_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false),
                    User_emmiterId = table.Column<int>(nullable: false),
                    User_receiverId = table.Column<int>(nullable: true),
                    Hosting_id1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Comment_id);
                    table.ForeignKey(
                        name: "FK_Comments_Hostings_Hosting_id1",
                        column: x => x.Hosting_id1,
                        principalTable: "Hostings",
                        principalColumn: "Hosting_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_User_emmiterId",
                        column: x => x.User_emmiterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_User_receiverId",
                        column: x => x.User_receiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hosting_Equipment",
                columns: table => new
                {
                    Hosting_id = table.Column<int>(nullable: false),
                    Equipment_id = table.Column<int>(nullable: false),
                    Hosting_id1 = table.Column<int>(nullable: true),
                    Equipment_id1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosting_Equipment", x => new { x.Equipment_id, x.Hosting_id });
                    table.ForeignKey(
                        name: "FK_Hosting_Equipment_Equipments_Equipment_id1",
                        column: x => x.Equipment_id1,
                        principalTable: "Equipments",
                        principalColumn: "Equipment_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hosting_Equipment_Hostings_Hosting_id1",
                        column: x => x.Hosting_id1,
                        principalTable: "Hostings",
                        principalColumn: "Hosting_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Image_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hosting_id = table.Column<int>(nullable: false),
                    File = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Image_id);
                    table.ForeignKey(
                        name: "FK_Images_Hostings_Hosting_id",
                        column: x => x.Hosting_id,
                        principalTable: "Hostings",
                        principalColumn: "Hosting_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unavailable_dates",
                columns: table => new
                {
                    Unavailable_date_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hosting_id = table.Column<int>(nullable: false),
                    Start_date = table.Column<DateTime>(nullable: false),
                    End_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unavailable_dates", x => x.Unavailable_date_id);
                    table.ForeignKey(
                        name: "FK_Unavailable_dates_Hostings_Hosting_id",
                        column: x => x.Hosting_id,
                        principalTable: "Hostings",
                        principalColumn: "Hosting_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bedroom_Beds",
                columns: table => new
                {
                    Bedroom_Bed_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bed_id = table.Column<int>(nullable: false),
                    Bedroom_id = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedroom_Beds", x => x.Bedroom_Bed_id);
                    table.ForeignKey(
                        name: "FK_Bedroom_Beds_Beds_Bed_id",
                        column: x => x.Bed_id,
                        principalTable: "Beds",
                        principalColumn: "Bed_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bedroom_Beds_Bedrooms_Bedroom_id",
                        column: x => x.Bedroom_id,
                        principalTable: "Bedrooms",
                        principalColumn: "Bedroom_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bedroom_Beds_Bed_id",
                table: "Bedroom_Beds",
                column: "Bed_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bedroom_Beds_Bedroom_id",
                table: "Bedroom_Beds",
                column: "Bedroom_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bedrooms_Hosting_id",
                table: "Bedrooms",
                column: "Hosting_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Hosting_id",
                table: "Bookings",
                column: "Hosting_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Hosting_id1",
                table: "Comments",
                column: "Hosting_id1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_User_emmiterId",
                table: "Comments",
                column: "User_emmiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_User_receiverId",
                table: "Comments",
                column: "User_receiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_Equipement_typeEquipment_type_id",
                table: "Equipments",
                column: "Equipement_typeEquipment_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hosting_Equipment_Equipment_id1",
                table: "Hosting_Equipment",
                column: "Equipment_id1");

            migrationBuilder.CreateIndex(
                name: "IX_Hosting_Equipment_Hosting_id1",
                table: "Hosting_Equipment",
                column: "Hosting_id1");

            migrationBuilder.CreateIndex(
                name: "IX_Hostings_AppUserId",
                table: "Hostings",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hostings_Hosting_type_id",
                table: "Hostings",
                column: "Hosting_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Hosting_id",
                table: "Images",
                column: "Hosting_id");

            migrationBuilder.CreateIndex(
                name: "IX_Unavailable_dates_Hosting_id",
                table: "Unavailable_dates",
                column: "Hosting_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bedroom_Beds");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Hosting_Equipment");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Unavailable_dates");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Beds");

            migrationBuilder.DropTable(
                name: "Bedrooms");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Hostings");

            migrationBuilder.DropTable(
                name: "Equipment_types");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Hosting_Types");
        }
    }
}
