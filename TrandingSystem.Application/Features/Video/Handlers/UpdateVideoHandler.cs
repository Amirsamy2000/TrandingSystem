 using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Video.Commands;

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class UpdateVideoHandler : IRequestHandler<UpdateVideoCommand, Response<bool>>
    {
        public async  Task<Response<bool>> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // here Logic
                string responseMessage = request.Culture == "ar"
              ? "تم تعديل  الفيديو بنجاح"
              : "Video updated successfully";
                return Response<bool>.SuccessResponse(true, responseMessage);
            }
            catch {
                string errorMsg = request.Culture == "ar"
                     ? "حدث خطأ أثناء تعديل  الفيديو"
                     : "An error occurred while Updating the video";
                return Response<bool>.ErrorResponse(
                    errorMsg,
                    status: System.Net.HttpStatusCode.InternalServerError
                );

            }

        }
    }
}
