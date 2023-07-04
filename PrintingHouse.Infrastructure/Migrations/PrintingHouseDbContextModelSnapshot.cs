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
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

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

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Account.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Employee first name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("Is active employee (soft delete property)");

                    b.Property<string>("LastName")
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

                    b.Property<string>("PictureName")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasComment("Picture name of the user (nullable)");

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

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Article primary key.");

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasComment("Article owner id");

                    b.Property<int>("ColorModelId")
                        .HasColumnType("int")
                        .HasComment("Article color model id");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasComment("Name of design image");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Soft delete boolean property");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int")
                        .HasComment("Article material");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasComment("Article name.");

                    b.Property<double>("RequiredMaterial")
                        .HasColumnType("float")
                        .HasComment("Required material lenght");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ColorModelId");

                    b.HasIndex("MaterialId");

                    b.ToTable("Articles");

                    b.HasComment("Particular client article ready for print.");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.ArticleConsumable", b =>
                {
                    b.Property<Guid>("ArticleId")
                        .HasColumnType("uniqueidentifier")
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Soft delete propery");

                    b.Property<int>("MerchantId")
                        .HasColumnType("int")
                        .HasComment("Client's merchant id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasComment("Client name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Client phone number");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId");

                    b.ToTable("Clients");

                    b.HasComment("Printing house client");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.ColorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasComment("Color model name");

                    b.HasKey("Id");

                    b.ToTable("ColorModels");

                    b.HasComment("Color model");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "RGB"
                        },
                        new
                        {
                            Id = 2,
                            Name = "CMYK"
                        });
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

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasComment("Consumable price");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasComment("Consumable type name");

                    b.HasKey("Id");

                    b.ToTable("Consumables");

                    b.HasComment("Machine consumable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InStock = 104,
                            Price = 50m,
                            Type = "Red"
                        },
                        new
                        {
                            Id = 2,
                            InStock = 92,
                            Price = 48m,
                            Type = "Green"
                        },
                        new
                        {
                            Id = 3,
                            InStock = 67,
                            Price = 57m,
                            Type = "Blue"
                        },
                        new
                        {
                            Id = 4,
                            InStock = 47,
                            Price = 52m,
                            Type = "Cyan"
                        },
                        new
                        {
                            Id = 5,
                            InStock = 38,
                            Price = 55m,
                            Type = "Magenta"
                        },
                        new
                        {
                            Id = 6,
                            InStock = 50,
                            Price = 47m,
                            Type = "Yellow"
                        },
                        new
                        {
                            Id = 7,
                            InStock = 60,
                            Price = 40m,
                            Type = "Black"
                        });
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Employee application user id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("Soft delete property");

                    b.Property<int>("PositionId")
                        .HasColumnType("int")
                        .HasComment("Employee office position id");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");

                    b.HasComment("Employee entity");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ColorModelId")
                        .HasColumnType("int")
                        .HasComment("Machine working color model id");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int")
                        .HasComment("Machine printing material id.");

                    b.Property<double>("MaterialPerPrint")
                        .HasColumnType("float")
                        .HasComment("Material required for single print");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Printing machine model (optional");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Printing machine name");

                    b.Property<TimeSpan>("PrintTime")
                        .HasColumnType("time")
                        .HasComment("Machine printing time for single unit");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasComment("Current status of the machine (has default value)");

                    b.HasKey("Id");

                    b.HasIndex("ColorModelId");

                    b.HasIndex("MaterialId");

                    b.ToTable("Machines");

                    b.HasComment("Printing machine");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ColorModelId = 2,
                            MaterialId = 2,
                            MaterialPerPrint = 5.0,
                            Name = "Machine 1",
                            PrintTime = new TimeSpan(0, 0, 3, 0, 0),
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            ColorModelId = 2,
                            MaterialId = 2,
                            MaterialPerPrint = 5.0,
                            Name = "Machine 2",
                            PrintTime = new TimeSpan(0, 0, 2, 30, 0),
                            Status = 0
                        },
                        new
                        {
                            Id = 3,
                            ColorModelId = 1,
                            MaterialId = 1,
                            MaterialPerPrint = 1.0,
                            Name = "Machine 3",
                            PrintTime = new TimeSpan(0, 0, 0, 3, 0),
                            Status = 0
                        },
                        new
                        {
                            Id = 4,
                            ColorModelId = 2,
                            MaterialId = 3,
                            MaterialPerPrint = 1.0,
                            Name = "Machine 4",
                            PrintTime = new TimeSpan(0, 0, 40, 0, 0),
                            Status = 0
                        });
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

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("Soft delete property");

                    b.Property<double>("Lenght")
                        .HasColumnType("float")
                        .HasComment("Material lenght");

                    b.Property<int>("MeasureUnit")
                        .HasColumnType("int")
                        .HasComment("Material measure unit (enumeration)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasComment("Material price");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasComment("Material type name");

                    b.Property<double>("Width")
                        .HasColumnType("float")
                        .HasComment("Material width");

                    b.HasKey("Id");

                    b.ToTable("Materials");

                    b.HasComment("Мaterial on which it is printed");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InStock = 10000,
                            IsActive = true,
                            Lenght = 594.0,
                            MeasureUnit = 1,
                            Price = 1m,
                            Type = "Plain paper A2",
                            Width = 420.0
                        },
                        new
                        {
                            Id = 2,
                            InStock = 100,
                            IsActive = true,
                            Lenght = 0.01,
                            MeasureUnit = 0,
                            Price = 1500.50m,
                            Type = "Vinil 2m",
                            Width = 0.002
                        },
                        new
                        {
                            Id = 3,
                            InStock = 20,
                            IsActive = true,
                            Lenght = 1.0,
                            MeasureUnit = 0,
                            Price = 850m,
                            Type = "Nylon 20cm",
                            Width = 0.00020000000000000001
                        });
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Order primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("ArticleId")
                        .HasColumnType("uniqueidentifier")
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

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("Soft delete property");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Position name");

                    b.HasKey("Id");

                    b.ToTable("Positions");

                    b.HasComment("Office position");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            Name = "Merchant"
                        },
                        new
                        {
                            Id = 3,
                            IsActive = true,
                            Name = "Employee"
                        },
                        new
                        {
                            Id = 4,
                            IsActive = true,
                            Name = "Designer"
                        },
                        new
                        {
                            Id = 5,
                            IsActive = true,
                            Name = "Manager"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Account.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Account.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Account.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Account.ApplicationUser", null)
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

                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.ColorModel", "ColorModel")
                        .WithMany("Articles")
                        .HasForeignKey("ColorModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Material", "Material")
                        .WithMany("Articles")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("ColorModel");

                    b.Navigation("Material");
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
                        .WithMany("Clients")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Employee", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Account.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Machine", b =>
                {
                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.ColorModel", "ColorModel")
                        .WithMany("Machines")
                        .HasForeignKey("ColorModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintingHouse.Infrastructure.Data.Entities.Material", "Material")
                        .WithMany("Machines")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ColorModel");

                    b.Navigation("Material");
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

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Client", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.ColorModel", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Machines");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Consumable", b =>
                {
                    b.Navigation("ArticleConsumables");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Employee", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Material", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Machines");
                });

            modelBuilder.Entity("PrintingHouse.Infrastructure.Data.Entities.Position", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
