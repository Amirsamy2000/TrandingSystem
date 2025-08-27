using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameCourseEnrollmentAndAddVideoIdAndMakeCourseIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseEnrollments_CourseId",
                table: "CourseEnrollments");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseEnrollments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "CourseEnrollments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_CourseId_UserId_VideoId_OrderStatus",
                table: "CourseEnrollments",
                columns: new[] { "CourseId", "UserId", "VideoId", "OrderStatus" },
                unique: true,
                filter: "[CourseId] IS NOT NULL AND [VideoId] IS NOT NULL AND [OrderStatus] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseEnrollments_CourseId_UserId_VideoId_OrderStatus",
                table: "CourseEnrollments");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "CourseEnrollments");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseEnrollments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_CourseId",
                table: "CourseEnrollments",
                column: "CourseId");
        }
    }
}
