using Microsoft.EntityFrameworkCore.Migrations;

namespace BottomTextLMS.Migrations
{
    public partial class TestAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Enrollments_EnrollmentStudentID_EnrollmentClassID",
                table: "Assignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment");

            migrationBuilder.RenameTable(
                name: "Assignment",
                newName: "Assignments");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_EnrollmentStudentID_EnrollmentClassID",
                table: "Assignments",
                newName: "IX_Assignments_EnrollmentStudentID_EnrollmentClassID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                columns: new[] { "StudentID", "ClassID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Enrollments_EnrollmentStudentID_EnrollmentClassID",
                table: "Assignments",
                columns: new[] { "EnrollmentStudentID", "EnrollmentClassID" },
                principalTable: "Enrollments",
                principalColumns: new[] { "StudentID", "ClassID" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Enrollments_EnrollmentStudentID_EnrollmentClassID",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.RenameTable(
                name: "Assignments",
                newName: "Assignment");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_EnrollmentStudentID_EnrollmentClassID",
                table: "Assignment",
                newName: "IX_Assignment_EnrollmentStudentID_EnrollmentClassID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment",
                columns: new[] { "StudentID", "ClassID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Enrollments_EnrollmentStudentID_EnrollmentClassID",
                table: "Assignment",
                columns: new[] { "EnrollmentStudentID", "EnrollmentClassID" },
                principalTable: "Enrollments",
                principalColumns: new[] { "StudentID", "ClassID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
