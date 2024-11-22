using System.ComponentModel.DataAnnotations;
using WonderPlane.Server.Models;

public class Question
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public required string Content { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Invalid Date")]
    public required DateTime Date { get; set; }

    public string? Theme { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public State StateQuestion { get; set; }

    // Propiedad de navegación para respuestas
    public ICollection<Response> Responses { get; set; } = new List<Response>();

    public enum State
    {
        Pendiente,
        Respondida
    }

  
}
