using DevChat.Data;
using DevChat.Data.Entities;
using DevChat.Share.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevChat.Controllers;

public class ConversationController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpPut("create")]
    public async Task<IActionResult> Create(string email)
    {
        //TODO check if it's already created

        var targetUser = await userManager.FindByEmailAsync(email);
        var authenticatedUser = await userManager.GetUserAsync(User);

        var hash = Utilities.HashEmails(new() { email, authenticatedUser.Email });
        var exist = await dbContext.Conversations
            .Where(c => c.Id == hash)
            .ToListAsync();

        if (exist.Any())
        {
            return Ok(hash);
        }

        var conversationEntity = await dbContext.Conversations
            .AddAsync(new()
            {
                Id = hash, // Important, the Id is unique per list of email
                Title = email + ", " + authenticatedUser.Email,
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
        return Ok(hash);
    }
}
