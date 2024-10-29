using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class Flight
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El origen es obligatorio.")]
    public required string Origin { get; set; }

    [Required(ErrorMessage = "El destino es obligatorio.")]
    public required string Destination { get; set; }

    [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
    public required DateTime DepartureDate { get; set; }

    [Required(ErrorMessage = "La hora de salida es obligatoria.")]
    [DataType(DataType.Time, ErrorMessage = "Hora no valida")]
    public required TimeSpan DepartureTime { get; set; }

    [Required(ErrorMessage = "La fecha de llegada es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
    public required DateTime ArriveDate { get; set; }

    [Required(ErrorMessage = "La hora de llegada es obligatoria.")]
    [DataType(DataType.Time, ErrorMessage = "Hora no valida")]
    public required TimeSpan ArriveTime  { get; set; }

    [Required(ErrorMessage = "El estado del vuelo es obligatorio.")]
    public required FlightStatus FlightStatus { get; set; }

    [Required(ErrorMessage = "Se debe decir si el vuelo es internacional o no.")]
    public required bool IsInternational { get; set; }

    [Required(ErrorMessage = "El precio de la maleta es obligatorio.")]
    public required decimal BagPrice { get; set; }
    [Required(ErrorMessage = "El código del vuelo es obligatorio.")]
    public required string FlightCode { get; set; }
    public string? Duration { get; set; }
    [Required(ErrorMessage = "El precio de asientos primera clase es obligatorio.")]
    public required decimal FirstClassPrice { get; set; }

    [Required(ErrorMessage = "El precio de asientos económicos es obligatorio.")]
    public required decimal EconomicClassPrice { get; set; }

    public ICollection<Promotion> Promotions { get; } = new List<Promotion>();

    public ICollection<News> News { get; } = new List<News>();

    public ICollection<Seat> Seats { get; } = new List<Seat>();

    public ICollection<FlightRecommendation> FlightRecommendations { get; } = new List<FlightRecommendation>();

}

public enum FlightStatus
{
    Scheduled,
    Completed,
    Canceled
}


