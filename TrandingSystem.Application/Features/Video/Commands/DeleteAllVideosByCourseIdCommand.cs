using MediatR;
 
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Video.Commands
{
    public class DeleteAllVideosByCourseIdCommand:IRequest<Response<bool>>
    {
        public int CourseId { get; set; }
        public string Culture { get; set; }
        public DeleteAllVideosByCourseIdCommand(int courseId, string language)
        {
            CourseId = courseId;
            Culture = language;
        }
    }
}
