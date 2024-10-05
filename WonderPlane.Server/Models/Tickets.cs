using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class Ticket
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El número de tiquete es obligatorio.")]
    public required int Number { get; set; }

    public int? SeatId { get; set; }
    public Seat? Seat { get; set; }

    public int? TravelerId { get; set; }
    public Traveler? Traveler { get; set; }

    public int? PurchaseId { get; set; }
    public Purchase? Purchase { get; set; }
    
    public int? ReservationId { get; set; }
    public Reservation? Reservation { get; set; }

    public int? BoardingPassId { get; set; }
    public BoardingPass? BoardingPass { get; set; }
}
