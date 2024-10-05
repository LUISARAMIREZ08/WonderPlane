using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class Purchase
{
    [Key]
    public int Id { get; set; }


    [Required(ErrorMessage = "La fecha de la compra es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
    public required DateTime PurchaseDate { get; set; }

    [Required(ErrorMessage = "El precio total de la compra es obligatorio.")]
    public required decimal TotalAmount { get; set; }

    [Required(ErrorMessage = "El estado de la compra es obligatorio.")]
    public required PurchaseStatus PurchaseStatus { get; set; }

    public int? RegisteredUserId { get; set; }
    public User? RegisteredUser { get; set; }

    public ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}

public enum PurchaseStatus
{
    Completed,
    Canceled,
    Pending,
}