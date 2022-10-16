using Microsoft.EntityFrameworkCore.Migrations;

namespace BottomTextLMS.Migrations
{
    public partial class TestingAssignmentChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Enrollments_EnrollmentStudentID_EnrollmentClassID",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_EnrollmentStudentID_EnrollmentClassID",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "EnrollmentClassID",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "EnrollmentStudentID",
                table: "Assignments");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "ID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Enrollments_StudentID_ClassID",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_StudentID_ClassID",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Assignments");

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentClassID",
                table: "Assignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentStudentID",
                table: "Assignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                columns: new[] { "StudentID", "ClassID" });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_EnrollmentStudentID_EnrollmentClassID",
                table: "Assignments",
                columns: new[] { "EnrollmentStudentID", "EnrollmentClassID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Enrollments_EnrollmentStudentID_EnrollmentClassID",
                table: "Assignments",
                columns: new[] { "EnrollmentStudentID", "EnrollmentClassID" },
                principalTable: "Enrollments",
                principalColumns: new[] { "StudentID", "ClassID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
