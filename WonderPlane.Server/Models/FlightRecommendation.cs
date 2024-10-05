using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;
public class FlightRecommendation
{
    [Key]
    public int Id { get; set; }

    public int? FlightId { get; set; }
    public Flight? Flight { get; set; }

    public int? RecommendationId { get; set; }
    public Recommendation? Recommendation { get; set; }
}
