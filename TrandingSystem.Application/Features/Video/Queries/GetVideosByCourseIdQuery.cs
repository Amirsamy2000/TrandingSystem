using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Video.Queries
{
    public class GetVideosByCourseIdQuery:IRequest<Response<IEnumerable<VideoDto>>>
    {
        public int CourseId { get; set; }
        public string Culture { get; set; } = "ar";
        public GetVideosByCourseIdQuery(int courseId, string culture = "ar")
        {
            CourseId = courseId;
            Culture = culture;
        }


    }
}
