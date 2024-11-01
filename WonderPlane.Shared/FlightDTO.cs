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

        [Required(ErrorMessage = "El precio de la maleta es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de la maleta debe ser mayor que 0.")]
        public required decimal BagPrice { get; set; }
        [Required(ErrorMessage = "El código del vuelo es obligatorio.")]
        public required string FlightCode { get; set; }
        [Required(ErrorMessage = "La duración del vuelo es obligatoria.")]
        public int Duration { get; set; }
        public required FlightStatusEnum FlightStatus { get; set; } = FlightStatusEnum.Scheduled;

        [Required(ErrorMessage = "El precio de asientos primera clase es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de primera clase debe ser mayor que 0.")]
        public required decimal FirstClassPrice { get; set; }

        [Required(ErrorMessage = "El precio de asientos económicos es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de clase económica debe ser mayor que 0.")]
        public required decimal EconomicClassPrice { get; set; }
    }
}

public enum FlightStatusEnum
{
    Scheduled,
    Completed,
    Canceled
}

