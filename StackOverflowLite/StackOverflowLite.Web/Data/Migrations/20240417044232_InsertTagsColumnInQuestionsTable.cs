using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflowLite.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertTagsColumnInQuestionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Questions");
        }
    }
}
