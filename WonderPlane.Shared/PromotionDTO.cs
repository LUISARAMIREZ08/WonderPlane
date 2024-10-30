using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WonderPlane.Shared
{
    public class PromotionDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción de la promoción es obligatoria.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "La fecha de inicio de la promoción es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Fecha no válida.")]
        public required DateTime StartDate { get; set; }

        [Required(ErrorMessage = "La fecha de fin de la promoción es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Fecha no válida.")]
        public required DateTime EndDate { get; set; }

        [Required(ErrorMessage = "El descuento de la promoción es obligatorio.")]
        public required decimal Discount { get; set; }

        [Required(ErrorMessage = "El estado de la promoción es obligatorio.")]
        public required string PromotionStatus { get; set; }

        [Required(ErrorMessage = "El tipo de promoción es obligatorio.")]
        public required string PromotionType { get; set; }

        public int? FlightId { get; set; }
    }
}

