using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflowLite.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterAnswerTableColumnQuestionPostedTOAnswerPosted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionPosted",
                table: "Answers",
                newName: "AnswerPosted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnswerPosted",
                table: "Answers",
                newName: "QuestionPosted");
        }
    }
}
