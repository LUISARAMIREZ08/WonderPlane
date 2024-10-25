using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Shared;

public class PromotionDTO
{
    [Required(ErrorMessage = "La descripción de la promoción es obligatoria.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no válida.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Fecha no válida.")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "El descuento es obligatorio.")]
    public decimal Discount { get; set; }

    [Required(ErrorMessage = "El estado de la promoción es obligatorio.")]
    public PromotionStatus PromotionStatus { get; set; }

    [Required(ErrorMessage = "El tipo de promoción es obligatorio.")]
    public PromotionType PromotionType { get; set; }

    public int? FlightId { get; set; } // Opcional
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