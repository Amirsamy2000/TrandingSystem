
using MediatR;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Dashboard.Queries
{
    public class GetUserDataKycQuery:IRequest<List<KycUserDto>>
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int Status { get; set; }

        public GetUserDataKycQuery(int userId, int courseId, int status)
        {
            UserId = userId;
            CourseId = courseId;
            Status = status;
        }
    }
}
