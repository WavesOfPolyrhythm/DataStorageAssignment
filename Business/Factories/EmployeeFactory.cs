using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class EmployeeFactory
{
    public static EmployeeRegistrationForm Create() => new();
    public static EmployeeUpdateForm UpdateForm() => new();

    public static EmployeeEntity Create(EmployeeRegistrationForm form) => new()
    {
      Name = form.Name,
      Email = form.Email,
      RoleId = form.RoleId,
    };

    public static EmployeeModel Create(EmployeeEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Email = entity.Email,
        RoleName = entity.Role.RoleName
    };
    public static EmployeeEntity Update(EmployeeUpdateForm form, EmployeeEntity existingEntity) => new()
    {
        Id = form.Id,
        Name = form.Name,
        Email = form.Email,
        RoleId= form.RoleId,
        Role = existingEntity.Role
    };
}
