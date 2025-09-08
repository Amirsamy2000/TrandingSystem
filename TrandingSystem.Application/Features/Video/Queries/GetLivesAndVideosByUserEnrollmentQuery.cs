using Amazon.Runtime.Internal;
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
    public class GetLivesAndVideosByUserEnrollmentQuery:IRequest<Response<LivesAndVideosDto>>
    {
        public int UserId { get; set; }
        public int CourseId { set; get; }
        public string Culture { get; set; }

        public GetLivesAndVideosByUserEnrollmentQuery(int userId, string culture,int courseid)
        {
            UserId = userId;
            Culture = culture;
            CourseId = courseid;
        }


    }
}
