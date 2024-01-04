using DevChat.Data;
using DevChat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevChat.Controllers;

[Route("avatar")]
[Authorize]
public class AvatarController : ControllerBase
{
    private IAvatarService avatarService;
    private ApplicationDbContext dbContext;
    private UserManager<ApplicationUser> userManager;

    public AvatarController(IAvatarService avatarService, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        this.avatarService = avatarService;
        this.dbContext = dbContext;
        this.userManager = userManager;
    }

    [HttpGet("{id}")]
    [Produces("image/svg+xml")]
    public async Task<IActionResult> Get(string id)
    {
        var svg = await avatarService.GetAvatar(id);
        return Content(svg, "image/svg+xml");
    }

    [HttpGet("getconv/{convId}")]
    [Produces("image/svg+xml")]
    public async Task<IActionResult> GetConv(string convId)
    {
        var convEntity = await dbContext.Conversations
            .Include(c => c.Members)
            .ThenInclude(m => m.User)
            .FirstOrDefaultAsync(c => c.Id == convId);

        if (convEntity.IsOneOnOne)
        {
            var user = await userManager.GetUserAsync(User);
            var theOtherMember = convEntity.Members.FirstOrDefault(m => m.UserId != user.Id);
            var svg = await avatarService.GetAvatar(theOtherMember.User.AvatarUrl.Split('/').Last());
            return Content(svg, "image/svg+xml");
        }
        else
        {
            //TODO: implement group chat
            throw new NotImplementedException();
        }
    }
}
