using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Video.Commands;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class DeleteVideoByIdHandler : IRequestHandler<DeleteVideoByIdCommand, Response<bool>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteVideoByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(DeleteVideoByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.Videos.Delete(request.VideoId);
                 _unitOfWork.SaveChangesAsync();
                string responseMessage = request.Culture == "ar"
                ? "تم حذف الفيديو بنجاح"
                : "Video deleted successfully";
                return   Response<bool>.SuccessResponse(true, responseMessage);
            }
            catch(Exception e)
            {
                string errorMsg = request.Culture == "ar"
                  ? "حدث خطأ أثناء حذف الفيديو"
                  : "An error occurred while deleting the video";
                return Response<bool>.ErrorResponse(
                    errorMsg,
                    status: System.Net.HttpStatusCode.InternalServerError
                );

            }

        }
    }
}
