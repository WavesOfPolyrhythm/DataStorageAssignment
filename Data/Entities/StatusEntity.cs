using System.ComponentModel.DataAnnotations;
namespace Data.Entities;

public class StatusEntity
{

    [Key]
    public int Id { get; set; }
    [Required]
    public string StatusName { get; set; } = string.Empty;

    public ICollection<ProjectEntity> Projects { get; set; } = [];
}