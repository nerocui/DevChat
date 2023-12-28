using DevChat.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace DevChat.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public ICollection<ConversationMember> Conversations { get; set; }
    public ICollection<Message> Messages { get; set; }
}
