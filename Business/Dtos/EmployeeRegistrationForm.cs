using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class EmployeeRegistrationForm
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
}
