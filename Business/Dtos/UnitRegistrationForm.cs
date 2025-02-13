﻿using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class UnitRegistrationForm
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;
}
