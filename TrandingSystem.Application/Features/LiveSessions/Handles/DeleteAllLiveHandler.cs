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
    public class DeleteAllLiveHandler:IRequestHandler<DeleteAllLivesCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileservice;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public DeleteAllLiveHandler(IUnitOfWork unitOfWork, IFileService fileservice,IStringLocalizer<ValidationMessages>localizer)
        {
            _unitOfWork = unitOfWork;
            _fileservice = fileservice;
            _localizer = localizer;
        }

        public async Task<Response<bool>> Handle(DeleteAllLivesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var livesOfCourse = _unitOfWork.LiveSessionRepositry.GetAllLiveSessionsForCouse(request.CourselId,0);
                if (livesOfCourse == null)
                {
                    return Response<bool>.ErrorResponse(_localizer["Notfoundlives"]);
                }
                foreach (var live in livesOfCourse)
                {
                    await _fileservice.DeleteImageAsync(live.ImageSessionUrl);

                }
                _unitOfWork.LiveSessionRepositry.DeleteAllSessionsByCourseId(livesOfCourse.ToList());
                await _unitOfWork.SaveChangesAsync();

                return Response<bool>.SuccessResponse(true, _localizer["GeneralOperationDone"]);
            }
            catch
            {
                return Response<bool>.ErrorResponse( _localizer["GeneralError"]);

            }


        }
    }
}
