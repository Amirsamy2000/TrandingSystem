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
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, Response<CategoryDto>>
    {

        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoryByIdHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<CategoryDto>> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            try
            {
                var category = _repository.ReadById(request.catId);
                if (category == null)
                {
                    return Response<CategoryDto>.ErrorResponse($"Category with ID {request.catId} not found.", null, HttpStatusCode.NotFound);
                }

                var categoryDto = _mapper.Map<CategoryDto>(category);

                return Response<CategoryDto>.SuccessResponse(categoryDto);
            }
            catch (Exception ex)
            {
                return Response<CategoryDto>.ErrorWithException("An error occurred", ex.Message);
            }
        }

    }
}
