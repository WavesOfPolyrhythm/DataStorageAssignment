using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class UnitFactory
{
    public static UnitRegistrationForm Create() => new();
    public static UnitUpdateForm Update() => new();
    public static UnitEntity Create(UnitRegistrationForm form) => new()
    {
        Name = form.Name,
        Description = form.Description,
    };

    public static UnitModel Create(UnitEntity entity) => 
        new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Description = entity.Description,
    };

    public static UnitEntity Update(UnitUpdateForm form, UnitEntity existingEntity) => new()
    {
        Id = form.Id,
        Name = string.IsNullOrWhiteSpace(form.Name) ? existingEntity.Name : form.Name,
        Description = string.IsNullOrWhiteSpace(form.Description) ? existingEntity.Description : form.Description,
    };

}
