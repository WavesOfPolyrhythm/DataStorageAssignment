using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class StatusFactory
{
    public static StatusRegistrationForm Create() => new();
    public static StatusUpdateForm UpdateForm() => new();
    public static StatusEntity Create(StatusRegistrationForm form) => new()
    {
        StatusName = form.StatusName,
    };

    public static StatusModel Create(StatusEntity entity) => new()
    {
        Id = entity.Id,
        StatusName = entity.StatusName,
    };

    public static StatusEntity Update(StatusUpdateForm form) => new()
    {
        Id = form.Id,
        StatusName = form.StatusName,
    };

}
