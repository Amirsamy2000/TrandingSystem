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
using TrandingSystem.Infrastructure.Services;

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class DeleteVideoByIdHandler : IRequestHandler<DeleteVideoByIdCommand, Response<bool>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
     
        public DeleteVideoByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<Response<bool>> Handle(DeleteVideoByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the video exists
                var video =  _unitOfWork.Videos.ReadById(request.VideoId);
                if (video == null)
                {
                    string errorMsg = request.Culture == "ar"
                        ? "الفيديو غير موجود"
                        : "Video not found";
                    return Response<bool>.ErrorResponse(
                        errorMsg,
                        status: System.Net.HttpStatusCode.NotFound
                    );
                }
               
                // Delete the video file from storage
                string videoUrl = video.VideoUrl;
                 var resultDelete= _fileService.DeleteVideoAsync(videoUrl);
                if (!resultDelete.Result)
                {
                    string errorMsg = request.Culture == "ar"
                 ? "حدث خطأ أثناء حذف الفيديو "
                 : "An error occurred while deleting the video";
                    return Response<bool>.ErrorResponse(
                        errorMsg,
                        status: System.Net.HttpStatusCode.InternalServerError
                    );

                }
                // delete Image Of Video
                await _fileService.DeleteImageAsync(video.ImageVideoUrl);
                // Delete the video record from the database
                _unitOfWork.Videos.Delete(request.VideoId);
                await _unitOfWork.SaveChangesAsync();
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
