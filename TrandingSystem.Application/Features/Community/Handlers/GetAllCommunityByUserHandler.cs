using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Community.Queries;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

public class GetAllCommunityByUserHandler
    : IRequestHandler<GetAllCommunityByUserQuery, Response<List<CommunitiesDto>>>
{
    private readonly IUnitOfWork _unitofwork;
    private readonly IStringLocalizer<ValidationMessages> _localizer;
    private readonly ILogger<GetAllCommunityByUserHandler> _logger;

    public GetAllCommunityByUserHandler(
        IUnitOfWork unitofwork,
        IStringLocalizer<ValidationMessages> localizer,
        ILogger<GetAllCommunityByUserHandler> logger)
    {
        _unitofwork = unitofwork;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Response<List<CommunitiesDto>>> Handle(GetAllCommunityByUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var communities =   _unitofwork.Communities
                .GetAllCommunitiesByUserId(request.UserId)
                .ToList();

            var communitiesDto = communities.Select(x =>
            {
                var lastMessage = x.Messages?
                    .OrderByDescending(m => m.SentAt)
                    .FirstOrDefault();

                return new CommunitiesDto
                {
                    CommunityId = x.CommunityId,
                    Title = x.Title ?? "",
                    IsDefault = x.IsDefault,
                    CountSubucribtor = x.CommunityMembers?.Count() ?? 0,
                    CourseTitle = x.Course?.TitleEN ?? "UN",
                    CourseId = x.CourseId ?? 0,
                    IsClosed = x.IsClosed,
                    IsAdminOnly = x.IsAdminOnly,
                    CreatedAt = x.CreatedAt,
                    Sender = lastMessage?.User?.FullName,
                    LastMessage = lastMessage?.MessageText,
                    LastMessageTime = lastMessage?.SentAt,
                    
                    UserIsBlock = x.CommunityMembers.Where(x => x.UserId == request.UserId).FirstOrDefault().IsBlocked??false,

                };
            }).ToList();

            return Response<List<CommunitiesDto>>.SuccessResponse(
                communitiesDto,
                "Done");
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error in GetAllCommunityByUserHandler");
            return Response<List<CommunitiesDto>>.ErrorResponse(_localizer["GeneralError"]);
        }
    }
}
