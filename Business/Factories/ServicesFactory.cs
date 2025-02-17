using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ServicesFactory
{
    public static ServicesRegistrationForm Create() => new();
    public static ServicesUpdateForm Update() => new();
    public static ServiceEntity Create(ServicesRegistrationForm form) => new()
    {
        Name = form.Name,
        Price = form.Price,
        UnitId = form.UnitId
    };

    public static ServicesModel Create(ServiceEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Price = entity.Price,
        UnitName = entity.Unit.Name,
    };

    public static ServiceEntity Update(ServicesUpdateForm form) => new()
    {
        Id = form.Id,
        Name = form.Name,
        Price = form.Price ?? 0,
        UnitId = form.UnitId,
    };

}
