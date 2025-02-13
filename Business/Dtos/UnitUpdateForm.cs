using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class UnitUpdateForm
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;
}
