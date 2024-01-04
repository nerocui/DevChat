using DevChat.Data;
using DevChat.Share.Dtos;
using DevChat.Share.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DevChat.Controllers;

[Route("message")]
[Authorize]
public class MessageController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IHubContext<MessageHub> messageHub) : ControllerBase
{
    [HttpGet("{id}")]
    [Produces("text/html")]
    public async Task<IActionResult> GetById(string id)
    {
        var contentEntity = await dbContext.ProgrammableContents
            .FindAsync(id);
        if (contentEntity == null)
        {
            return BadRequest("MessageNotFound");
        }
        return Content(Utilities.GenerateMessageSource(contentEntity.Html, contentEntity.Js, contentEntity.Css), "text/html", Encoding.UTF8);
    }

    [HttpPut("send")]
    public async Task<IActionResult> Send(string convId, [FromBody] MessageDtoForSending message)
    {
        var contentEntity = await dbContext.ProgrammableContents.AddAsync(new()
        {
            Js = message.Js ?? "",
            Css = message.Css ?? "",
            Html = message.Html ?? "",
            LibrariesCsv = message.LibrariesCsv ?? ""
        });
        var user = await userManager.GetUserAsync(User);
        var messageEntity = await dbContext.Messages.AddAsync(new()
        {
            FromUserId = user?.Id,
            ConvId = convId,
            ContentId = contentEntity.Entity.Id,
        });
        await dbContext.SaveChangesAsync();
        messageHub.Clients.Groups(convId).SendAsync("ReceiveMessage", new MessageDtoForViewing
        {
            Id = messageEntity.Entity.Id,
            FromUserId = user.Id,
            FromUserEmail = user.Email,
            ConvId = convId,
            ContentId = contentEntity.Entity.Id,
            CreatedAt = messageEntity.Entity.CreatedAt,
        });
        return Ok();
    }

    [HttpGet("getpage")]
    public async Task<IActionResult> GetPage(string convId, int skip = 0)
    {
        var user = await userManager.GetUserAsync(User);
        var messages = await dbContext.Messages
            .Where(message => message.ConvId == convId)
            .OrderBy(message => message.CreatedAt)
            .Skip(skip)
            .Take(25)// magic number, we fetch 25 per page
            .Include(message => message.FromUser)
            .ToListAsync();
        return Ok(messages.Select(message => new MessageDtoForViewing
        {
            Id = message.Id,
            FromUserId = message.FromUserId,
            FromUserEmail = message.FromUser.Email,
            FromUserAvatarUrl = message.FromUser.AvatarUrl,
            ConvId = message.ConvId,
            ContentId = message.ContentId,
            IsFromYou = message.FromUserId == user?.Id,
            CreatedAt = message.CreatedAt,
        }));
    }
}
