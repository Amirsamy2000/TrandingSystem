using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Data;
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
                throw new HubException("Not a member of this community || or is blocked.");

            
            var community = await _communityRepo.GetByIdAsync(id);
            if (community == null)
                return NotFound();

            ViewBag.CommunityId = id;
            ViewBag.UserId = userId;
            ViewBag.Name = community.Title;

            var roleClaim = User.FindAll(ClaimTypes.Role).ToList();

            if (roleClaim != null)
            {
                ViewBag.adminOnly = community.IsAdminOnly == true && !roleClaim.Any(r => r.Value.ToLower() == "admin");
                // e.g., "Admin", "User", etc.
            }


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> History(int communityId, int count = 30)
        {
            var messages = await _messageRepo.GetRecentAsync(communityId, count);
            return Json(messages.Select(m => new {
                m.MessageId,
                m.CommunityId,
                m.UserId,
                SenderName = m.User?.FullName ?? $"User {m.UserId}",
                Text = m.MessageText,
                SentAt = m.SentAt?.ToString("o") // ISO 8601 for JS Date
            }));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessage(int messageId, int communityId)
        {
            
            var isAdmin = User.IsInRole("Admin");

            var message = await _messageRepo.GetRecentAsync(communityId);
            var targetMessage = message.FirstOrDefault(m => m.MessageId == messageId);
            if (targetMessage == null)
                return NotFound();
            if (!isAdmin)
                return Forbid();
            var result = await _messageRepo.deleteMessage(messageId);
            if (!result)
                return StatusCode(500, "Failed to delete message.");
            return Ok(new { success = true });
        }


    }
}
