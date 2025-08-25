using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

namespace TradingSystem.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ICommunityRepository _communityRepo;
        private readonly IMessageRepository _messageRepo;

        public ChatController(ICommunityRepository communityRepo, IMessageRepository messageRepo)
        {
            _communityRepo = communityRepo;
            _messageRepo = messageRepo;
        }

        public async Task<IActionResult> Room(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
            if (!await _communityRepo.IsUserInCommunityAsync(id, userId) || _communityRepo.IsUserBlocked(id, userId))
                throw new HubException("Not a member of this community.");

            
            var community = await _communityRepo.GetByIdAsync(id);
            if (community == null)
                return NotFound();

            ViewBag.CommunityId = id;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> History(int communityId, int count = 30)
        {
            var messages = await _messageRepo.GetRecentAsync(communityId, count);
            return Json(messages.Select(m => new {
                m.CommunityId,
                m.UserId,
                SenderName = m.User?.FullName ?? $"User {m.UserId}",
                Text = m.MessageText,
                SentAt = m.SentAt?.ToString("o") // ISO 8601 for JS Date
            }));
        }
    }
}
