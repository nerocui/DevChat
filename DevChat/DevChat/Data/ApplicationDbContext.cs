using DevChat.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevChat.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ConversationMember> ConversationMembers { get; set; }
    public DbSet<ProgrammableContent> ProgrammableContents { get; set; }
    public DbSet<Avatar> Avatars { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Conversation>()
            .HasMany(conv => conv.Members)
            .WithOne(cm => cm.Conversation)
            .HasForeignKey(cm => cm.ConvId);

        builder.Entity<ApplicationUser>()
            .HasMany(user => user.Conversations)
            .WithOne(cm => cm.User)
            .HasForeignKey(cm => cm.UserId);

        builder.Entity<ConversationMember>()
            .HasKey(cm => new { cm.ConvId, cm.UserId });

        builder.Entity<ConversationMember>()
            .HasOne(cm => cm.Conversation)
            .WithMany(conv => conv.Members)
            .HasForeignKey(cm => cm.ConvId);

        builder.Entity<ConversationMember>()
            .HasOne(cm => cm.User)
            .WithMany(user => user.Conversations)
            .HasForeignKey(cm => cm.UserId);

        builder.Entity<Message>()
            .HasOne(message => message.FromUser)
            .WithMany(user => user.Messages)
            .HasForeignKey(message => message.FromUserId);

        builder.Entity<Message>()
            .HasOne(message => message.Conversation)
            .WithMany(conv => conv.Messages)
            .HasForeignKey(message => message.ConvId);

        builder.Entity<Message>()
            .HasOne(message => message.Content)
            .WithMany(content => content.Messages)
            .HasForeignKey(message => message.ContentId);

        builder.Entity<ProgrammableContent>();
        builder.Entity<Avatar>();
    }
}
