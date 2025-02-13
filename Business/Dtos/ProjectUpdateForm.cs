using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Dtos;

public class ProjectUpdateForm
{
    [Required]
    public int Id { get; set; }
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
