using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerRegistrationForm Create() => new();
    public static CustomerUpdateForm Update() => new();

    public static CustomerEntity Create(CustomerRegistrationForm form) => new()
    {
        CustomerName = form.CustomerName,
    };

    public static CustomerModel Create(CustomerEntity entity) => new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName,
    };

    public static CustomerEntity Update(CustomerUpdateForm form, CustomerEntity existingEntity) => new()
    {
        Id = form.Id,
        CustomerName = string.IsNullOrWhiteSpace(form.CustomerName) ? existingEntity.CustomerName : form.CustomerName,
    };
}
