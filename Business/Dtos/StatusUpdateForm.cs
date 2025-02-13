using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class StatusUpdateForm
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string StatusName { get; set; } = string.Empty;
}
