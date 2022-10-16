using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BottomTextLMS.Migrations
{
    public partial class AddAssignmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxPoints = table.Column<int>(type: "int", nullable: false),
                    AssignmentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasSubmitted = table.Column<bool>(type: "bit", nullable: false),
                    EnrollmentStudentID = table.Column<int>(type: "int", nullable: true),
                    EnrollmentClassID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => new { x.StudentID, x.ClassID });
                    table.ForeignKey(
                        name: "FK_Assignment_Enrollments_EnrollmentStudentID_EnrollmentClassID",
                        columns: x => new { x.EnrollmentStudentID, x.EnrollmentClassID },
                        principalTable: "Enrollments",
                        principalColumns: new[] { "StudentID", "ClassID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_EnrollmentStudentID_EnrollmentClassID",
                table: "Assignment",
                columns: new[] { "EnrollmentStudentID", "EnrollmentClassID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignment");
        }
    }
}
