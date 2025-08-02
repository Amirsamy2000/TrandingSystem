  
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Video.Commands
{
    public class BlockVideoByIdCommand: IRequest<Response<bool>>
    {
        public int VideoId { get; set; }
        public string Culuter { get; set; }
        public bool Status { get; set; }
        public BlockVideoByIdCommand(int videoId, string culuter, bool status)
        {
            VideoId = videoId;
            Culuter = culuter;
            Status = status;
        }
    }
}
