﻿// <auto-generated />
using System;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    [Migration("20231109053356_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DataAccess.Models.Branch", b =>
                {
                    b.Property<short>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("BranchId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("DataAccess.Models.InventoryInDetail", b =>
                {
                    b.Property<int>("InventoryInDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BatchNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("ConsumerPrice")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<DateOnly>("ExpireDate")
                        .HasColumnType("date");

                    b.Property<int>("InventoryInHeaderId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<byte>("PackageId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<decimal>("Quantity")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<int>("Serial")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("InventoryInDetailId");

                    b.HasIndex("InventoryInHeaderId");

                    b.HasIndex("ItemId");

                    b.HasIndex("PackageId");

                    b.ToTable("InventoryInDetails");
                });

            modelBuilder.Entity("DataAccess.Models.InventoryInHeader", b =>
                {
                    b.Property<int>("InventoryInHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<short>("BranchId")
                        .HasColumnType("smallint");

                    b.Property<DateOnly>("DocDate")
                        .HasColumnType("date");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.HasKey("InventoryInHeaderId");

                    b.HasIndex("BranchId");

                    b.ToTable("InventoryInHeaders");
                });

            modelBuilder.Entity("DataAccess.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("DataAccess.Models.Package", b =>
                {
                    b.Property<byte>("PackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("PackageId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("DataAccess.Models.InventoryInDetail", b =>
                {
                    b.HasOne("DataAccess.Models.InventoryInHeader", "InventoryInHeader")
                        .WithMany("InventoryInDetails")
                        .HasForeignKey("InventoryInHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Item", "Item")
                        .WithMany("InventoryInDetails")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Package", "Package")
                        .WithMany("InventoryInDetails")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InventoryInHeader");

                    b.Navigation("Item");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("DataAccess.Models.InventoryInHeader", b =>
                {
                    b.HasOne("DataAccess.Models.Branch", "Branch")
                        .WithMany("InventoryInHeaders")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("DataAccess.Models.Branch", b =>
                {
                    b.Navigation("InventoryInHeaders");
                });

            modelBuilder.Entity("DataAccess.Models.InventoryInHeader", b =>
                {
                    b.Navigation("InventoryInDetails");
                });

            modelBuilder.Entity("DataAccess.Models.Item", b =>
                {
                    b.Navigation("InventoryInDetails");
                });

            modelBuilder.Entity("DataAccess.Models.Package", b =>
                {
                    b.Navigation("InventoryInDetails");
                });
#pragma warning restore 612, 618
        }
    }
}