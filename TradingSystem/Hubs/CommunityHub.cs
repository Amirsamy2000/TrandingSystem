using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TrandingSystem.Infrastructure.Data;

namespace TradingSystem.Hubs
{
    [Authorize]
    public class CommunityHub : Hub
    {
        private readonly ICommunityRepository _communityRepo;
        private readonly IMessageRepository _messageRepo;
        private readonly db23617Context _context;

        public CommunityHub(ICommunityRepository communityRepo, IMessageRepository messageRepo, db23617Context context)
        {
            _communityRepo = communityRepo;
            _messageRepo = messageRepo;
            _context = context;
        }

        public async Task JoinCommunity(int communityId)
        {
            var userId = int.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!await _communityRepo.IsUserInCommunityAsync(communityId, userId) || _communityRepo.IsUserBlocked(communityId,userId))
                throw new HubException("Not a member of this community.");

            await Groups.AddToGroupAsync(Context.ConnectionId, $"community:{communityId}");
        }

        public async Task SendMessage(int communityId, string messageText)
        {
            var userId = int.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!await _communityRepo.IsUserInCommunityAsync(communityId, userId))
                throw new HubException("Not a member of this community.");

            var message = new Message
            {
                CommunityId = communityId,
                UserId = userId,
                MessageText = messageText,
                SentAt = DateTime.UtcNow
            };
            await _messageRepo.AddAsync(message);

            // Try to get the user's display name from the Users table
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var senderName = user?.FullName ?? $"User {userId}";

            await Clients.Group($"community:{communityId}").SendAsync("ReceiveMessage", new {
                CommunityId = communityId,
                SenderId = userId,
                SenderName = senderName,
                Text = messageText,
                SentAt = message.SentAt
            });
        }
    }
}
