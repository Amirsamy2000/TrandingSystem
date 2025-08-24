using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Community.Commands
{
    public class DeleteCommunityCommand:IRequest<Response<bool>>
    {
        public int CommunityId { set; get; }
        public DeleteCommunityCommand(int Id)
        {
            CommunityId = Id;
        }
    }
}
