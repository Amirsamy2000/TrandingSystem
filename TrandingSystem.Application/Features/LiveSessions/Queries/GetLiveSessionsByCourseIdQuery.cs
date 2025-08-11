using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.LiveSessions.Queries
{
    public class GetLiveSessionsByCourseIdQuery:IRequest<Response<IEnumerable<LiveSessionsDto>>>
    {
          public int CourseId { get; set; }
        public string Culture { get; set; } = "ar";
        public GetLiveSessionsByCourseIdQuery(int courseId, string culture = "ar")
        {
            CourseId = courseId;
            Culture = culture;
        }
    }
}
