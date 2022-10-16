using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BottomTextLMS.Migrations
{
    public partial class addedSubmitTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Submissions");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitTime",
                table: "Submissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmitTime",
                table: "Submissions");

            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "Submissions",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }
    }
}
