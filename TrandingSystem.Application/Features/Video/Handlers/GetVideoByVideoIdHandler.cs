using AutoMapper;
using MediatR;

using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Video.Queries;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class GetVideoByVideoIdHandler : IRequestHandler<GetVideoByVideoIdQuery, Response<ViedoUpdateDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideoByVideoIdHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<ViedoUpdateDto>> Handle(GetVideoByVideoIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var video= _unitOfWork.Videos.ReadById(request.VideoId);
                if (video == null)
                {
                    string NotFountMessage = request.Culture == "ar"
                        ? "عذرًا، هذا الفيديو غير موجود أو قد تم حذفه بالفعل."
                        : "Sorry, this video does not exist or may have already been deleted.";
                    return Response<ViedoUpdateDto>.ErrorResponse(NotFountMessage);
                }
                var videoDto = _mapper.Map<ViedoUpdateDto>(video);

                return Response<ViedoUpdateDto>.SuccessResponse(videoDto);
            }
            catch
            {
                string errorMsg = request.Culture == "ar"
                    ? "حدث خطأ أثناء استرجاع الفيديو"
                    : "An error occurred while retrieving the video";
                return Response<ViedoUpdateDto>.ErrorResponse(errorMsg);

            }
        }
    }
}
