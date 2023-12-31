using System.ComponentModel.DataAnnotations;

namespace DevChat.Client.FormModels;

public class ComposeBoxModel
{
    [Required]
    public string Html { get; set; }
    public string Js { get; set; }
    public string Css { get; set; }
}
