using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeVideoIdReferenceKey_MissingSolve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_VideoId",
                table: "CourseEnrollments",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollments_Videos_VideoId",
                table: "CourseEnrollments",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "VideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollments_Videos_VideoId",
                table: "CourseEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_CourseEnrollments_VideoId",
                table: "CourseEnrollments");
        }
    }
}
