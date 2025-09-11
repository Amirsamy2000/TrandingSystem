using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newcolliveidinVideo_CourseEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "liveId",
                table: "VideoCourseEnrollments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoCourseEnrollments_liveId",
                table: "VideoCourseEnrollments",
                column: "liveId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoCourseEnrollments_LiveSessions_liveId",
                table: "VideoCourseEnrollments",
                column: "liveId",
                principalTable: "LiveSessions",
                principalColumn: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoCourseEnrollments_LiveSessions_liveId",
                table: "VideoCourseEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_VideoCourseEnrollments_liveId",
                table: "VideoCourseEnrollments");

            migrationBuilder.DropColumn(
                name: "liveId",
                table: "VideoCourseEnrollments");
        }
    }
}
