﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pseudo_MD_DoC_API.Persistence;

namespace Pseudo_MD_DoC_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pseudo_MD_DoC_API.Applications.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("ApplicationStatus")
                        .HasColumnType("int");

                    b.Property<string>("CellPhone")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<DateTime?>("DateBackgroundCheckComplete")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateProcessed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateReceived")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<bool>("HasFelony")
                        .HasColumnType("bit");

                    b.Property<string>("HomePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsUsCitizen")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("MiddleInitial")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("SocialSecurityNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(9)")
                        .HasMaxLength(9);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<int?>("TestScore")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("WillDrugTest")
                        .HasColumnType("bit");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Pseudo_MD_DoC_API.Applications.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("Pseudo_MD_DoC_API.Applications.Employment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<bool>("CanContact")
                        .HasColumnType("bit");

                    b.Property<string>("EmployerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Employment");
                });

            modelBuilder.Entity("Pseudo_MD_DoC_API.Applications.Reference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Reference");
                });

            modelBuilder.Entity("Pseudo_MD_DoC_API.Entities.Configuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("propertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("propertyValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Configuration");
                });

            modelBuilder.Entity("Pseudo_MD_DoC_API.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AccountVerified")
                        .HasColumnType("bit");

                    b.Property<string>("AccountVerifyToken")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<bool>("Administrator")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("ResetPasswordExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Pseudo_MD_DoC_API.Applications.Application", b =>
                {
                    b.HasOne("Pseudo_MD_DoC_API.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pseudo_MD_DoC_API.Applications.Education", b =>
                {
                    b.HasOne("Pseudo_MD_DoC_API.Applications.Application", null)
                        .WithMany("Education")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("Pseudo_MD_DoC_API.Applications.Employment", b =>
                {
                    b.HasOne("Pseudo_MD_DoC_API.Applications.Application", null)
                        .WithMany("Employment")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("Pseudo_MD_DoC_API.Applications.Reference", b =>
                {
                    b.HasOne("Pseudo_MD_DoC_API.Applications.Application", null)
                        .WithMany("References")
                        .HasForeignKey("ApplicationId");
                });
#pragma warning restore 612, 618
        }
    }
}
