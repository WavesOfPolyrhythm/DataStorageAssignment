using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create() => new();


    public static ProjectEntity Create(ProjectRegistrationForm form) => new()
    {
        Title = form.Title,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        EmployeeId = form.EmployeeId,
        TotalPrice = form.TotalPrice,
        CustomerId = form.CustomerId,
        ServiceId = form.ServiceId,
        StatusId = form.StatusId
    };

    public static ProjectModel Create(ProjectEntity entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
    };
 
}
