using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ServicesFactory
{
    public static ServicesRegistrationForm Create() => new();
    public static ServiceEntity Create(ServicesRegistrationForm form) => new()
    {
        Name = form.Name,
        Price = form.Price,
    };

    public static ServicesModel Create(ServiceEntity entity) => new()
    {
        Name = entity.Name,
        Price = entity.Price,
        Id = entity.Id,
    };

}
