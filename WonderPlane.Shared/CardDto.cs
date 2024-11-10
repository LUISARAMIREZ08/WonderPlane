using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Shared
{
    public class CardDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "El número de tarjeta debe contener exactamente 16 dígitos y no puede contener espacios.")]
        public required string Number { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        [StringLength(27, ErrorMessage = "El nombre del titular no puede ser mayor a 27 caracteres.")]
        [RegularExpression(@"^(?!.* {2})[A-Za-z\s]+$", ErrorMessage = "El nombre del titular no puede tener dos espacios consecutivos.")]
        public required string HolderName { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        public required DateTime? ExpirationDate { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        public required CardTypeDto CardTypeDto { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "El código de seguridad debe contener exactamente 3 dígitos.")]
        public required string SecurityCode { get; set; }

        public int? RegisteredUserId { get; set; }
    }

    public enum CardTypeDto
    {
        Credit,
        Debit
    }
}
