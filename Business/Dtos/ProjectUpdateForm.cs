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

    //Ta bort denna sen, ska räknas ut automatiskt i factoryn?
    [Required]
    public decimal? TotalPrice { get; set; }

    //Kopplingar
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int ServiceId { get; set; }
    [Required]
    public int StatusId { get; set; }
}
