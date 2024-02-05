using DevChat.Share.Dtos;
using System.Text.Json;

namespace DevChat.Client.Services;

public class UserService(HttpClient httpClient)
{
    public UserDtoForViewing? AuthenticatedUser { get; set; }
    public Dictionary<string, UserDtoForViewing> Users = new Dictionary<string, UserDtoForViewing>();

    public async Task<UserDtoForViewing> GetAuthenticatedUser()
    {
        if (AuthenticatedUser is not null)
        {
            return AuthenticatedUser;
        }
        var res = await httpClient.GetAsync("/user/GetAuthenticatedUser");
        var json = await res.Content.ReadAsStringAsync();
        AuthenticatedUser = JsonSerializer.Deserialize<UserDtoForViewing>(json, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        return AuthenticatedUser;
    }

    public async Task GetUsersByConvId(string convId)
    {
        var res = await httpClient.GetAsync($"/user/GetUsersByConvId?convId={convId}");
        var json = await res.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<ICollection<UserDtoForViewing>>(json, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        foreach (var user in users)
        {
            Users.Add(user.Id, user);
        }
    }

    public async Task<UserDtoForViewing> GetUser(string id)
    {
        if (Users.ContainsKey(id))
        {
            return Users[id];
        }
        var res = await httpClient.GetAsync($"/user/GetUserById?id={id}");
        var json = await res.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserDtoForViewing>(json, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        Users.Add(id, user);
        return user;
    }
}
