using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class Seat
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El número de silla es obligatorio.")]
    public required int Number { get; set; }

    [Required(ErrorMessage = "El tipo de silla es obligatorio.")]
    public required SeatType SeatType { get; set; }

    [Required(ErrorMessage = "El estado de la silla es obligatorio.")]
    public required SeatStatus SeatStatus { get; set; }

    [Required(ErrorMessage = "El precio de la silla es obligatorio.")]
    public required decimal Price { get; set; }

    public int? FlightId { get; set; }
    public Flight? Flight { get; set; }

    public int? TicketId { get; set; }
    public Ticket? Ticket { get; set; }
}


public enum SeatType
{
    Economic,
    FirstClass
}

public enum SeatStatus
{
    Available,
    Occupied
}