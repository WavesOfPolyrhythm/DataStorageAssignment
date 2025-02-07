using System.ComponentModel.DataAnnotations;
namespace Data.Entities;

public class CustomerContactEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;

    //Koppling
    public int CustomerId { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;
}
