 using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Category.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Category.Handlers
{
    public class AddNewCategoryHandler : IRequestHandler<AddNewCategoryCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
 
        public AddNewCategoryHandler(IUnitOfWork unitofwork, IStringLocalizer<ValidationMessages> localizer)
        {
            _unitofwork = unitofwork;
            _localizer = localizer;
           
        }
        public Task<Response<bool>> Handle(AddNewCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {

                // Check if the category already exists
                var existingCategory = _unitofwork.Categories.GetCategoryByName(request.ENName, request.ARName);
                if (existingCategory != null)
                    return Task.FromResult(Response<bool>.ErrorResponse(_localizer["CategoryAlreadyExists"]));

                var newCategory = new TrandingSystem.Domain.Entities.Category()
                {
                    CategoryNameAr = request.ARName,
                    CategoryNameEn = request.ENName,
                    CreateAt = DateTime.Now,
                    IsActive = true,
                    CreateBy = request.CreateBy,
                };
                _unitofwork.Categories.Create(newCategory);
                return Task.FromResult(Response<bool>.SuccessResponse(true, _localizer["GeneralOperationDone"]));
            }
            catch
            {
                return Task.FromResult(Response<bool>.ErrorResponse(_localizer["GeneralError"]));
            }
        }
    }
}
