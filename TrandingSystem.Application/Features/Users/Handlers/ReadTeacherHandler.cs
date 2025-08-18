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
    public class ReadTeacherHandler : IRequestHandler<ReadTeachersQuery, Response<List<UserDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public ReadTeacherHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Response<List<UserDto>>> Handle(ReadTeachersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = _unitOfWork.Users.ReadAllTeacher();

               List<UserDto> usersDto = _mapper.Map<List<UserDto>>(users);

                if (usersDto == null || !usersDto.Any())
                {
                    return Task.FromResult(Response<List<UserDto>>.ErrorResponse("No teachers found", null, HttpStatusCode.NotFound));
                }

                return Task.FromResult(Response<List<UserDto>>.SuccessResponse(usersDto));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<List<UserDto>>.ErrorWithException("An error occurred", ex.Message));
            }
        }
    }
}
