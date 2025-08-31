 using MediatR;
using System.Linq;
using System.Linq.Expressions;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Users.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Users.Handlers
{
    public class GetAllUsersByStatusHandler : IRequestHandler<GetAllUsersByStatusQuery, Response<List<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersByStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
         }
        public async Task<Response<List<UserDto>>> Handle(GetAllUsersByStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
               Func<User, bool> predicate = request.Status switch
                {
                    0 => u => true,                          // كل المستخدمين
                    1 => u => !u.IsBlocked && u.EmailConfirmed, // Active Users
                    2 => u => u.IsBlocked,                   // Blocked Users
                    3 => u => !u.EmailConfirmed,             // Users لم يؤكدوا الإيميل
                    4 => u => !u.CommunityMembers.Any(x=>x.CommunityId==request.Id),// Users خارج الكوميونيتي
                    _=>u=>u.CommunityMembers.Any(x => x.CommunityId == request.Id) // Users داخل الكوميونيتي

                };
                var filterUser = _unitOfWork.Users.Read().Where(predicate).
                    Select(x=> new UserDto()
                    {
                        Id = x.Id,
                        FullName = x.FullName,
                        Mobile = x.Mobile,
                        IsBlockedSite = x.IsBlocked,
                        IsBlockedCommunity=x.CommunityMembers.Where(x=>x.CommunityId==request.Id).FirstOrDefault().IsBlocked ?? false,
                        Email =x.Email,
                        RegisteredAt = x.RegisteredAt,
                        NationalId = x.NationalId,
                        
                        // Fit All Propery if Need


                    }).ToList();


                return Response<List<UserDto>>.SuccessResponse(filterUser, "Success");
            }
            catch
            {
                return Response<List<UserDto>>.ErrorResponse("");
            }
            
        }
    }
}
