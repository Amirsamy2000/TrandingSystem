using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrandingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeFKtoCascadeinalltables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Categorie__Creat__607251E5",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK__Communiti__Cours__6166761E",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK__Community__Commu__625A9A57",
                table: "CommunityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK__Community__UserI__634EBE90",
                table: "CommunityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK__ContactIn__Cread__6442E2C9",
                table: "ContactInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseLec__Cours__671F4F74",
                table: "CourseLecturers");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseLec__Lectu__681373AD",
                table: "CourseLecturers");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseRat__Cours__690797E6",
                table: "CourseRatings");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseRat__UserI__69FBBC1F",
                table: "CourseRatings");

            migrationBuilder.DropForeignKey(
                name: "FK__Courses__Categor__6AEFE058",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK__Courses__CreateB__6BE40491",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK__LandingPa__Cread__6CD828CA",
                table: "LandingPageContent");

            migrationBuilder.DropForeignKey(
                name: "FK__LiveSessi__Cours__6DCC4D03",
                table: "LiveSessions");

            migrationBuilder.DropForeignKey(
                name: "FK__LiveSessi__Cread__6EC0713C",
                table: "LiveSessions");

            migrationBuilder.DropForeignKey(
                name: "FK__Messages__Commun__6FB49575",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK__Messages__UserId__70A8B9AE",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK__Notificat__UserI__719CDDE7",
                table: "NotificationsQueue");

            migrationBuilder.DropForeignKey(
                name: "FK__UsersConn__UserI__756D6ECB",
                table: "UsersConnections");

            migrationBuilder.DropForeignKey(
                name: "FK__UserSessi__UserI__76619304",
                table: "UserSessions");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseEnr__Cours__65370702",
                table: "VideoCourseEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseEnr__UserI__662B2B3B",
                table: "VideoCourseEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK__Videos__CourseId__7755B73D",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK__Videos__CreadteB__7849DB76",
                table: "Videos");

            migrationBuilder.AddForeignKey(
                name: "FK__Categorie__Creat__607251E5",
                table: "Categories",
                column: "CreateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Communiti__Cours__6166761E",
                table: "Communities",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Community__Commu__625A9A57",
                table: "CommunityMembers",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "CommunityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Community__UserI__634EBE90",
                table: "CommunityMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__ContactIn__Cread__6442E2C9",
                table: "ContactInfo",
                column: "CreadteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__CourseLec__Cours__671F4F74",
                table: "CourseLecturers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__CourseLec__Lectu__681373AD",
                table: "CourseLecturers",
                column: "LecturerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__CourseRat__Cours__690797E6",
                table: "CourseRatings",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__CourseRat__UserI__69FBBC1F",
                table: "CourseRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Courses__Categor__6AEFE058",
                table: "Courses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Courses__CreateB__6BE40491",
                table: "Courses",
                column: "CreateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__LandingPa__Cread__6CD828CA",
                table: "LandingPageContent",
                column: "CreadteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__LiveSessi__Cours__6DCC4D03",
                table: "LiveSessions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__LiveSessi__Cread__6EC0713C",
                table: "LiveSessions",
                column: "CreadteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Messages__Commun__6FB49575",
                table: "Messages",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "CommunityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Messages__UserId__70A8B9AE",
                table: "Messages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Notificat__UserI__719CDDE7",
                table: "NotificationsQueue",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__UsersConn__UserI__756D6ECB",
                table: "UsersConnections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__UserSessi__UserI__76619304",
                table: "UserSessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__CourseEnr__Cours__65370702",
                table: "VideoCourseEnrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__CourseEnr__UserI__662B2B3B",
                table: "VideoCourseEnrollments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Videos__CourseId__7755B73D",
                table: "Videos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Videos__CreadteB__7849DB76",
                table: "Videos",
                column: "CreadteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Categorie__Creat__607251E5",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK__Communiti__Cours__6166761E",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK__Community__Commu__625A9A57",
                table: "CommunityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK__Community__UserI__634EBE90",
                table: "CommunityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK__ContactIn__Cread__6442E2C9",
                table: "ContactInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseLec__Cours__671F4F74",
                table: "CourseLecturers");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseLec__Lectu__681373AD",
                table: "CourseLecturers");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseRat__Cours__690797E6",
                table: "CourseRatings");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseRat__UserI__69FBBC1F",
                table: "CourseRatings");

            migrationBuilder.DropForeignKey(
                name: "FK__Courses__Categor__6AEFE058",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK__Courses__CreateB__6BE40491",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK__LandingPa__Cread__6CD828CA",
                table: "LandingPageContent");

            migrationBuilder.DropForeignKey(
                name: "FK__LiveSessi__Cours__6DCC4D03",
                table: "LiveSessions");

            migrationBuilder.DropForeignKey(
                name: "FK__LiveSessi__Cread__6EC0713C",
                table: "LiveSessions");

            migrationBuilder.DropForeignKey(
                name: "FK__Messages__Commun__6FB49575",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK__Messages__UserId__70A8B9AE",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK__Notificat__UserI__719CDDE7",
                table: "NotificationsQueue");

            migrationBuilder.DropForeignKey(
                name: "FK__UsersConn__UserI__756D6ECB",
                table: "UsersConnections");

            migrationBuilder.DropForeignKey(
                name: "FK__UserSessi__UserI__76619304",
                table: "UserSessions");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseEnr__Cours__65370702",
                table: "VideoCourseEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK__CourseEnr__UserI__662B2B3B",
                table: "VideoCourseEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK__Videos__CourseId__7755B73D",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK__Videos__CreadteB__7849DB76",
                table: "Videos");

            migrationBuilder.AddForeignKey(
                name: "FK__Categorie__Creat__607251E5",
                table: "Categories",
                column: "CreateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Communiti__Cours__6166761E",
                table: "Communities",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK__Community__Commu__625A9A57",
                table: "CommunityMembers",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK__Community__UserI__634EBE90",
                table: "CommunityMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__ContactIn__Cread__6442E2C9",
                table: "ContactInfo",
                column: "CreadteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__CourseLec__Cours__671F4F74",
                table: "CourseLecturers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK__CourseLec__Lectu__681373AD",
                table: "CourseLecturers",
                column: "LecturerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__CourseRat__Cours__690797E6",
                table: "CourseRatings",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK__CourseRat__UserI__69FBBC1F",
                table: "CourseRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Courses__Categor__6AEFE058",
                table: "Courses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK__Courses__CreateB__6BE40491",
                table: "Courses",
                column: "CreateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__LandingPa__Cread__6CD828CA",
                table: "LandingPageContent",
                column: "CreadteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__LiveSessi__Cours__6DCC4D03",
                table: "LiveSessions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK__LiveSessi__Cread__6EC0713C",
                table: "LiveSessions",
                column: "CreadteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Messages__Commun__6FB49575",
                table: "Messages",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK__Messages__UserId__70A8B9AE",
                table: "Messages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Notificat__UserI__719CDDE7",
                table: "NotificationsQueue",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__UsersConn__UserI__756D6ECB",
                table: "UsersConnections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__UserSessi__UserI__76619304",
                table: "UserSessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__CourseEnr__Cours__65370702",
                table: "VideoCourseEnrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK__CourseEnr__UserI__662B2B3B",
                table: "VideoCourseEnrollments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Videos__CourseId__7755B73D",
                table: "Videos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK__Videos__CreadteB__7849DB76",
                table: "Videos",
                column: "CreadteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
