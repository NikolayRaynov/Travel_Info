﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Travel_Info.Data;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

            modelBuilder.Entity("Travel_Info.Data.Models.ApplicationUser", b =>
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

                    b.HasData(
                        new
                        {
                            Id = "dea12856-c198-4129-b3f3-b893d8395082",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f0c7ca54-f18e-4f6f-a672-0715d1d59a23",
                            Email = "user@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@MAIL.COM",
                            NormalizedUserName = "USER@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEBtTx8TH2absM7/lhv8DrySRLlSVIRL7AOa+wouRE9MH6252ELec9dUFbxGjPGW0vg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "68ea42ef-098c-4d71-a837-6e2d8ef9e0a6",
                            TwoFactorEnabled = false,
                            UserName = "user@mail.com"
                        },
                        new
                        {
                            Id = "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ba6e25fc-062c-4c0b-8c37-6770d7048a16",
                            Email = "admin@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@MAIL.COM",
                            NormalizedUserName = "ADMIN@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAELBs9MyZa4/bbDr0C+aoZvIqLEBq1nG4s9G/2QOaIDK/sH4VYg14vKlBMsy/mKrs8g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "666d172d-0fb9-45f3-a04f-b0c21c584f1d",
                            TwoFactorEnabled = false,
                            UserName = "admin@mail.com"
                        });
                });

            modelBuilder.Entity("Travel_Info.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NameBg")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasComment("Name of the category in Cyrillic.");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasComment("Name of the category in English.");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NameBg = "Плаж",
                            NameEn = "Beach"
                        },
                        new
                        {
                            Id = 2,
                            NameBg = "Планина",
                            NameEn = "Mount"
                        },
                        new
                        {
                            Id = 3,
                            NameBg = "Разходка",
                            NameEn = "Stroll"
                        },
                        new
                        {
                            Id = 4,
                            NameBg = "Исторически",
                            NameEn = "Historical"
                        });
                });

            modelBuilder.Entity("Travel_Info.Data.Models.Destination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasComment("Identifier of the category to which the destination belongs.");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)")
                        .HasComment("Description of the destination.");

                    b.Property<int?>("FavoritePlaceId")
                        .HasColumnType("int")
                        .HasComment("Identifier of the favorite place associated with the destination.");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Indicator for logical deletion of the destination.");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(85)
                        .HasColumnType("nvarchar(85)")
                        .HasComment("Name of the destination.");

                    b.Property<int?>("PlaceToVisistId")
                        .HasColumnType("int")
                        .HasComment("Identifier of the place to visit associated with the destination.");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FavoritePlaceId");

                    b.HasIndex("PlaceToVisistId");

                    b.ToTable("Destinations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Парк Росенец (известен още като \"Отманли\") се намира в землището на село Атия,                                            около 15 км южно от Бургас и 19 километра северно от Созопол.За любителите на слънцето и морските бани съвсем близо е един от най-дивите и                          слабонаселени плажове в близост до Бургас, който ще превърне почивката ви в                           истинско удоволствие сред прекрасната природа, сгушена между тайнствената Странджа                    и красивото Черно море.",
                            FavoritePlaceId = 1,
                            IsDeleted = false,
                            Name = "Парк Росенец",
                            PlaceToVisistId = 1
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "\"Дяволското гърло\", макар и да звучи зловещо, е една от най-интересните и                                                   необикновени пещери в България. Тя е част от уникалния феномен Триградско                                                 ждрело, който се намира се в Рило-Родопската област.Пещерата ще ви предложи                                               неповторимата гледка на най-високия подземен водопад на Балканите (42м).Тя ще ви                                          пренесе в едно приключение, разкривайки част от тайните си галерии.Според                                                 легендата в Дяволското гърло Орфей губи завинаги любимата Евридика и тя потъва в                                          подземното царство на Х Страховитото име произлиза от скалната форма на входа й,                                          която наподобява дяволск глава.",
                            FavoritePlaceId = 2,
                            IsDeleted = false,
                            Name = "Дявoлcĸoтo гъpлo",
                            PlaceToVisistId = 2
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Description = "Екопътека Струилица-Калето-Лъката се намира в Родопите, по поречието на река                                               Девинска, което е със статут на защитена местност от 2002г. Край реката са изградени                                      места за отдих, подходящи за пикник и почивка на прохлада през лятото. Обособени са                                       места за риболовен туризъм, а по маршрута са поставени информационни табелки за                                           растителния и животинския свят в района.",
                            FavoritePlaceId = 1,
                            IsDeleted = false,
                            Name = "Екопътека Струилица",
                            PlaceToVisistId = 2
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 4,
                            Description = "Крепостта „Царевец“ е била главната българска крепост по времето на                                                       Втората българска държава (1185-1393 г.), има три входа, запазени и до                                                    днес. Главният е разположен в най-западната част, „Асеновата“ порта (или                                                  Малката порта) се намира в северозападния край, а на югоизток е                                                           Френкхисарската порта. Последната е охранявана от бойна кула, известна                                                    като „Балдуинова кула“. Според легендата след историческата битка при                                                     Одрин през 1205 г., в която цар Калоян пленява латинския император Балдуин                                                Фландърски, той е затворен в тази кула до края на живота си.",
                            FavoritePlaceId = 2,
                            IsDeleted = false,
                            Name = "Царевец",
                            PlaceToVisistId = 1
                        });
                });

            modelBuilder.Entity("Travel_Info.Data.Models.FavoritePlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FavoritesPlaces");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            UserId = "8b3d2e65-4498-4d45-9127-2fde83fef2a4"
                        });
                });

            modelBuilder.Entity("Travel_Info.Data.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("DestinationId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.HasKey("Id");

                    b.HasIndex("DestinationId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Парк Росенец",
                            DestinationId = 1,
                            IsDeleted = false,
                            Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTpzqgqIh5O8gA3lalctwh0VDhqphHdYRf1ow&s"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Дявoлcĸoтo гъpлo",
                            DestinationId = 2,
                            IsDeleted = false,
                            Url = "https://sunrisinglife.com/wp-content/uploads/2020/02/DSC00496.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Екопътека Струилица",
                            DestinationId = 3,
                            IsDeleted = false,
                            Url = "https://static.pochivka.bg/sights.bgstay.com/images/01/1565/54c568cb2fbea.jpg"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Царевец",
                            DestinationId = 4,
                            IsDeleted = false,
                            Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQNiTp27Uafvlivnn89hGTJIirhYgLbYHhZbw&s"
                        });
                });

            modelBuilder.Entity("Travel_Info.Data.Models.PlaceToVisit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PlacesToVisit");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            UserId = "8b3d2e65-4498-4d45-9127-2fde83fef2a4"
                        });
                });

            modelBuilder.Entity("Travel_Info.Data.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DestinationId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DestinationId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Comment = "Страхотно място!",
                            CreatedAt = new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9354),
                            DestinationId = 1,
                            IsDeleted = false,
                            Rating = 5,
                            UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9509),
                            DestinationId = 2,
                            IsDeleted = false,
                            Rating = 5,
                            UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                        },
                        new
                        {
                            Id = 3,
                            Comment = "Страхотно място!",
                            CreatedAt = new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9525),
                            DestinationId = 3,
                            IsDeleted = false,
                            Rating = 5,
                            UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9530),
                            DestinationId = 4,
                            IsDeleted = false,
                            Rating = 5,
                            UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                        });
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
                    b.HasOne("Travel_Info.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Travel_Info.Data.Models.ApplicationUser", null)
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

                    b.HasOne("Travel_Info.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Travel_Info.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Travel_Info.Data.Models.Destination", b =>
                {
                    b.HasOne("Travel_Info.Data.Models.Category", "Category")
                        .WithMany("Destinations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travel_Info.Data.Models.FavoritePlace", "FavoritePlace")
                        .WithMany("Destinations")
                        .HasForeignKey("FavoritePlaceId");

                    b.HasOne("Travel_Info.Data.Models.PlaceToVisit", "PlaceToVisit")
                        .WithMany("Destinations")
                        .HasForeignKey("PlaceToVisistId");

                    b.Navigation("Category");

                    b.Navigation("FavoritePlace");

                    b.Navigation("PlaceToVisit");
                });

            modelBuilder.Entity("Travel_Info.Data.Models.FavoritePlace", b =>
                {
                    b.HasOne("Travel_Info.Data.Models.ApplicationUser", "User")
                        .WithMany("FavoritesPlaces")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Travel_Info.Data.Models.Image", b =>
                {
                    b.HasOne("Travel_Info.Data.Models.Destination", "Destination")
                        .WithMany("Images")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");
                });

            modelBuilder.Entity("Travel_Info.Data.Models.PlaceToVisit", b =>
                {
                    b.HasOne("Travel_Info.Data.Models.ApplicationUser", "User")
                        .WithMany("PlacesToVisit")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Travel_Info.Data.Models.Review", b =>
                {
                    b.HasOne("Travel_Info.Data.Models.Destination", "Destination")
                        .WithMany("Reviews")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travel_Info.Data.Models.ApplicationUser", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Travel_Info.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("FavoritesPlaces");

                    b.Navigation("PlacesToVisit");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Travel_Info.Data.Models.Category", b =>
                {
                    b.Navigation("Destinations");
                });

            modelBuilder.Entity("Travel_Info.Data.Models.Destination", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Travel_Info.Data.Models.FavoritePlace", b =>
                {
                    b.Navigation("Destinations");
                });

            modelBuilder.Entity("Travel_Info.Data.Models.PlaceToVisit", b =>
                {
                    b.Navigation("Destinations");
                });
#pragma warning restore 612, 618
        }
    }
}
