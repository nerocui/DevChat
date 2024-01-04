using DevChat.Data;

namespace DevChat.Services;

public interface IAvatarService
{
    // saves the avatar to the db, return ir
    Task<string> RegisterAvatar(ApplicationUser user);
    Task<string> GetAvatar(string id);
}
