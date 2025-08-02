using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AutoGenerateOfCourseLecturerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseLecturerId",
                table: "CourseLecturers");

            migrationBuilder.AddColumn<int>(
                name: "CourseLecturerId",
                table: "CourseLecturers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseLecturers",
                table: "CourseLecturers",
                column: "CourseLecturerId");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Remove the identity column
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseLecturers",
                table: "CourseLecturers");

            migrationBuilder.DropColumn(
                name: "CourseLecturerId",
                table: "CourseLecturers");

            // 2. Re-add the column without identity (if needed)
            migrationBuilder.AddColumn<int>(
                name: "CourseLecturerId",
                table: "CourseLecturers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Optional: Re-add primary key if needed
            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseLecturers",
                table: "CourseLecturers",
                column: "CourseLecturerId");
        }

    }
}
