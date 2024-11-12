using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class Card
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El número de tarjeta es obligatorio.")]
    public required string Number { get; set; }

    [Required(ErrorMessage = "El nombre del titular de la tarjeta es obligatorio.")]
    public required string HolderName { get; set; }

    [Required(ErrorMessage = "La fecha de expiración de la tarjeta es obligatoria.")]
    public required DateTime ExpirationDate { get; set; }

    [Required(ErrorMessage = "El tipo de tarjeta es obligatorio.")]
    public required CardType CardType { get; set; }

    [Required(ErrorMessage = "El código de seguridad de la tarjeta es obligatorio.")]
    public required string SecurityCode { get; set; }

    public decimal? Balance { get; set; } = 3000000;

    public int? RegisteredUserId { get; set; }
    public User? RegisteredUser { get; set; }
}


public enum CardType
{
    Credit,
    Debit
}
