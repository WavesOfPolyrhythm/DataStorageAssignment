using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class RoleEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string RoleName { get; set; } = null!;

    public ICollection<EmployeeEntity> Employees { get; set; } = [];
}
