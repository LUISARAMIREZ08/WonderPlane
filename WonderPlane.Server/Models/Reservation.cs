using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class Reservation
{
    [Key]
    public int Id { get; set; }


    [Required(ErrorMessage = "La fecha de la reserva es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
    public required DateTime ReservationDate { get; set; }

    [Required(ErrorMessage = "La fecha del limite de pago es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
    public required DateTime PaymentLimitDate { get; set; }

    [Required(ErrorMessage = "El estado de la reserva es obligatorio.")]
    public required ReservationStatus ReservationStatus { get; set; }

    public int? RegisteredUserId { get; set; }
    public User? RegisteredUser { get; set; }

    public ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}

public enum ReservationStatus
{
    Reserved,
    Canceled
}