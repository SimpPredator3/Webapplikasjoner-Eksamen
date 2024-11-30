using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUpvoteCountPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3616), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3618), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3619), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3620), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3620), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3621), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3622), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3623), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3624), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3625), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3626), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3627), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3628), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3629), 0 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 16, 6, 47, 573, DateTimeKind.Utc).AddTicks(3630), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(74), 35 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(75), 20 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(76), 40 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(77), 15 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(78), 28 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(79), 22 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(80), 30 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(81), 18 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(82), 22 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(83), 10 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(84), 25 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(85), 14 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(86), 23 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(87), 19 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "UpvoteCount" },
                values: new object[] { new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(88), 16 });
        }
    }
}
