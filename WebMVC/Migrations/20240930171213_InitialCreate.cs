﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "Content", "CreatedDate", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { 1, "John Doe", "This is the first post", new DateTime(2024, 9, 30, 19, 12, 13, 144, DateTimeKind.Local).AddTicks(5881), null, "First Post" },
                    { 2, "Jane Smith", "This is the second post", new DateTime(2024, 9, 30, 19, 12, 13, 144, DateTimeKind.Local).AddTicks(5925), null, "Second Post" },
                    { 3, "Jim Beam", "This is the third post", new DateTime(2024, 9, 30, 19, 12, 13, 144, DateTimeKind.Local).AddTicks(5926), null, "Third Post" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
