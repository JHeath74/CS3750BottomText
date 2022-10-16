using Microsoft.EntityFrameworkCore.Migrations;

namespace BottomTextLMS.Migrations
{
    public partial class TestInstructorID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorID",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_InstructorID",
                table: "Classes",
                column: "InstructorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Users_InstructorID",
                table: "Classes",
                column: "InstructorID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Users_InstructorID",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_InstructorID",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "InstructorID",
                table: "Classes");
        }
    }
}
