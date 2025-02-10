using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class CustomerContactModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int CustomerId { get; set; }
}
