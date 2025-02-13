using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class RolesModel
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;
}
