using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Video.Commands;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class BlockVideoByIdHandler : IRequestHandler<BlockVideoByIdCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlockVideoByIdHandler(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<bool>> Handle(BlockVideoByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var video = _unitOfWork.Videos.ReadById(request.VideoId);
                if (video == null)
                {
                    string NotFountMessage = request.Culuter == "ar"
                     ? "عذرًا، هذا الفيديو غير موجود أو قد تم حذفه بالفعل."
                     : "Sorry, this video does not exist or may have already been deleted.";
                    return Response<bool>.ErrorResponse(
                        NotFountMessage,
                        status: HttpStatusCode.NotFound
                    );
                }
                video.IsActive = request.Status;
                _unitOfWork.Videos.Update(video);
                await _unitOfWork.SaveChangesAsync();
                string responseMessage = request.Culuter == "ar"
               ? "تم  العملية بنجاح"
               : "operation successfully";
                return Response<bool>.SuccessResponse(true, responseMessage);

            }
            catch
            {
                string errorMsg = request.Culuter == "ar"
                               ? "حدث خطأ أثناء تحديث حالة الفيديو"
                               : "An error occurred while updating the video status";
                return Response<bool>.ErrorResponse(
                    errorMsg,
                    status: System.Net.HttpStatusCode.InternalServerError
                );

            }
        }
    }
}
