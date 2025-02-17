using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class RolesFactory
{
    public static RolesRegistrationForm Create() => new();
    public static RolesUpdateForm Update() => new();

    public static RoleEntity Create(RolesRegistrationForm form) => new()
    {
        RoleName = form.RoleName,
    };

    public static RolesModel Create(RoleEntity entity) => new()
    {
        Id = entity.Id,
        RoleName = entity.RoleName,
    };

    public static RolesUpdateForm Create(RolesUpdateForm form) => new()
    {
        Id = form.Id,
        RoleName = form.RoleName,
    };
}
