using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class StatusRegistrationForm
{
    [Required]
    public string StatusName { get; set; } = string.Empty;
}
