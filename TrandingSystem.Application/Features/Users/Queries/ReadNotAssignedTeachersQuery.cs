using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Features.Users.Queries
{
    public class ReadNotAssignedTeachersQuery:IRequest<Response<List<UserDto>>>
    {
        /// <summary>
        /// if you want to get just teachers who not assigned to this course, you can set this property to 0 if you want to get all teachers
        /// </summary>
        public int CourseId { get; set; }


    }
}
