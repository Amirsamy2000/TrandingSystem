 
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
    public class DeleteAllVideosByCourseIdHandler : IRequestHandler<DeleteAllVideosByCourseIdCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAllVideosByCourseIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(DeleteAllVideosByCourseIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var AllVideos = _unitOfWork.Videos.GetAllVideosForCouse(request.CourseId);
                if (AllVideos == null || !AllVideos.Any())
                {
                    var notFoundMsg = request.Culture == "ar" ? "لا توجد فيديوهات لحذفها" : "No videos to delete";
                    return Response<bool>.ErrorResponse(notFoundMsg, status: System.Net.HttpStatusCode.NotFound);

                }
                _unitOfWork.Videos.DeleteAllVideosByCourseId(AllVideos.ToList());
                 await _unitOfWork.SaveChangesAsync();
                string responseMessage = request.Culture == "ar" ? "تم حذف الفيديوهات بنجاح" : "Videos deleted successfully";
                return Response<bool>.SuccessResponse(true, responseMessage);

            }
            catch 
            {
                return new Response<bool>
                {
                    Data = false,
                    Message = request.Culture == "ar" ? "حدث خطأ أثناء حذف الفيديوهات" : "An error occurred while deleting videos",
                    Status = HttpStatusCode.InternalServerError,
                    Success = false
                };
}
        }
    }
}
