﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Travel_Info.Data;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250124193100_AddConstForComment")]
    partial class AddConstForComment
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            ConcurrencyStamp = "fee8000d-a7b3-48d8-80bc-6f6dbee341fa",
                            Email = "user@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@MAIL.COM",
                            NormalizedUserName = "USER@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEKyPN7a7pBaf/mdaWbkMRap/2FtCz1afZcSx5gnDR004xaezHaZrxXk2O77UqIVIYA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "4b2018cf-f65c-4728-90c3-d2276f574e4b",
                            TwoFactorEnabled = false,
                            UserName = "user@mail.com"
                        },
                        new
                        {
                            Id = "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "41dc3207-8f97-4f56-b7ff-4f9876f258de",
                            Email = "admin@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@MAIL.COM",
                            NormalizedUserName = "ADMIN@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEOYS5u3V84O3KRrsMkIvj6QXI84NLAod7hjvPoOGw4UvONAxRLaufAJMNhU9GS5o0A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "396b0be5-4e65-4f37-927c-cb6d741a669c",
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Море"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Планина"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Разходка"
                        },
                        new
                        {
                            Id = 4,
                            Name = "История"
                        });
                });

            modelBuilder.Entity("Travel_Info.Data.Models.Destination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<int>("FavoritePlaceId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(85)
                        .HasColumnType("nvarchar(85)");

                    b.Property<int>("PlaceToVisistId")
                        .HasColumnType("int");

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
                            Name = "Парк Росенец",
                            PlaceToVisistId = 1
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "\"Дяволското гърло\", макар и да звучи зловещо, е една от най-интересните и                                                   необикновени пещери в България. Тя е част от уникалния феномен Триградско                                                 ждрело, който се намира се в Рило-Родопската област.Пещерата ще ви предложи                                               неповторимата гледка на най-високия подземен водопад на Балканите (42м).Тя ще ви                                          пренесе в едно приключение, разкривайки част от тайните си галерии.Според                                                 легендата в Дяволското гърло Орфей губи завинаги любимата Евридика и тя потъва в                                          подземното царство на Х Страховитото име произлиза от скалната форма на входа й,                                          която наподобява дяволск глава.",
                            FavoritePlaceId = 2,
                            Name = "Дявoлcĸoтo гъpлo",
                            PlaceToVisistId = 2
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Description = "Екопътека Струилица-Калето-Лъката се намира в Родопите, по поречието на река                                               Девинска, което е със статут на защитена местност от 2002г. Край реката са изградени                                      места за отдих, подходящи за пикник и почивка на прохлада през лятото. Обособени са                                       места за риболовен туризъм, а по маршрута са поставени информационни табелки за                                           растителния и животинския свят в района.",
                            FavoritePlaceId = 1,
                            Name = "Екопътека Струилица",
                            PlaceToVisistId = 2
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 4,
                            Description = "Екопътека Струилица-Калето-Лъката се намира в Родопите, по поречието на                                                   река Девинска, което е със статут на защитена местност от 2002г. Край                                                     реката са изградени места за отдих, подходящи за пикник и почивка на                                                      прохлада през лятото. Обособени са места за риболовен туризъм, а по                                                       маршрута са поставени  информационни табелки за растителния и животинския                                                 свят в района.",
                            FavoritePlaceId = 2,
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
                            UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                        },
                        new
                        {
                            Id = 2,
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
                            Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTpzqgqIh5O8gA3lalctwh0VDhqphHdYRf1ow&s"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Дявoлcĸoтo гъpлo",
                            DestinationId = 2,
                            Url = "https://sunrisinglife.com/wp-content/uploads/2020/02/DSC00496.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Екопътека Струилица",
                            DestinationId = 3,
                            Url = "https://static.pochivka.bg/sights.bgstay.com/images/01/1565/54c568cb2fbea.jpg"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Царевец",
                            DestinationId = 4,
                            Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQNiTp27Uafvlivnn89hGTJIirhYgLbYHhZbw&s"
                        });
                });

            modelBuilder.Entity("Travel_Info.Data.Models.PlaceToVisit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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
                            UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                        },
                        new
                        {
                            Id = 2,
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

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DestinationId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
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
                        .HasForeignKey("FavoritePlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travel_Info.Data.Models.PlaceToVisit", "PlaceToVisit")
                        .WithMany("Destinations")
                        .HasForeignKey("PlaceToVisistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
