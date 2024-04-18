using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StackOverflowLite.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedingApplyInQuestionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Content", "Downvote", "QuestionPosted", "Tags", "Title", "Upvote", "UserId" },
                values: new object[,]
                {
                    { new Guid("83e3f4b8-cfc7-4fde-82e5-f86a80318ba8"), "Test Content-2", 0, new DateTime(2024, 4, 18, 23, 46, 9, 814, DateTimeKind.Local).AddTicks(8742), "Test2", "Test Question-2", 0, new Guid("0580a986-7dbe-483a-6b67-08dc56e3bd5b") },
                    { new Guid("d788a40b-dc00-4869-856c-cf1a22156501"), "Test Content-1", 0, new DateTime(2024, 4, 18, 23, 46, 9, 814, DateTimeKind.Local).AddTicks(8689), "Test1", "Test Question-1", 0, new Guid("0580a986-7dbe-483a-6b67-08dc56e3bd5b") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("83e3f4b8-cfc7-4fde-82e5-f86a80318ba8"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("d788a40b-dc00-4869-856c-cf1a22156501"));
        }
    }
}
