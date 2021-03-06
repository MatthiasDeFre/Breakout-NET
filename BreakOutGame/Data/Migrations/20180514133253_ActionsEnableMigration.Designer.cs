﻿// <auto-generated />
using BreakOutGame.Data;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.GroupOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BreakOutGame.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180514133253_ActionsEnableMigration")]
    partial class ActionsEnableMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BreakOutGame.Models.ApplicationUser", b =>
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

            modelBuilder.Entity("BreakOutGame.Models.Domain.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessCode");

                    b.Property<int>("EXERCISE_ID");

                    b.Property<int>("GROUPOPERATION_ID");

                    b.Property<int>("ReferenceNr");

                    b.Property<int>("SessionPathId")
                        .HasColumnName("PathId");

                    b.Property<int>("Status");

                    b.Property<int>("WrongCount");

                    b.HasKey("Id");

                    b.HasIndex("EXERCISE_ID");

                    b.HasIndex("GROUPOPERATION_ID");

                    b.HasIndex("SessionPathId");

                    b.ToTable("Assignment");
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.BoBAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Action")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("BOBACTION");
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.BoBGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<int?>("BoBSessionId");

                    b.Property<string>("GroupName")
                        .HasColumnName("name");

                    b.Property<int>("PathId")
                        .HasColumnName("PATH_ID");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("BoBSessionId");

                    b.HasIndex("PathId")
                        .IsUnique();

                    b.ToTable("BOBGROUP");
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.BoBSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BoxId")
                        .HasColumnName("BOX_ID");

                    b.Property<int>("SessionStatus")
                        .HasColumnName("SESSIONSTATUS");

                    b.Property<string>("name");

                    b.HasKey("Id");

                    b.ToTable("BoBSession");
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<string>("Feedback")
                        .HasColumnName("feedback");

                    b.Property<string>("Name");

                    b.Property<string>("PDF")
                        .HasColumnName("assignment");

                    b.Property<int>("Time")
                        .HasColumnName("timeInMinutes");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.GroupOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupOperationCategory")
                        .HasColumnName("category");

                    b.Property<string>("ValueString");

                    b.HasKey("Id");

                    b.ToTable("GroupOperation");
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.GroupStudent", b =>
                {
                    b.Property<int>("BoBGroupId")
                        .HasColumnName("BoBGroup_ID");

                    b.Property<int>("StudentId")
                        .HasColumnName("students_ID");

                    b.HasKey("BoBGroupId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("BOBGROUP_Student");
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.SessionAction", b =>
                {
                    b.Property<int>("BoxId")
                        .HasColumnName("BOX_ID");

                    b.Property<int>("ActionId")
                        .HasColumnName("actions_ID");

                    b.Property<int>("OrderNumber")
                        .HasColumnName("actions_ORDER");

                    b.HasKey("BoxId", "ActionId");

                    b.HasIndex("ActionId");

                    b.ToTable("BOX_BOBACTION");
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.SessionPath", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("SessionPath");
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClassNumber")
                        .HasColumnName("CLASSNUMBER");

                    b.Property<string>("FirstName")
                        .HasColumnName("FIRSTNAME");

                    b.Property<string>("LastName")
                        .HasColumnName("LASTNAME");

                    b.HasKey("Id");

                    b.ToTable("Student");
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
                        .ValueGeneratedOnAdd();

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
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("BreakOutGame.Models.Domain.Assignment", b =>
                {
                    b.HasOne("BreakOutGame.Models.Domain.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("EXERCISE_ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BreakOutGame.Models.Domain.GroupOperation", "GroupOperation")
                        .WithMany()
                        .HasForeignKey("GROUPOPERATION_ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BreakOutGame.Models.Domain.SessionPath")
                        .WithMany("Assignments")
                        .HasForeignKey("SessionPathId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.BoBGroup", b =>
                {
                    b.HasOne("BreakOutGame.Models.Domain.BoBSession")
                        .WithMany("Groups")
                        .HasForeignKey("BoBSessionId");

                    b.HasOne("BreakOutGame.Models.Domain.SessionPath", "Path")
                        .WithOne()
                        .HasForeignKey("BreakOutGame.Models.Domain.BoBGroup", "PathId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.GroupStudent", b =>
                {
                    b.HasOne("BreakOutGame.Models.Domain.BoBGroup", "Group")
                        .WithMany("Students")
                        .HasForeignKey("BoBGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BreakOutGame.Models.Domain.Student", "Student")
                        .WithMany("Groups")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BreakOutGame.Models.Domain.SessionAction", b =>
                {
                    b.HasOne("BreakOutGame.Models.Domain.BoBAction", "Action")
                        .WithMany("Session")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BreakOutGame.Models.Domain.BoBSession", "Session")
                        .WithMany("Actions")
                        .HasForeignKey("BoxId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                    b.HasOne("BreakOutGame.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BreakOutGame.Models.ApplicationUser")
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

                    b.HasOne("BreakOutGame.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BreakOutGame.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
