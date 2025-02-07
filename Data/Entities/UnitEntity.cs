﻿using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UnitEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    public ICollection<ServiceEntity> Services { get; set; } = [];
}
