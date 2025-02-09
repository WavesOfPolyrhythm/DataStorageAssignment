using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProjectRegistrationForm
{
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    [Required]
    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }
}
