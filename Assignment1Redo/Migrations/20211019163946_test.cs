using Microsoft.EntityFrameworkCore.Migrations;

namespace BottomTextLMS.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    AssignmentID = table.Column<int>(type: "int", nullable: false),
                    HasSubmitted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Submissions_Assignments_AssignmentID",
                        column: x => x.AssignmentID,
                        principalTable: "Assignments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ClassID",
                table: "Assignments",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_AssignmentID",
                table: "Submissions",
                column: "AssignmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Classes_ClassID",
                table: "Assignments",
                column: "ClassID",
                principalTable: "Classes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Classes_ClassID",
                table: "Assignments");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_ClassID",
                table: "Assignments");

            migrationBuilder.AddColumn<bool>(
                name: "HasSubmitted",
                table: "Assignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_StudentID_ClassID",
                table: "Assignments",
                columns: new[] { "StudentID", "ClassID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Enrollments_StudentID_ClassID",
                table: "Assignments",
                columns: new[] { "StudentID", "ClassID" },
                principalTable: "Enrollments",
                principalColumns: new[] { "StudentID", "ClassID" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
