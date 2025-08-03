using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Category.Queries;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Category.Handlers
{
    internal class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, Response<List<CategoryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<Response<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = _unitOfWork.Categories.Read(); // assuming this returns List<Course>
                if (categories == null || !categories.Any())
                {
                    return Task.FromResult(Response<List<CategoryDto>>.ErrorResponse("there are no Categories available", null, HttpStatusCode.NotFound));

                }
                
                var CategoriesDTO = _mapper.Map<List<CategoryDto>>(categories);
                return Task.FromResult(Response<List<CategoryDto>>.SuccessResponse(CategoriesDTO));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<List<CategoryDto>>.ErrorWithException("There was an error", ex.Message));


            }
        }
    }
}
