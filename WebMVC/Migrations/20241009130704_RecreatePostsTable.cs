using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebMVC.Migrations
{
    /// <inheritdoc />
    public partial class RecreatePostsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "Content", "CreatedDate", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { 1, "John Doe", "This is the first post", new DateTime(2024, 10, 1, 16, 41, 36, 638, DateTimeKind.Local).AddTicks(5470), null, "First Post" },
                    { 2, "Jane Smith", "This is the second post", new DateTime(2024, 10, 1, 16, 41, 36, 638, DateTimeKind.Local).AddTicks(5511), null, "Second Post" },
                    { 3, "Jim Beam", "This is the third post", new DateTime(2024, 10, 1, 16, 41, 36, 638, DateTimeKind.Local).AddTicks(5514), null, "Third Post" }
                });
        }
    }
}
