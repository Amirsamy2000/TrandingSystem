using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeindexintableVideo_CourseEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VideoCourseEnrollments_CourseId_UserId_VideoId_OrderStatus",
                table: "VideoCourseEnrollments");

            migrationBuilder.CreateIndex(
                name: "IX_VideoCourseEnrollments_CourseId",
                table: "VideoCourseEnrollments",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VideoCourseEnrollments_CourseId",
                table: "VideoCourseEnrollments");

            migrationBuilder.CreateIndex(
                name: "IX_VideoCourseEnrollments_CourseId_UserId_VideoId_OrderStatus",
                table: "VideoCourseEnrollments",
                columns: new[] { "CourseId", "UserId", "VideoId", "OrderStatus" },
                unique: true,
                filter: "[CourseId] IS NOT NULL AND [VideoId] IS NOT NULL AND [OrderStatus] IS NOT NULL");
        }
    }
}
