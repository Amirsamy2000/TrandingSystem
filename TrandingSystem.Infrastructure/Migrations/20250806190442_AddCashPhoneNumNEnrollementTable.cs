using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCashPhoneNumNEnrollementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CashPhoneNum",
                table: "CourseEnrollments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashPhoneNum",
                table: "CourseEnrollments");
        }
    }
}
