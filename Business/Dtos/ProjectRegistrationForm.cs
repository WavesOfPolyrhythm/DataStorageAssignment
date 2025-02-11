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
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int ServiceId { get; set; }
    [Required]
    public decimal TotalPrice { get; set; }
    [Required]
    public int StatusId { get; set; }
    [Required]
    public int Units { get; set; }

}
