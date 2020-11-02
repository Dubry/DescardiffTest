﻿// <auto-generated />
using DescartesDiff.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DescartesDiff.Migrations
{
    [DbContext(typeof(DiffContext))]
    [Migration("20201101102256_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("DescartesDiff.Models.DataModel", b =>
                {
                    b.Property<int>("DataModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LeftBase")
                        .HasColumnType("TEXT");

                    b.Property<string>("RightBase")
                        .HasColumnType("TEXT");

                    b.HasKey("DataModelId");

                    b.ToTable("DiffResults");
                });
#pragma warning restore 612, 618
        }
    }
}
