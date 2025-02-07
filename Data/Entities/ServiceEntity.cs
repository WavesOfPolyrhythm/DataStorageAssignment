using System.ComponentModel.DataAnnotations;
namespace Data.Entities;

public class ServiceEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public decimal Price { get; set; }

    //Kopplingar
    public int UnitId { get; set; }
    public UnitEntity Unit { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];
}