using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Users.Commands
{
    public class SwitchIsBlockedEmailConfirmedCommand: IRequest<Response<UserDto>>
    {
        public int UserId { get; set; }

        /// <summary>
        /// 0 not change , no switch IsBlocked
        /// 1 yes swtch IsBlocked column
        /// </summary>
        public int SwitchIsBlockedColumn { get; set; }

        /// <summary>
        /// 0 not change , not switch EmailConfirmed
        /// 1 yes swtch EmailConfirmed column
        /// </summary>
        public int SwitchEmailConfirmed { get; set; } 


        public string ColumnName { get; }
        public SwitchIsBlockedEmailConfirmedCommand(int userId, int switchIsBlockedColumn, int switchEmailConfirmed)
        {
            UserId = userId;
            SwitchEmailConfirmed = switchEmailConfirmed;
            SwitchIsBlockedColumn = switchIsBlockedColumn;
            ColumnName = switchIsBlockedColumn == 1? "IsBlocked": switchEmailConfirmed == 1? "EmailConfirmed":null;
        }
    }
}
