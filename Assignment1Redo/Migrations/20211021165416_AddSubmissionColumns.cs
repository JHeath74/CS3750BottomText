using Microsoft.EntityFrameworkCore.Migrations;

namespace BottomTextLMS.Migrations
{
    public partial class AddSubmissionColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileSubmission",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextSubmission",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileSubmission",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "TextSubmission",
                table: "Submissions");
        }
    }
}
