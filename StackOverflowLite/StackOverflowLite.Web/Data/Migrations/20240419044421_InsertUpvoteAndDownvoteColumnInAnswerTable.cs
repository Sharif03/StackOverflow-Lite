using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflowLite.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertUpvoteAndDownvoteColumnInAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Downvote",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Upvote",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("83e3f4b8-cfc7-4fde-82e5-f86a80318ba8"),
                column: "QuestionPosted",
                value: new DateTime(2024, 4, 19, 10, 44, 20, 214, DateTimeKind.Local).AddTicks(4491));

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("d788a40b-dc00-4869-856c-cf1a22156501"),
                column: "QuestionPosted",
                value: new DateTime(2024, 4, 19, 10, 44, 20, 214, DateTimeKind.Local).AddTicks(4403));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downvote",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Upvote",
                table: "Answers");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("83e3f4b8-cfc7-4fde-82e5-f86a80318ba8"),
                column: "QuestionPosted",
                value: new DateTime(2024, 4, 18, 23, 46, 9, 814, DateTimeKind.Local).AddTicks(8742));

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("d788a40b-dc00-4869-856c-cf1a22156501"),
                column: "QuestionPosted",
                value: new DateTime(2024, 4, 18, 23, 46, 9, 814, DateTimeKind.Local).AddTicks(8689));
        }
    }
}
