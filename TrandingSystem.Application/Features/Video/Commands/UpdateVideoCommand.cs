using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Video.Commands
{
    public class UpdateVideoCommand:IRequest<Response<bool>>
    {
        public ViedoUpdateDto viedoUpdateDto { get; set; }
        public string Culture { set; get; }
        public UpdateVideoCommand(ViedoUpdateDto dto, string culture)
        {
            viedoUpdateDto = dto;
            Culture = culture;
        }
    }
}
