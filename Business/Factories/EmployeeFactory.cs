using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class EmployeeFactory
{
    public static EmployeeRegistrationForm Create() => new();

    public static EmployeeEntity Create(EmployeeRegistrationForm form) => new()
    {
      Name = form.Name,
      Email = form.Email,
    };

    public static EmployeeModel Create(EmployeeEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Email = entity.Email,
    };
}
