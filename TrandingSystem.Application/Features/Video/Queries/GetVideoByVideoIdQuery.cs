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
    public class GetVideoByVideoIdQuery:IRequest<Response<ViedoUpdateDto>>
    {
        public int VideoId { get; set; }
        public string Culture { get; set; }
        public GetVideoByVideoIdQuery(int videoId, string culture)
        {
            VideoId = videoId;
            Culture = culture;
        }
    }
}
