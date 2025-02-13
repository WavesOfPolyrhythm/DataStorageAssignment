using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ServicesUpdateForm
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public decimal Price { get; set; }
}
