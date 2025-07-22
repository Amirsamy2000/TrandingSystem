using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createnewaracol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "CategoryNameEN");

            migrationBuilder.AddColumn<string>(
                name: "CategoryNameAR",
                table: "Categories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryNameAR",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryNameEN",
                table: "Categories",
                newName: "CategoryName");
        }
    }
}
