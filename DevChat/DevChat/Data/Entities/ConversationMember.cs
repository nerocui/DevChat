namespace DevChat.Data.Entities;

public class ConversationMember
{
    public Conversation Conversation { get; set; }
    public string ConvId { get; set; }
    public ApplicationUser User { get; set; }
    public string UserId { get; set; }
}
