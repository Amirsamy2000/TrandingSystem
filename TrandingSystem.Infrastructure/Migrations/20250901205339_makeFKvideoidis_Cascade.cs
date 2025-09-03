using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class makeFKvideoidis_Cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoCourseEnrollments_Videos_VideoId",
                table: "VideoCourseEnrollments");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoCourseEnrollments_Videos_VideoId",
                table: "VideoCourseEnrollments",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoCourseEnrollments_Videos_VideoId",
                table: "VideoCourseEnrollments");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoCourseEnrollments_Videos_VideoId",
                table: "VideoCourseEnrollments",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "VideoId");
        }
    }
}
