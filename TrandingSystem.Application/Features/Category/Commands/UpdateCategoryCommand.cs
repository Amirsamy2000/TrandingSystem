using Amazon.Runtime.Internal;
using MediatR;
using TradingSystem.Application.Common.Response;


namespace TrandingSystem.Application.Features.Category.Commands
{
    public class UpdateCategoryCommand:IRequest<Response<bool>>
    {
        public int CategoryId { get; set; }
        public string ENName { get; set; }
        public string ARName { get; set; }
        public UpdateCategoryCommand(int categoryId, string eNName, string aRName)
        {
            CategoryId = categoryId;
            ENName = eNName;
            ARName = aRName;
        }

    }
}
