using MediatR;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Dashboard.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

public class GetUserDataKycHandlers : IRequestHandler<GetUserDataKycQuery, List<KycUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserDataKycHandlers(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<KycUserDto>> Handle(GetUserDataKycQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = _unitOfWork.Users.GetUser(request.UserId, request.Status);
            var count = users.Count();

            //// فلترة الكورس لو موجود
            //if (request.CourseId != 0)
            //{
            //    users = users.Where(x => x.CourseEnrollments.Any(e => e.CourseId == request.CourseId));
            //}


            if (!users.Any())
                return Response<List<KycUserDto>>.SuccessResponse(new List<KycUserDto>()).Data;

            var filterKycUsers = users.Select(x => new KycUserDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email ?? "UN",
                PhoneNumber = x.Mobile ?? "UN",
                NationalId = x.NationalId,
                IsBlocketed = x.IsBlocked,
                IsConfirmed = x.EmailConfirmed,
                JoinDate = x.RegisteredAt ?? DateTime.Now,

                // Orders
                // order type 0 = course ,1 = video ,2 = live
                Orders = x.CourseEnrollments.Select(e => new OrdersDto
                {
                    CreatedAt = e.CreatedAt,
                    CourseName = e.Course.TitleEN ?? "",
                    VideoName = e.Video.TitleEN ?? "",
                    LiveName = e.Live.TitleEN ?? "",
                    CostCourse = e.Course.Cost ,
                    LiveCost = e.Live.Cost ?? 0,
                    VideoCost = e.Video.Cost??0,
                    OrderStatus = e.OrderStatus,
                    OrderByType=e.liveId!=null?2:e.VideoId!=null?1:0


                }).ToList(),

                // Courses
                Courses = x.CourseEnrollments.Where(x=>x.OrderStatus==1).Select(c => new CoursesUserKycDto
                {
                    CourseName = c.Course.TitleEN ?? "",
                    CountLives = c.Course.LiveSessions.Count(),
                    CountVideos = c.Course.Videos.Count(),
                    CourseStatus = c. Course.IsActive ?? false,
                    CourseCost = c.Course.Cost,
                    CreateAt = c.Course.CreateAt ?? DateTime.Now
                }).ToList(),

                // Communities
                Communities = x.CommunityMembers.Select(cm => new CommunityUserKycDto
                {
                    CommunityName = cm.Community.Title ?? "",
                    JoinDate = cm.JoinedAt ?? DateTime.Now,
                    CommunityStatus = cm.Community.IsClosed ?? false,
                    UserStatus = cm.IsBlocked ?? false,
                    CourseName = cm.Community.Course.TitleEN ?? ""
                }).ToList(),

                // Counters
                CountAcceptedOrders = x.CourseEnrollments.Count(e => e.OrderStatus == 1),
                CountPandingOrders = x.CourseEnrollments.Count(e => e.OrderStatus == 2),
                CountRejectedOrders = x.CourseEnrollments.Count(e => e.OrderStatus == 0),

                // Total Paid
                TotalPaid = x.CourseEnrollments.Where(x=>x.OrderStatus==1).Sum(e =>
                    (e.Video.Cost ?? 0) +
                    (e.Course.Cost) +
                    (e.Live.Cost ?? 0))
            }).ToList();

            return Response<List<KycUserDto>>.SuccessResponse(filterKycUsers).Data;
        }
        catch
        {
            return Response<List<KycUserDto>>.ErrorResponse("Error In Server").Data;
        }
    }
}
