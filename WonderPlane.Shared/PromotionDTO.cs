﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WonderPlane.Shared;

public class PromotionDTO
{
        public int Id { get; set; }

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
    public PromotionStatusEnum PromotionStatus { get; set; }

    [Required(ErrorMessage = "El tipo de promoción es obligatorio.")]
    public PromotionTypeEnum PromotionType { get; set; }

    public int? FlightId { get; set; } // Opcional
}


public enum PromotionStatusEnum
{
    Active,
    Inactive
}

public enum PromotionTypeEnum
{
    Seat,
    Bag,
}