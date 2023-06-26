﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrintingHouse.Infrastructure.Data;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    [DbContext(typeof(PrintingHouseDbContext))]
    partial class PrintingHouseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Article primary key.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasComment("Article owner id");

                    b.Property<int>("ColorModel")
                        .HasColumnType("int")
                        .HasComment("Article color model");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Soft delete boolean property");

                    b.Property<int>("Material")
                        .HasColumnType("int")
                        .HasComment("Article material");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasComment("Article name.");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Articles");

                    b.HasComment("Particular client article ready for print.");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.ArticleConsumable", b =>
                {
                    b.Property<int>("ArticleId")
                        .HasColumnType("int")
                        .HasComment("Article id");

                    b.Property<int>("ConsumableId")
                        .HasColumnType("int")
                        .HasComment("Consumable id");

                    b.Property<double>("ConsumableQuantity")
                        .HasColumnType("float")
                        .HasComment("Required consumable quantity for single print of article");

                    b.HasKey("ArticleId", "ConsumableId");

                    b.HasIndex("ConsumableId");

                    b.ToTable("ArticlesConsumables");

                    b.HasComment("Article consumable with quantity (connecting table");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Client primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasComment("Client e-mail");

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Client's merchant id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasComment("Client name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Client phone number");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId");

                    b.ToTable("Clients");

                    b.HasComment("Printing house client");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Consumable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Consumable primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("InStock")
                        .HasColumnType("int")
                        .HasComment("Consumable current quantit in stock");

                    b.Property<int>("MyProperty")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Consumable price");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasComment("Consumable type (enumeration)");

                    b.HasKey("Id");

                    b.ToTable("Consumables");

                    b.HasComment("Machine consumable");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Employee first name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("Is active employee");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Employee last name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasComment("Extention of identity user");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ColorModel")
                        .HasColumnType("int")
                        .HasComment("Machine working color model");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int")
                        .HasComment("Machine printing material id.");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Printing machine model (optional");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Printing machine name");

                    b.Property<DateTime>("PrintTime")
                        .HasColumnType("datetime2")
                        .HasComment("Machine printing time for single unit");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasComment("Current status of the machine");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.ToTable("Machines");

                    b.HasComment("Printing machine");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.MachineArticle", b =>
                {
                    b.Property<int>("MachineId")
                        .HasColumnType("int")
                        .HasComment("Machine primary key");

                    b.Property<int>("ArticleId")
                        .HasColumnType("int")
                        .HasComment("Article primary key");

                    b.HasKey("MachineId", "ArticleId");

                    b.HasIndex("ArticleId");

                    b.ToTable("MachinesArticles");

                    b.HasComment("Connecting table between machines and articles (many to many)");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("InStock")
                        .HasColumnType("int")
                        .HasComment("Material current quantit in stock");

                    b.Property<double>("Lenght")
                        .HasColumnType("float")
                        .HasComment("Material lenght");

                    b.Property<int>("MeasureUnit")
                        .HasColumnType("int")
                        .HasComment("Material measure unit (enumeration)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Material price");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasComment("Material type (enumeration)");

                    b.Property<double>("Width")
                        .HasColumnType("float")
                        .HasComment("Material width");

                    b.HasKey("Id");

                    b.ToTable("Materials");

                    b.HasComment("Мaterial on which it is printed");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Order primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArticleId")
                        .HasColumnType("int")
                        .HasComment("Order article id");

                    b.Property<string>("Comment")
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)")
                        .HasComment("Additional information about the order.");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2")
                        .HasComment("Order expected end date if required from the client");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime2")
                        .HasComment("DateTime of order creation");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasComment("Order article quantity");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasComment("Order current status");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Orders");

                    b.HasComment("Order from client for print");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Article", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Client", "Client")
                        .WithMany("Articles")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.ArticleConsumable", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Article", "Article")
                        .WithMany("ArticleConsumables")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Consumable", "Consumable")
                        .WithMany("ArticleConsumables")
                        .HasForeignKey("ConsumableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Consumable");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Client", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Employee", "Merchant")
                        .WithMany()
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Machine", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.MachineArticle", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Article", "Article")
                        .WithMany("MachinesArticles")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Machine", "Machine")
                        .WithMany("MachinesArticles")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Order", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Article", "Article")
                        .WithMany("Orders")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Article", b =>
                {
                    b.Navigation("ArticleConsumables");

                    b.Navigation("MachinesArticles");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Client", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Consumable", b =>
                {
                    b.Navigation("ArticleConsumables");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Machine", b =>
                {
                    b.Navigation("MachinesArticles");
                });
#pragma warning restore 612, 618
        }
    }
}
