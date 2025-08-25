using Amazon.Runtime.Internal;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Roles.Queries;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Roles.Handlers
{
    public class GetUserRolesHandler : IRequestHandler<GetUserRolesQuery, List<RoleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;


        public GetUserRolesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<List<RoleDto>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _unitOfWork.Roles.getUserRole(request.UserId);
            var rolesDto = _mapper.Map<List<RoleDto>>(roles);
            return Task.FromResult(rolesDto);
        }
    }
}
