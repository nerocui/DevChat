
using DevChat.Data;
using Microsoft.EntityFrameworkCore;

namespace DevChat.Services;

public class DiceBearService(HttpClient httpClient, ApplicationDbContext dbContext) : IAvatarService
{
    public async Task<string> GetAvatar(string id)
    {
        //TODO: handle failure
        var entity = await dbContext.Avatars.FirstOrDefaultAsync(x => x.Id == id);
        return entity.SVG;
    }

    public async Task<string> RegisterAvatar(ApplicationUser user)
    {
        //TODO: handle failure
        var res = await httpClient.GetAsync($"https://api.dicebear.com/7.x/bottts/svg?seed={user.FirstName}");
        var avatar = await res.Content.ReadAsStringAsync();
        var entity = await dbContext.Avatars.AddAsync(new Data.Entities.Avatar()
        {
            SVG = avatar,
        });
        await dbContext.SaveChangesAsync();
        return entity.Entity.Id;
    }


}
