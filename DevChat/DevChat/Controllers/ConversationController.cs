using DevChat.Data;
using DevChat.Data.Entities;
using DevChat.Share.Dtos;
using DevChat.Share.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevChat.Controllers;

[Route("conversation")]
[Authorize]
public class ConversationController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var authenticatedUser = await userManager.GetUserAsync(User);
        var conversations = await dbContext.Conversations
            .Include(c => c.Members)
            .ThenInclude(m => m.User)
            .Where(c => c.Members.Any(m => m.UserId == authenticatedUser.Id))
            .ToListAsync();
        return Ok(conversations.Select(c =>
        {
            var theOtherUser = c.Members.FirstOrDefault(m => m.UserId != authenticatedUser.Id).User;
            return new ConversationDtoForViewing()
            {
                Id = c.Id,
                Title = c.IsOneOnOne ? $"{theOtherUser.FirstName} {theOtherUser.LastName}" : c.Title,
                AvatarUrl = c.AvatarUrl
            };
        }
        ));
    }

    [HttpPut("create")]
    public async Task<IActionResult> Create(string email)
    {
        var targetUser = await userManager.FindByEmailAsync(email);
        var authenticatedUser = await userManager.GetUserAsync(User);

        var hash = Utilities.HashEmails(new() { email, authenticatedUser.Email });
        var exist = await dbContext.Conversations
            .Where(c => c.Id == hash)
            .ToListAsync();
        // TODO: Store and check this in redis for 1 minute
        // in case anyone creating the same chat at the same time
        if (exist.Any())
        {
            return Ok(hash);
        }

        var conversationEntity = await dbContext.Conversations
            .AddAsync(new()
            {
                Id = hash, // Important, the Id is unique per list of email
                Title = $"{targetUser.FirstName}, {authenticatedUser.FirstName}", //TODO: support group chat
                IsOneOnOne = true,
                AvatarUrl = $"/avatar/getconv/{hash}"
            });

        var members = new ConversationMember[]
        {
            new()
            {
                ConvId = hash,
                UserId = targetUser.Id,
            },
            new()
            {
                ConvId = hash,
                UserId = authenticatedUser.Id,
            }
        };
        await dbContext.ConversationMembers.AddRangeAsync(members);
        await dbContext.SaveChangesAsync();
        return Ok(hash);
    }

    [HttpGet("GetUserByEmail")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        return Ok(new UserDtoForViewing
        {
            Id = user.Id,
            Email = user.Email,
            AvatarUrl = user.AvatarUrl,
        });
    }
}
