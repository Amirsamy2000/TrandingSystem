using Amazon.Runtime.Internal;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Video.Commands
{
    public class EnrollmentInPaidVideoCommand:IRequest<Response<bool>>
    {
        public int VideoId { get; set; }
        public IFormFile RecieptImage { get; set; }
        public int UserId { get; set; }

        public EnrollmentInPaidVideoCommand(int videoId, IFormFile recieptImage, int userId)
        {
            VideoId = videoId;
            RecieptImage = recieptImage;
            UserId = userId;
        }
    }
}
