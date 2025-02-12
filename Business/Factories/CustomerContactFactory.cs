using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerContactFactory
{
    public static CustomerContactRegistrationForm Create() => new();

    public static CustomerContactEntity Create(CustomerContactRegistrationForm form) => new()
    {
        Name = form.Name,
        PhoneNumber = form.PhoneNumber,
        Email = form.Email,
        CustomerId = form.CustomerId,
    };

    public static CustomerContactModel Create(CustomerContactEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        PhoneNumber = entity.PhoneNumber,
        Email = entity.Email,
        CustomerId = entity.CustomerId
    };
}
