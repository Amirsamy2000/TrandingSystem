using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Community.Queries
{
    public class GellAllCommunityByCourseQuery:IRequest<Response<IEnumerable<CommunitiesDto>>>
    {
        public int CourseId { get; set; }

        // courseId=0 means all communities
        // courseId>0 means communities by courseId
        // courseId=-1 means default communities
        public GellAllCommunityByCourseQuery(int courseId)
        {
            CourseId = courseId;
        }

    }
}
