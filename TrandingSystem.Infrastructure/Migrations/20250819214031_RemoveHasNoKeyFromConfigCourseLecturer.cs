using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHasNoKeyFromConfigCourseLecturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseLecturers",
                table: "CourseLecturers",
                column: "CourseLecturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseLecturers",
                table: "CourseLecturers");
        }
    }
}
