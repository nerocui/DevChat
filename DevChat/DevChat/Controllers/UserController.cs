using DevChat.Data;
using DevChat.Share.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevChat.Controllers;

[Route("user")]
[Authorize]
public class UserController(
        ApplicationDbContext _dbContext,
        UserManager<ApplicationUser> _userManager
    ) : ControllerBase
{
    [HttpGet("GetAuthenticatedUser")]
    public async Task<IActionResult> GetAuthenticatedUser()
    {
        var user = await _userManager.GetUserAsync(User);
        return Ok(new UserDtoForViewing()
        {
            Id = user.Id,
            Email = user.Email,
            AvatarUrl = user.AvatarUrl,
            FirstName = user.FirstName,
            LastName = user.LastName,
        });
    }

    [HttpGet("GetUsersByConvId")]
    public async Task<IActionResult> GetUsersByConvId(string convId)
    {
        var cms = await _dbContext.ConversationMembers
            .Where(cm => cm.ConvId == convId)
            .Include(cm => cm.User)
            .ToListAsync();

        var users = cms.Select(cm => new UserDtoForViewing()
        {
            Id = cm.User.Id,
            Email = cm.User.Email,
            AvatarUrl = cm.User.AvatarUrl,
            FirstName = cm.User.FirstName,
            LastName = cm.User.LastName,
        });

        return Ok(users);
    }

    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return Ok(new UserDtoForViewing()
        {
            Id = user.Id,
            Email = user.Email,
            AvatarUrl = user.AvatarUrl,
            FirstName = user.FirstName,
            LastName = user.LastName,
        });
    }
}
