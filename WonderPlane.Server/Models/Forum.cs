using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class Forum
{   
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public required string Title { get; set; }

    [StringLength(255)]
    public required string Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime PublicationDate { get; set; }

    public string? UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<Message> Messages { get; } = new List<Message>();
}