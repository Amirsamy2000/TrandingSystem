using MediatR;
using TrandingSystem.Application.Features.Users.Commands;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
{
    //private readonly IUserRepository _userRepository;


    //public AddUserCommandHandler(IUserRepository userRepository)
    //{
    //    _userRepository = userRepository;

    //}

    //public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
    //{
    //   var res= _userRepository.AddUser(request.Name, request.Email);

    //    return res;
    //}
    public Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
