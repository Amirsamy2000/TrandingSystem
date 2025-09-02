using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Users.Commands;
using TrandingSystem.Infrastructure.Repositories;

namespace TrandingSystem.Application.Features.Users.Handlers
{
    public class SwitchIsBlockedEmailConfirmedHandler : IRequestHandler<SwitchIsBlockedEmailConfirmedCommand, Response<UserDto>>
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SwitchIsBlockedEmailConfirmedHandler(UnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<Response<UserDto>> Handle(SwitchIsBlockedEmailConfirmedCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ColumnName))
                    return Task.FromResult(Response<UserDto>.ErrorResponse("ColumnName is invalid", null, System.Net.HttpStatusCode.BadRequest));
                
                var user = _unitOfWork.Users.SwitchColumn(request.UserId, request.ColumnName);
                if (user == null)
                    return Task.FromResult(Response<UserDto>.ErrorResponse("User not found", null, System.Net.HttpStatusCode.NotFound));

                _unitOfWork.SaveChangesAsync();
                return Task.FromResult(Response<UserDto>.SuccessResponse(_mapper.Map<UserDto>(user)));


            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<UserDto>.ErrorResponse(ex.Message, null, System.Net.HttpStatusCode.InternalServerError));
            }


        }
    }
}
