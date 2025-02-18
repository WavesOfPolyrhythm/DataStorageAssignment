using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Entities;

public class ProjectEntity
{
    [Key]
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
    [Required]
    public decimal TotalPrice { get; set; }

    //Kopplingar
    public int EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; } = null!;

    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

    public int ServiceId { get; set; }
    public ServiceEntity Service { get; set; } = null!;

    public int StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;
   
}