using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Application.Features.analysis.Queriers;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.analysis.Handlers
{
    public class GetCountStds_Courses_Videos_lecHandler : IRequestHandler<GetCountStds_Courses_Videos_lecQuery, List<int>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly UserManager<User> _userManager;
        //private readonly IStringLocalizer<ValidationMessages> _localizer;

        public GetCountStds_Courses_Videos_lecHandler(IUnitOfWork unitofwork, UserManager<User> userManager)
        {
            _unitofwork = unitofwork;
            _userManager = userManager;
         }
        public Task<List<int>> Handle(GetCountStds_Courses_Videos_lecQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<int> result = new List<int>();
                var countstds = _unitofwork.Users.GetActiveAndConfirmUser().Count();
                var countcourses = _unitofwork.Courses.Read().Count();
                var countvideos = _unitofwork.Videos.Read().Count();
                var countlec = _userManager.GetUsersInRoleAsync("LECTURER").Result.Count();
                result.Add(countstds);
                result.Add(countcourses);
                result.Add(countvideos);
                result.Add(countlec);
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                // For simplicity, we'll just return an empty list in case of an error
                return Task.FromResult(new List<int>());
            }

        }
    }
}
