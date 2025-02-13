using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class RolesUpdateForm
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string RoleName { get; set; } = null!;
}
