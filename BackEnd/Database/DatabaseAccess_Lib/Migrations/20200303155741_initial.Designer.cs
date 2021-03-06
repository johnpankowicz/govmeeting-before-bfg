﻿// <auto-generated />
using System;
using GM.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseAccess_Lib.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200303155741_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GM.DatabaseAccess.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
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

            modelBuilder.Entity("GM.DatabaseModel.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("TopicId");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("GM.DatabaseModel.GovernmentBody", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country");

                    b.Property<string>("County");

                    b.Property<string>("Municipality");

                    b.Property<string>("Name");

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("GovernmentBodies");
                });

            modelBuilder.Entity("GM.DatabaseModel.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GovernmentBodyId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GovernmentBodyId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("GM.DatabaseModel.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("GovernmentBodyId");

                    b.Property<int>("Length");

                    b.Property<string>("Name");

                    b.Property<int>("WorkStatus");

                    b.HasKey("Id");

                    b.HasIndex("GovernmentBodyId");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("GM.DatabaseModel.Speaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Speakers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Speaker");
                });

            modelBuilder.Entity("GM.DatabaseModel.Talk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Sequence");

                    b.Property<int>("SpeakerId");

                    b.Property<string>("Text");

                    b.Property<int>("TopicDiscussionId");

                    b.HasKey("Id");

                    b.HasIndex("SpeakerId");

                    b.HasIndex("TopicDiscussionId");

                    b.ToTable("Talks");
                });

            modelBuilder.Entity("GM.DatabaseModel.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GovernmentBodyId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GovernmentBodyId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("GM.DatabaseModel.TopicDiscussion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MeetingId");

                    b.Property<int>("Sequence");

                    b.Property<int?>("TopicId");

                    b.HasKey("Id");

                    b.HasIndex("MeetingId");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicDiscussions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GM.DatabaseModel.Citizen", b =>
                {
                    b.HasBaseType("GM.DatabaseModel.Speaker");

                    b.HasDiscriminator().HasValue("Citizen");
                });

            modelBuilder.Entity("GM.DatabaseModel.Representative", b =>
                {
                    b.HasBaseType("GM.DatabaseModel.Speaker");

                    b.Property<int>("GovernmentBodyId");

                    b.Property<string>("Identifier");

                    b.HasDiscriminator().HasValue("Representative");
                });

            modelBuilder.Entity("GM.DatabaseModel.Category", b =>
                {
                    b.HasOne("GM.DatabaseModel.Topic")
                        .WithMany("Categories")
                        .HasForeignKey("TopicId");
                });

            modelBuilder.Entity("GM.DatabaseModel.Language", b =>
                {
                    b.HasOne("GM.DatabaseModel.GovernmentBody")
                        .WithMany("Languages")
                        .HasForeignKey("GovernmentBodyId");
                });

            modelBuilder.Entity("GM.DatabaseModel.Meeting", b =>
                {
                    b.HasOne("GM.DatabaseModel.GovernmentBody")
                        .WithMany("Meetings")
                        .HasForeignKey("GovernmentBodyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GM.DatabaseModel.Talk", b =>
                {
                    b.HasOne("GM.DatabaseModel.Speaker", "Speaker")
                        .WithMany()
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GM.DatabaseModel.TopicDiscussion")
                        .WithMany("Talks")
                        .HasForeignKey("TopicDiscussionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GM.DatabaseModel.Topic", b =>
                {
                    b.HasOne("GM.DatabaseModel.GovernmentBody")
                        .WithMany("Topics")
                        .HasForeignKey("GovernmentBodyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GM.DatabaseModel.TopicDiscussion", b =>
                {
                    b.HasOne("GM.DatabaseModel.Meeting")
                        .WithMany("TopicDiscussions")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GM.DatabaseModel.Topic", "Topic")
                        .WithMany("TopicDiscussions")
                        .HasForeignKey("TopicId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GM.DatabaseAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GM.DatabaseAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GM.DatabaseAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GM.DatabaseAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
