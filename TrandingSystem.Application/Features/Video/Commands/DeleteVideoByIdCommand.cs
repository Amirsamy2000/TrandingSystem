
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Video.Commands
{
    public class DeleteVideoByIdCommand:IRequest<Response<bool>>
    {
        public int VideoId { get; set; }
        public string Culture { get; set; } = "ar"; // Default to English
        public DeleteVideoByIdCommand(int id, string lang)
        {
            VideoId = id;
            Culture = lang;

        }
    }
}
