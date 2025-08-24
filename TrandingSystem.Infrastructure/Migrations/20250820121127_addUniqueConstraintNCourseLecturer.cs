using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUniqueConstraintNCourseLecturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseLecturers_CourseId",
                table: "CourseLecturers");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLecturers_CourseId_LecturerId",
                table: "CourseLecturers",
                columns: new[] { "CourseId", "LecturerId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseLecturers_CourseId_LecturerId",
                table: "CourseLecturers");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLecturers_CourseId",
                table: "CourseLecturers",
                column: "CourseId");
        }
    }
}
