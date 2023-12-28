using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevChat.Data.Entities;

public class Message
{
    [ScaffoldColumn(false)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public ApplicationUser FromUser { get; set; }
    public string FromUserId { get; set; }
    public Conversation Conversation { get; set; }
    public string ConvId { get; set; }
    public ProgrammableContent Content { get; set; }
    public string ContentId { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
