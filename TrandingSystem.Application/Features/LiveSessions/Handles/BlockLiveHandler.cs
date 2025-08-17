using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.LiveSessions.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.LiveSessions.Handles
{
    public class BlockLiveHandler:IRequestHandler<BlockLiveCommand, Response<bool>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public BlockLiveHandler(IUnitOfWork unitOfWork, IFileService fileService, IStringLocalizer<ValidationMessages> localizer)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _localizer = localizer;
        }

        public async Task<Response<bool>> Handle(BlockLiveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the live session exists
                var live = _unitOfWork.LiveSessionRepositry.ReadById(request.SessionId);
                if (live == null)
                {
                    return Response<bool>.ErrorResponse(_localizer["notFoundMessage"]);
                }
                // Update the live session status
                live.IsActive = request.Status;
                _unitOfWork.LiveSessionRepositry.Update(live);
                await _unitOfWork.SaveChangesAsync();
                return Response<bool>.SuccessResponse(true, _localizer["GeneralOperationDone"]);

            }
            catch
            {
                return Response<bool>.ErrorResponse(_localizer["GeneralServerFail"], status: System.Net.HttpStatusCode.InternalServerError);

            }



        }
    }
}
