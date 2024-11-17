using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Shared
{
    public class FlightDTO
    {
        public int Id { get; set; } = 0;
        [Required(ErrorMessage = "El origen es obligatorio.")]
        public required string Origin { get; set; }

        [Required(ErrorMessage = "El destino es obligatorio.")]
        public required string Destination { get; set; }

        [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
        public required DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "La hora de salida es obligatoria.")]
        public required string DepartureTime { get; set; }

        [Required(ErrorMessage = "La fecha de llegada es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
        public required DateTime ArriveDate { get; set; }

        [Required(ErrorMessage = "La hora de llegada es obligatoria.")]
        public required string ArriveTime { get; set; }

        [Required(ErrorMessage = "Se debe decir si el vuelo es internacional o no.")]
        public required bool IsInternational { get; set; }

        public int BagPrice { get; set; }
        [Required(ErrorMessage = "El código del vuelo es obligatorio.")]
        public required string FlightCode { get; set; }
        [Required(ErrorMessage = "La duración del vuelo es obligatoria.")]
        public int Duration { get; set; }
        public required FlightStatusEnum FlightStatus { get; set; } = FlightStatusEnum.Scheduled;

        [Required(ErrorMessage = "El precio de asientos de primera clase es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio de primera clase debe ser mayor que 0.")]
        public required int FirstClassPrice { get; set; }
        public int FirstClassPricePromotion { get; set; }

        [Required(ErrorMessage = "El precio de asientos económicos es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio de clase económica debe ser mayor que 0.")]
        public required int EconomicClassPrice { get; set; }
        public int EconomicClassPricePromotion { get; set; }
        public bool HasPromotion { get; set; }
        public string? CodePromotion { get; set; }
        [Range(0, 100, ErrorMessage = "El porcentaje de descuento debe estar entre 0 y 100.")]
        public int? DiscountPercentage { get; set; }
        public string? PromotionDescription { get; set; }
    }
}

public enum FlightStatusEnum
{
    Scheduled,
    Completed,
    Canceled
}

