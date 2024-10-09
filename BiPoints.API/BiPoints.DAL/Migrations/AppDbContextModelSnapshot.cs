﻿// <auto-generated />
using System;
using BiPoints.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BiPoints.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BiPoints.API.Models.AuthenticateEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authenticates");
                });

            modelBuilder.Entity("BiPoints.DAL.Entities.ItemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeQr")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("BiPoints.DAL.Entities.PointEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BiPoints")
                        .HasColumnType("int");

                    b.Property<int>("BiPointsForUse")
                        .HasColumnType("int");

                    b.Property<int>("BiPointsUsed")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Points");
                });

            modelBuilder.Entity("BiPoints.DAL.Entities.ScanHistoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CodeQr")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<bool>("ScanSuccess")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CodeQr");

                    b.HasIndex("UserId");

                    b.ToTable("ScanHistories");
                });

            modelBuilder.Entity("BiPoints.DAL.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("AuthenticateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthenticateId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BiPoints.DAL.Entities.PointEntity", b =>
                {
                    b.HasOne("BiPoints.DAL.Entities.UserEntity", "UserEntity")
                        .WithOne("PointEntity")
                        .HasForeignKey("BiPoints.DAL.Entities.PointEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("BiPoints.DAL.Entities.ScanHistoryEntity", b =>
                {
                    b.HasOne("BiPoints.DAL.Entities.ItemEntity", "Item")
                        .WithMany("ScanHistories")
                        .HasForeignKey("CodeQr")
                        .HasPrincipalKey("CodeQr")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiPoints.DAL.Entities.UserEntity", "UserEntity")
                        .WithMany("ScanHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("BiPoints.DAL.Entities.UserEntity", b =>
                {
                    b.HasOne("BiPoints.API.Models.AuthenticateEntity", "Authenticate")
                        .WithOne("User")
                        .HasForeignKey("BiPoints.DAL.Entities.UserEntity", "AuthenticateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Authenticate");
                });

            modelBuilder.Entity("BiPoints.API.Models.AuthenticateEntity", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("BiPoints.DAL.Entities.ItemEntity", b =>
                {
                    b.Navigation("ScanHistories");
                });

            modelBuilder.Entity("BiPoints.DAL.Entities.UserEntity", b =>
                {
                    b.Navigation("PointEntity");

                    b.Navigation("ScanHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
