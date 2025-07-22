using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsNCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryNameEN",
                table: "Categories",
                newName: "CategoryNameEn");

            migrationBuilder.RenameColumn(
                name: "CategoryNameAR",
                table: "Categories",
                newName: "CategoryNameAr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryNameEn",
                table: "Categories",
                newName: "CategoryNameEN");

            migrationBuilder.RenameColumn(
                name: "CategoryNameAr",
                table: "Categories",
                newName: "CategoryNameAR");
        }
    }
}
