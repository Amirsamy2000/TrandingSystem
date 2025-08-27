using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameCourseEnrollment_MissingSolve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollments_Videos_VideoId",
                table: "CourseEnrollments");

            migrationBuilder.RenameTable(
                name: "CourseEnrollments",
                newName: "VideoCourseEnrollments");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEnrollments_VideoId",
                table: "VideoCourseEnrollments",
                newName: "IX_VideoCourseEnrollments_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEnrollments_UserId",
                table: "VideoCourseEnrollments",
                newName: "IX_VideoCourseEnrollments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEnrollments_CourseId_UserId_VideoId_OrderStatus",
                table: "VideoCourseEnrollments",
                newName: "IX_VideoCourseEnrollments_CourseId_UserId_VideoId_OrderStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoCourseEnrollments_Videos_VideoId",
                table: "VideoCourseEnrollments",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "VideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoCourseEnrollments_Videos_VideoId",
                table: "VideoCourseEnrollments");

            migrationBuilder.RenameTable(
                name: "VideoCourseEnrollments",
                newName: "CourseEnrollments");

            migrationBuilder.RenameIndex(
                name: "IX_VideoCourseEnrollments_VideoId",
                table: "CourseEnrollments",
                newName: "IX_CourseEnrollments_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_VideoCourseEnrollments_UserId",
                table: "CourseEnrollments",
                newName: "IX_CourseEnrollments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_VideoCourseEnrollments_CourseId_UserId_VideoId_OrderStatus",
                table: "CourseEnrollments",
                newName: "IX_CourseEnrollments_CourseId_UserId_VideoId_OrderStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollments_Videos_VideoId",
                table: "CourseEnrollments",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "VideoId");
        }
    }
}
