using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Community.Queries;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;


namespace TrandingSystem.Application.Features.Community.Handlers
{
    public class GetAllCommunityByCourseHandler : IRequestHandler<GellAllCommunityByCourseQuery, Response<IEnumerable<CommunitiesDto>>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        //private readonly IFileService _imageService;
        private readonly IMapper _mapper;

        public GetAllCommunityByCourseHandler(IUnitOfWork unitOfWork, IStringLocalizer<ValidationMessages> localizer, IFileService imageService, IMapper mapper)
        {
            _unitofwork = unitOfWork;
            _localizer = localizer;
            //_imageService = imageService;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<CommunitiesDto>>> Handle(GellAllCommunityByCourseQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Communities=_unitofwork.Communities.GetAllCommunitiesByCourseId(request.CourseId).ToList();
                IEnumerable<CommunitiesDto> CommunitiesDto = Communities.Select(x=>new CommunitiesDto
                {
                    CommunityId = x.CommunityId,
                    Title = x.Title??"",
                    IsDefault = x.IsDefault,
                    CountSubucribtor = x.CommunityMembers?.Count() ?? 0,
                    CourseTitle = x.Course?.TitleEN ?? "UN",
                    CourseId = x.CourseId??0,
                    IsClosed = x.IsClosed,
                    IsAdminOnly = x.IsAdminOnly,
                    CreatedAt = x.CreatedAt
                });
                //var MappCommunities = _mapper.Map<IEnumerable<CommunitiesDto>>(Communities);
                var r = CommunitiesDto.ToList();
                return Response<IEnumerable<CommunitiesDto>>.SuccessResponse(
                    CommunitiesDto
                  );

            }
            catch (Exception ex)
            {
                return Response<IEnumerable<CommunitiesDto>>.ErrorResponse(
                    _localizer["GeneralError"]
                  );
            }
            throw new NotImplementedException();
        }
    }
}
