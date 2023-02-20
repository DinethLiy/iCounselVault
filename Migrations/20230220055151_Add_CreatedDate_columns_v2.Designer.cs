﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using icounselvault.Utility;

#nullable disable

namespace icounselvault.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230220055151_Add_CreatedDate_columns_v2")]
    partial class Add_CreatedDate_columns_v2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("icounselvault.Models.Auth.User", b =>
                {
                    b.Property<int>("USER_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PASSWORD")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PRIVILEGE_TYPE")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("USERNAME")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("USER_STATUS")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("USER_ID");

                    b.ToTable("USER");
                });

            modelBuilder.Entity("icounselvault.Models.Counseling.ClientGuidanceHistory", b =>
                {
                    b.Property<int>("CLIENT_GUIDANCE_HISTORY_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CLIENT_ID")
                        .HasColumnType("int");

                    b.Property<string>("CLIENT_SATISFACTION")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("COUNSELOR_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CREATED_DATE")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EXTERNAL_GUIDANCE_SOURCE")
                        .HasColumnType("longtext");

                    b.Property<string>("GUIDANCE_ADVICE")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HISTORY_STATUS")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CLIENT_GUIDANCE_HISTORY_ID");

                    b.HasIndex("CLIENT_ID");

                    b.HasIndex("COUNSELOR_ID");

                    b.ToTable("CLIENT_GUIDANCE_HISTORY");
                });

            modelBuilder.Entity("icounselvault.Models.Counseling.CounselDataInsertRequest", b =>
                {
                    b.Property<int>("COUNSEL_DATA_INSERT_REQUEST_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CLIENT_ID")
                        .HasColumnType("int");

                    b.Property<int?>("COUNSELOR_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CREATED_DATE")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("INSERT_REQUEST_REMARK")
                        .HasColumnType("longtext");

                    b.Property<string>("INSERT_REQUEST_STATUS")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("clientGuidanceHistoryCLIENT_GUIDANCE_HISTORY_ID")
                        .HasColumnType("int");

                    b.HasKey("COUNSEL_DATA_INSERT_REQUEST_ID");

                    b.HasIndex("CLIENT_ID");

                    b.HasIndex("COUNSELOR_ID");

                    b.HasIndex("clientGuidanceHistoryCLIENT_GUIDANCE_HISTORY_ID");

                    b.ToTable("COUNSEL_DATA_INSERT_REQUEST");
                });

            modelBuilder.Entity("icounselvault.Models.Counseling.CounselRequest", b =>
                {
                    b.Property<int>("COUNSEL_REQUEST_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CLIENT_ID")
                        .HasColumnType("int");

                    b.Property<int>("COUNSELOR_ID")
                        .HasColumnType("int");

                    b.Property<string>("COUNSEL_REQUEST_REMARK")
                        .HasColumnType("longtext");

                    b.Property<string>("COUNSEL_REQUEST_STATUS")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CREATED_DATE")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FROM_DATE")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("TO_DATE")
                        .HasColumnType("datetime(6)");

                    b.HasKey("COUNSEL_REQUEST_ID");

                    b.HasIndex("CLIENT_ID");

                    b.HasIndex("COUNSELOR_ID");

                    b.ToTable("COUNSEL_REQUEST");
                });

            modelBuilder.Entity("icounselvault.Models.Profiles.Client", b =>
                {
                    b.Property<int>("CLIENT_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ADDRESS")
                        .HasColumnType("longtext");

                    b.Property<Guid>("CLIENT_CODE")
                        .HasColumnType("char(36)");

                    b.Property<string>("CLIENT_STATUS")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CONTACT_NUM")
                        .HasColumnType("longtext");

                    b.Property<string>("COUNTRY")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EMAIL")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GENDER")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NAME")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NIC")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("User")
                        .HasColumnType("int");

                    b.HasKey("CLIENT_ID");

                    b.HasIndex("User");

                    b.ToTable("CLIENT");
                });

            modelBuilder.Entity("icounselvault.Models.Profiles.ClientExperience", b =>
                {
                    b.Property<int>("CLIENT_EXPERIENCE_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Client")
                        .HasColumnType("int");

                    b.Property<string>("HIGHER_EDU_EXPERIENCE")
                        .HasColumnType("longtext");

                    b.Property<string>("JOB_EXPERIENCE")
                        .HasColumnType("longtext");

                    b.Property<string>("PREFERED_CAREER_FIELD")
                        .HasColumnType("longtext");

                    b.Property<string>("SCHOOL_EXPERIENCE")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CLIENT_EXPERIENCE_ID");

                    b.HasIndex("Client");

                    b.ToTable("CLIENT_EXPERIENCE");
                });

            modelBuilder.Entity("icounselvault.Models.Profiles.Counselor", b =>
                {
                    b.Property<int>("COUNSELOR_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ADDRESS")
                        .HasColumnType("longtext");

                    b.Property<string>("CONTACT_NUM")
                        .HasColumnType("longtext");

                    b.Property<string>("COUNSELOR_STATUS")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("COUNTRY")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EMAIL")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GENDER")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NAME")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NIC")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("User")
                        .HasColumnType("int");

                    b.HasKey("COUNSELOR_ID");

                    b.HasIndex("User");

                    b.ToTable("COUNSELOR");
                });

            modelBuilder.Entity("icounselvault.Models.Profiles.CounselorExperience", b =>
                {
                    b.Property<int>("COUNSELOR_EXPERIENCE_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Counselor")
                        .HasColumnType("int");

                    b.Property<string>("HIGHER_EDU_EXPERIENCE")
                        .HasColumnType("longtext");

                    b.Property<string>("JOB_EXPERIENCE")
                        .HasColumnType("longtext");

                    b.Property<string>("SCHOOL_EXPERIENCE")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("COUNSELOR_EXPERIENCE_ID");

                    b.HasIndex("Counselor");

                    b.ToTable("COUNSELOR_EXPERIENCE");
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("icounselvault.Models.Counseling.ClientGuidanceHistory", b =>
                {
                    b.HasOne("icounselvault.Models.Profiles.Client", "client")
                        .WithMany()
                        .HasForeignKey("CLIENT_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("icounselvault.Models.Profiles.Counselor", "counselor")
                        .WithMany()
                        .HasForeignKey("COUNSELOR_ID");

                    b.Navigation("client");

                    b.Navigation("counselor");
                });

            modelBuilder.Entity("icounselvault.Models.Counseling.CounselDataInsertRequest", b =>
                {
                    b.HasOne("icounselvault.Models.Profiles.Client", "client")
                        .WithMany()
                        .HasForeignKey("CLIENT_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("icounselvault.Models.Profiles.Counselor", "counselor")
                        .WithMany()
                        .HasForeignKey("COUNSELOR_ID");

                    b.HasOne("icounselvault.Models.Counseling.ClientGuidanceHistory", "clientGuidanceHistory")
                        .WithMany()
                        .HasForeignKey("clientGuidanceHistoryCLIENT_GUIDANCE_HISTORY_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("client");

                    b.Navigation("clientGuidanceHistory");

                    b.Navigation("counselor");
                });

            modelBuilder.Entity("icounselvault.Models.Counseling.CounselRequest", b =>
                {
                    b.HasOne("icounselvault.Models.Profiles.Client", "client")
                        .WithMany()
                        .HasForeignKey("CLIENT_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("icounselvault.Models.Profiles.Counselor", "counselor")
                        .WithMany()
                        .HasForeignKey("COUNSELOR_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("client");

                    b.Navigation("counselor");
                });

            modelBuilder.Entity("icounselvault.Models.Profiles.Client", b =>
                {
                    b.HasOne("icounselvault.Models.Auth.User", "user")
                        .WithMany()
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("icounselvault.Models.Profiles.ClientExperience", b =>
                {
                    b.HasOne("icounselvault.Models.Profiles.Client", "client")
                        .WithMany()
                        .HasForeignKey("Client")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("client");
                });

            modelBuilder.Entity("icounselvault.Models.Profiles.Counselor", b =>
                {
                    b.HasOne("icounselvault.Models.Auth.User", "user")
                        .WithMany()
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("icounselvault.Models.Profiles.CounselorExperience", b =>
                {
                    b.HasOne("icounselvault.Models.Profiles.Counselor", "counselor")
                        .WithMany()
                        .HasForeignKey("Counselor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("counselor");
                });
#pragma warning restore 612, 618
        }
    }
}
