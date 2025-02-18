using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    public decimal TotalPrice { get; set; }

    public string ServiceName { get; set; } = null!;
    public decimal ServicePrice { get; set; }

    public string Unit {  get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string CustomerContact { get; set; } = null!;
    public string StatusName { get; set; } = null!;

    public string ProjectManager { get; set; } = null!;
    public string Role { get; set; } = null!;

}   
