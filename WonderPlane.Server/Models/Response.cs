using System.ComponentModel.DataAnnotations;
using WonderPlane.Server.Models;

public class Response
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public required string Content { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Invalid Date")]
    public required DateTime Date { get; set; }

    public int? QuestionId { get; set; }
    public Question? Question { get; set; }

    public int AdminId { get; set; }
    public User? User { get; set; }
}
