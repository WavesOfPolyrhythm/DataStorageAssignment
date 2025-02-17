using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class ServicesModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string UnitName { get; set; } = null!;
}
