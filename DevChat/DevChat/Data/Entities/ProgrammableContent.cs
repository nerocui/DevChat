using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevChat.Data.Entities;

public class ProgrammableContent
{
    [ScaffoldColumn(false)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string Js { get; set; }
    public string Css { get; set; }
    public string Html { get; set; }
    public string LibrariesCsv { get; set; }
    public ICollection<Message> Messages { get; set; }
}
