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
using TrandingSystem.Application.Features.Users.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Users.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Response<List<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<Response<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> users = _unitOfWork.Users.Read();
            if (!users.Any() || users == null)
                return Task.FromResult( Response<List<UserDto>>.ErrorResponse("there isn't any user", null, HttpStatusCode.NotFound));

            return Task.FromResult(Response<List<UserDto>>.SuccessResponse(_mapper.Map<List<UserDto>>(users)));
        }
    }
}
