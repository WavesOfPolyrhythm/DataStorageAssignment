using System.ComponentModel.DataAnnotations;
namespace Data.Entities;

public class EmployeeEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;

    //KOpplingar
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;

    //En anställd kan ha flera projekt
    public ICollection<ProjectEntity> Projects { get; set; } = [];
}

