namespace DevChat.Share.Dtos;

public class MessageDtoForViewing
{
    public string Id { get; set; }
    public string FromUserId { get; set; }
    public string FromUserEmail { get; set; }
    public string FromUserAvatarUrl { get; set; }
    public string ConvId { get; set; }
    public string ContentId { get; set; }
    public bool IsFromYou { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
