using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.models;

public class Forum
{   
    [Key]
    public long Id { get; set; }

    [StringLength(50)]
    public required string Title { get; set; }

    [StringLength(255)]
    public required string Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime PublicationDate { get; set; }

    public User User { get; set; }

    public ICollection<Message> Messages { get; } = new List<Message>();
}