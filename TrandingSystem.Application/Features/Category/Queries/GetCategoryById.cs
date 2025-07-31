using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Category.Queries
{
    public class GetCategoryById : IRequest<Response<CategoryDto>>
    {
        public int catId { get; set; }
    }
}
