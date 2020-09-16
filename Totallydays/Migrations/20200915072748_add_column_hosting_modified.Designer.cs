﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Totallydays.Data;

namespace Totallydays.Migrations
{
    [DbContext(typeof(TotallydaysContext))]
    [Migration("20200915072748_add_column_hosting_modified")]
    partial class add_column_hosting_modified
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Totallydays.Models.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Totallydays.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Totallydays.Models.Bed", b =>
                {
                    b.Property<int>("Bed_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Bed_id");

                    b.ToTable("Beds");
                });

            modelBuilder.Entity("Totallydays.Models.Bedroom", b =>
                {
                    b.Property<int>("Bedroom_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Hosting_id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Bedroom_id");

                    b.HasIndex("Hosting_id");

                    b.ToTable("Bedrooms");
                });

            modelBuilder.Entity("Totallydays.Models.Bedroom_Bed", b =>
                {
                    b.Property<int>("Bedroom_Bed_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Bed_id")
                        .HasColumnType("int");

                    b.Property<int>("Bedroom_id")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Bedroom_Bed_id");

                    b.HasIndex("Bed_id");

                    b.HasIndex("Bedroom_id");

                    b.ToTable("Bedroom_Beds");
                });

            modelBuilder.Entity("Totallydays.Models.Booking", b =>
                {
                    b.Property<int>("Booking_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("End_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Hosting_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("Validated")
                        .HasColumnType("bit");

                    b.HasKey("Booking_id");

                    b.HasIndex("Hosting_id");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Totallydays.Models.Comment", b =>
                {
                    b.Property<int>("Comment_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Hosting_id")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("User_emmiterId")
                        .HasColumnType("int");

                    b.Property<int?>("User_receiverId")
                        .HasColumnType("int");

                    b.HasKey("Comment_id");

                    b.HasIndex("Hosting_id");

                    b.HasIndex("User_emmiterId");

                    b.HasIndex("User_receiverId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Totallydays.Models.Equipment", b =>
                {
                    b.Property<int>("Equipment_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Equipement_typeEquipment_type_id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Equipment_id");

                    b.HasIndex("Equipement_typeEquipment_type_id");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("Totallydays.Models.Equipment_type", b =>
                {
                    b.Property<int>("Equipment_type_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Equipment_type_id");

                    b.ToTable("Equipment_types");
                });

            modelBuilder.Entity("Totallydays.Models.Hosting", b =>
                {
                    b.Property<int>("Hosting_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cover_image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Hosting_type_id")
                        .HasColumnType("int");

                    b.Property<float>("Lat")
                        .HasColumnType("real");

                    b.Property<float>("Lng")
                        .HasColumnType("real");

                    b.Property<bool>("Modified")
                        .HasColumnType("bit");

                    b.Property<string>("Post_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<string>("Resume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Hosting_id");

                    b.HasIndex("Hosting_type_id");

                    b.HasIndex("UserId");

                    b.ToTable("Hostings");
                });

            modelBuilder.Entity("Totallydays.Models.Hosting_Equipment", b =>
                {
                    b.Property<int>("Equipment_id")
                        .HasColumnType("int");

                    b.Property<int>("Hosting_id")
                        .HasColumnType("int");

                    b.Property<int?>("Equipment_id1")
                        .HasColumnType("int");

                    b.Property<int?>("Hosting_id1")
                        .HasColumnType("int");

                    b.HasKey("Equipment_id", "Hosting_id");

                    b.HasIndex("Equipment_id1");

                    b.HasIndex("Hosting_id1");

                    b.ToTable("Hosting_Equipment");
                });

            modelBuilder.Entity("Totallydays.Models.Hosting_type", b =>
                {
                    b.Property<int>("Hosting_type_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Hosting_type_id");

                    b.ToTable("Hosting_Types");
                });

            modelBuilder.Entity("Totallydays.Models.Image", b =>
                {
                    b.Property<int>("Image_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("File")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Hosting_id")
                        .HasColumnType("int");

                    b.HasKey("Image_id");

                    b.HasIndex("Hosting_id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Totallydays.Models.Unavailable_date", b =>
                {
                    b.Property<int>("Unavailable_date_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("End_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Hosting_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start_date")
                        .HasColumnType("datetime2");

                    b.HasKey("Unavailable_date_id");

                    b.HasIndex("Hosting_id");

                    b.ToTable("Unavailable_dates");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Totallydays.Models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Totallydays.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Totallydays.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Totallydays.Models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Totallydays.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Totallydays.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Totallydays.Models.Bedroom", b =>
                {
                    b.HasOne("Totallydays.Models.Hosting", "Hosting")
                        .WithMany("Bedrooms")
                        .HasForeignKey("Hosting_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Totallydays.Models.Bedroom_Bed", b =>
                {
                    b.HasOne("Totallydays.Models.Bed", "Bed")
                        .WithMany("Bedroom_Beds")
                        .HasForeignKey("Bed_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Totallydays.Models.Bedroom", "Bedroom")
                        .WithMany("Bedroom_Beds")
                        .HasForeignKey("Bedroom_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Totallydays.Models.Booking", b =>
                {
                    b.HasOne("Totallydays.Models.Hosting", "Hosting")
                        .WithMany("Bookings")
                        .HasForeignKey("Hosting_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Totallydays.Models.AppUser", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Totallydays.Models.Comment", b =>
                {
                    b.HasOne("Totallydays.Models.Hosting", "Hosting")
                        .WithMany("Comments")
                        .HasForeignKey("Hosting_id");

                    b.HasOne("Totallydays.Models.AppUser", "User_emmiter")
                        .WithMany("Comments_emmit")
                        .HasForeignKey("User_emmiterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Totallydays.Models.AppUser", "User_receiver")
                        .WithMany("Comments_receive")
                        .HasForeignKey("User_receiverId");
                });

            modelBuilder.Entity("Totallydays.Models.Equipment", b =>
                {
                    b.HasOne("Totallydays.Models.Equipment_type", "Equipement_type")
                        .WithMany("Equipments")
                        .HasForeignKey("Equipement_typeEquipment_type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Totallydays.Models.Hosting", b =>
                {
                    b.HasOne("Totallydays.Models.Hosting_type", "Hosting_type")
                        .WithMany("Hostings")
                        .HasForeignKey("Hosting_type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Totallydays.Models.AppUser", "User")
                        .WithMany("Hostings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Totallydays.Models.Hosting_Equipment", b =>
                {
                    b.HasOne("Totallydays.Models.Equipment", "Equipment")
                        .WithMany("Hosting_Equipment")
                        .HasForeignKey("Equipment_id1");

                    b.HasOne("Totallydays.Models.Hosting", "Hosting")
                        .WithMany("Hosting_Equipment")
                        .HasForeignKey("Hosting_id1");
                });

            modelBuilder.Entity("Totallydays.Models.Image", b =>
                {
                    b.HasOne("Totallydays.Models.Hosting", "Hosting")
                        .WithMany("Images")
                        .HasForeignKey("Hosting_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Totallydays.Models.Unavailable_date", b =>
                {
                    b.HasOne("Totallydays.Models.Hosting", "Hosting")
                        .WithMany("Unavailables_date")
                        .HasForeignKey("Hosting_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
