using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ServicesRegistrationForm
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public decimal Price { get; set; }
}
