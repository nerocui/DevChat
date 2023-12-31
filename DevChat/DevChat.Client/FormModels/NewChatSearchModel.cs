using System.ComponentModel.DataAnnotations;

namespace DevChat.Client.FormModels;

public class NewChatSearchModel
{
    [Required(ErrorMessage = "Email is required for searching.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; }
}
