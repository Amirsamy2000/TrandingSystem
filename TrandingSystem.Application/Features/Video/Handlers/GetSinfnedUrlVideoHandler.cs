using Amazon.S3.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Video.Queries;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class GetSinfnedUrlVideoHandler : IRequestHandler<GetSingnedUrlVideoQuery, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IFileService _fileService;
        private IStringLocalizer<ValidationMessages> _localizer;
        public GetSinfnedUrlVideoHandler(IUnitOfWork unitOfWork,IFileService fileService,IStringLocalizer<ValidationMessages> localizer)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(GetSingnedUrlVideoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var video = _unitOfWork.Videos.ReadById(request.VideoId);
                if(video is null)
                {
                    return Response<string>.ErrorResponse(
                        _localizer["NotFoundVideo"]
                       
                    );
                }
                var url =await _fileService.GetVideoUrlAsync(video.VideoUrl);
                return Response<string>.SuccessResponse(url);
            }
            catch(Exception e)
            {
                return Response<string>.ErrorResponse(
                    e.Message.ToString()

                                   );
            }
        }
    }
}
