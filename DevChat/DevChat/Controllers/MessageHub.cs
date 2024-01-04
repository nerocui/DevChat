using DevChat.Share.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace DevChat.Controllers;

public class MessageHub : Hub
{
    public async Task SendMessage(string convId, MessageDtoForViewing message)
    {
        await Clients.Group(convId).SendAsync("ReceiveMessage", message);
    }

    public async Task AddToGroup(string convId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, convId);
    }

    public async Task RemoveFromGroup(string convId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, convId);
    }

    public async Task IndicateTyping(string convId)
    {
        await Clients.Group(convId).SendAsync("IndicateTyping", Context.UserIdentifier);
    }
}
