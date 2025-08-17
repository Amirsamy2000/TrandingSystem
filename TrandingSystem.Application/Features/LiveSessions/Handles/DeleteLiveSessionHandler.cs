using Amazon.Runtime.Internal;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.LiveSessions.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.LiveSessions.Handles
{
    public class DeleteLiveSessionHandler : IRequestHandler<DeleteLiveSessionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public DeleteLiveSessionHandler(IUnitOfWork unitOfWork, IFileService fileService,IStringLocalizer<ValidationMessages>localizer)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _localizer = localizer;
        }
        public async Task<Response<bool>> Handle(DeleteLiveSessionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                // Check if the video exists
                var live = _unitOfWork.LiveSessionRepositry.ReadById(request.SessionId);
                if (live == null)
                {
                    return Response<bool>.ErrorResponse(_localizer["notFoundMessage"]);
                }
                // Delete the ;image
                await _fileService.DeleteImageAsync(live.ImageSessionUrl);
                //Delete Live From Database
                _unitOfWork.LiveSessionRepositry.Delete(live.SessionId);
                await _unitOfWork.SaveChangesAsync();
                return Response<bool>.SuccessResponse(true,_localizer["GeneralOperationDone"]);
            }
            catch
            {
                return Response<bool>.ErrorResponse(_localizer["GeneralServerFail"], status: System.Net.HttpStatusCode.InternalServerError);
            }






        }
    }
}
