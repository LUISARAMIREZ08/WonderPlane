using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class News
{
    [Key]
    public int Id { get; set; }

    public int? FlightId { get; set; }
    public Flight? Flight { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(50, ErrorMessage = "El título no puede exceder los 50 caracteres.")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "El contenido es obligatorio.")]
    [StringLength(255, ErrorMessage = "El contenido no puede exceder los 255 caracteres.")]
    public required string Content { get; set; }

    [Required(ErrorMessage = "La fecha de publicación es obligatoria.")]
    [DataType(DataType.Date)]
    public required DateTime Date { get; set; }
}   
