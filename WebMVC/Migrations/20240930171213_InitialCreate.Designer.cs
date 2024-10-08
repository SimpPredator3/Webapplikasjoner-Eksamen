﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebMVC.DAL;

#nullable disable

namespace WebMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240930171213_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("WebMVC.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "John Doe",
                            Content = "This is the first post",
                            CreatedDate = new DateTime(2024, 9, 30, 19, 12, 13, 144, DateTimeKind.Local).AddTicks(5881),
                            Title = "First Post"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Jane Smith",
                            Content = "This is the second post",
                            CreatedDate = new DateTime(2024, 9, 30, 19, 12, 13, 144, DateTimeKind.Local).AddTicks(5925),
                            Title = "Second Post"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Jim Beam",
                            Content = "This is the third post",
                            CreatedDate = new DateTime(2024, 9, 30, 19, 12, 13, 144, DateTimeKind.Local).AddTicks(5926),
                            Title = "Third Post"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
