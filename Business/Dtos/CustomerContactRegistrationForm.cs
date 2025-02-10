using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class CustomerContactRegistrationForm
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public int CustomerId { get; set; }
}
