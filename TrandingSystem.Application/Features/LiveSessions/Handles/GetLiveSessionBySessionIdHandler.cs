using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.LiveSessions.Queries;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.LiveSessions.Handles
{
    public class GetLiveSessionBySessionIdHandler : IRequestHandler<GetLiveSessionBySessionIdQuery, Response<LiveSessionAddDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public GetLiveSessionBySessionIdHandler(IUnitOfWork unitOfWork, IStringLocalizer<ValidationMessages> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Response<LiveSessionAddDto>> Handle(GetLiveSessionBySessionIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var liveSession = _unitOfWork.LiveSessionRepositry.ReadById(request.SessionId);
                if (liveSession == null)
                {
                    return Response<LiveSessionAddDto>.ErrorResponse(_localizer["notFoundMessage"]);
                }
                var liveSessionDto = new LiveSessionAddDto
                {
                    SessionId = liveSession.SessionId,
                    TitleAR = liveSession.TitleAR,
                    TitleEN = liveSession.TitleEN,
                    DescriptionAR = liveSession.DescriptionAR,
                    DescriptionEN = liveSession.DescriptionEN,
                    ScheduledTime = liveSession.ScheduledTime,
                    ScheduledAt = liveSession.ScheduledAt,
                    CourseId = liveSession.CourseId,
                    IsActive = liveSession.IsActive ?? false,
                    CreatedAt = liveSession.CreatedAt ?? DateTime.Now,
                    IsLocked = liveSession.IsLocked ?? false,
                    Cost=liveSession.Cost,
                    YoutubeLink=liveSession.YoutubeLink
                };
                return Response<LiveSessionAddDto>.SuccessResponse(liveSessionDto);
            }
            catch
            {
                return Response<LiveSessionAddDto>.ErrorResponse(_localizer["GeneralServerFail"]);
                    
                   
            }
        }
    }
}
