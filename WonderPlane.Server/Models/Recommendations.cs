using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class Recommendation
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "La fecha de la recomendacion es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
    public required DateTime RecommendationDate { get; set; }

    [Required(ErrorMessage = "El motivo de la recomendacion es obligatorio.")]
    public required string Reason { get; set; }

    public int? RegisteredUserId { get; set; }
    public User? RegisteredUser { get; set; }

    public ICollection<FlightRecommendation> FlightRecommendations { get; } = new List<FlightRecommendation>();
}