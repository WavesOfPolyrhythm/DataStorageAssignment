using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class RolesRegistrationForm
{

    [Required]
    public string RoleName { get; set; } = null!;
}
