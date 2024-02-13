namespace DevChat.Share.Dtos;

public class MessageDtoForViewing
{
    public string Id { get; set; }
    public UserDtoForViewing Sender { get; set; }
    public string ConvId { get; set; }
    public string ContentId { get; set; }
    public bool IsFromYou { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool NewlyLoadedBottom { get; set; }
}
