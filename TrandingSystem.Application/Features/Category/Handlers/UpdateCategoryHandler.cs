

 using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Category.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Category.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;

        public UpdateCategoryHandler (IUnitOfWork unitofwork, IStringLocalizer<ValidationMessages> localizer)
        {
            _unitofwork = unitofwork;
            _localizer = localizer;

        }
        public  async Task<Response<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //  Check if the category already exists
                var existingCategory = _unitofwork.Categories.ReadById(request.CategoryId);
                if (existingCategory == null)
                    return Response<bool>.ErrorResponse(_localizer["NotFoundCategory"]);
                existingCategory.CategoryNameAr = request.ARName;
                existingCategory.CategoryNameEn = request.ENName;
                _unitofwork.Categories.Update(existingCategory);
                await _unitofwork.SaveChangesAsync();
                return Response<bool>.SuccessResponse(true, _localizer["GeneralOperationDone"]);
            }
            catch
            {
                return Response<bool>.ErrorResponse(_localizer["GeneralError"]);

            }


        }
    }
}
