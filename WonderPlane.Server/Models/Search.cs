using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;
public class Search
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "La fecha de la busqueda es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
    public required DateTime SearchDate { get; set; }

    [Required(ErrorMessage = "El contenido de la busqueda es obligatorio.")]
    [StringLength(100, ErrorMessage = "El contenido de la busqueda no puede exceder los 100 caracteres.")]
    public required string Content { get; set; }

    public int? RegisteredUserId { get; set; }
    public User? RegisteredUser { get; set; }

}
