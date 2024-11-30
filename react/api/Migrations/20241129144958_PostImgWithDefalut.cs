using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class PostImgWithDefalut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(74));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(75), "images/post2.webp" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(76), "images/post3.webp" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(77), null });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(78), "images/post2.webp" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(79), null });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(80), "images/post1.webp" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(81), null });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(82), "images/post3.webp" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(83), null });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(84), "images/post1.webp" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(85), null });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(86), null });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(87), null });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(88), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4615));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4617), "https://example.com/images/online-classes.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4618), "https://example.com/images/time-management.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4619), "https://example.com/images/calculus-help.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4620), "https://example.com/images/note-taking.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4621), "https://example.com/images/biology-notes.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4621), "https://example.com/images/procrastination.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4622), "https://example.com/images/group-project-deadline.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4623), "https://example.com/images/study-space.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4624), "https://example.com/images/physics-solutions.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4625), "https://example.com/images/exam-preparation.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4626), "https://example.com/images/chemistry-lab.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4627), "https://example.com/images/math-study-group.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4628), "https://example.com/images/biology-flashcards.jpg" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 54, 6, 715, DateTimeKind.Utc).AddTicks(4629), "https://example.com/images/history-dates.jpg" });
        }
    }
}
