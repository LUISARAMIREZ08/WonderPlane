using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class Promotion
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "La descripción de la promoción es obligatorio.")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "La fecha de inicio de la promoción es obligatorio.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
    public required DateTime StartDate { get; set; }

    [Required(ErrorMessage = "La fecha de fin de la promoción es obligatorio.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
    public required DateTime EndDate { get; set; }

    [Required(ErrorMessage = "El descuento de la promoción es obligatorio.")]
    public required decimal Discount { get; set; }

    public required PromotionStatus PromotionStatus { get; set; }

    public required PromotionType PromotionType { get; set; }

    public int? FlightId { get; set; }
    public Flight? Flight { get; set; }
}

public enum PromotionStatus
{
    Active,
    Inactive
}

public enum PromotionType
{
    Seat,
    Bag,
}