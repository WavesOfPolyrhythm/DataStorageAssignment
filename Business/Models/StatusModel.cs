using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class StatusModel
{
    public int Id { get; set; }
    public string StatusName { get; set; } = string.Empty;
}
