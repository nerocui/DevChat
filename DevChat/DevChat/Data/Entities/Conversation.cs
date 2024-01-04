namespace DevChat.Data.Entities;

public class Conversation
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string AvatarUrl { get; set; }
    public bool IsOneOnOne { get; set; }
    public ICollection<ConversationMember> Members { get; set; }
    public ICollection<Message> Messages { get; set; }
}
