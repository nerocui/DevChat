using DevChat.Share.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace DevChat.Controllers;

public class NotificationHub : Hub
{
    public async Task CreateNewChat(string userId, ConversationDtoForViewing conv)
    {
        await Clients.User(userId).SendAsync("NewChatCreated", conv);
    }
}
