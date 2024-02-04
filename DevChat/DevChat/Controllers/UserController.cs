using DevChat.Data;
using DevChat.Share.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevChat.Controllers;

[Route("user")]
[Authorize]
public class UserController : ControllerBase
{
    private UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

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
}
