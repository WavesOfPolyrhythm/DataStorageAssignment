using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class EmployeeModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;
}
