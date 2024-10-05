using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class BoardingPass
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La fecha de check-in es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
    public DateTime CheckInDate { get; set; }

    [Required(ErrorMessage = "La hora de check-in es obligatoria.")]
    [DataType(DataType.Time, ErrorMessage = "Hora no valida")]
    public TimeSpan CheckInTime { get; set; }

    [Required(ErrorMessage = "El estado del check-in es obligatorio.")]
    public CheckInStatus CheckInStatus { get; set; }

    public int? TicketId { get; set; }
    public Ticket? Ticket { get; set; }
}

public enum CheckInStatus
{
    Completed,
    Pending,
    Canceled
}