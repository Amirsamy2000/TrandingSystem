 using MediatR;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Category.Commands
{
    public class AddNewCategoryCommand:IRequest<Response<bool>>
    {
        public string ENName { get; set; }
        public string ARName { get; set; }
        public int CreateBy { set; get; }

        public AddNewCategoryCommand(string eNName, string aRName, int createBy)
        {
            ENName = eNName;
            ARName = aRName;
            CreateBy = createBy;
        }
    }
}
