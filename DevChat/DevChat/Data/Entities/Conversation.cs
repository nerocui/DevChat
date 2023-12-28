using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevChat.Data.Entities;

public class Conversation
{
    public string Id { get; set; }
    public string Title { get; set; }
    public ICollection<ConversationMember> Members { get; set; }
    public ICollection<Message> Messages { get; set; }
}
