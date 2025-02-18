﻿using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, ICustomerService customerService, IEmployeeService employeeService, IServicesService servicesService, IStatusService statusService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICustomerService _customerService = customerService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IServicesService _servicesService = servicesService;
    private readonly IStatusService _statusService = statusService;

    public async Task<ProjectModel> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var existingProject = await _projectRepository.GetAsync(x => x.Title == form.Title);
        if (existingProject != null)
            return null!;

        var customer = await _customerService.GetCustomerEntityAsync(x => x.Id == form.CustomerId);
        if (customer == null)
        {
            Console.WriteLine("\n Customer not found. Returning to menu.");
            return null!;
        }

        var entity = await _projectRepository.CreateAsync(ProjectFactory.Create(form));

        if (entity == null)
            return null!;
        
        var project = ProjectFactory.Create(entity);

        return project ?? null!;
    }

    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(ProjectFactory.Create);
        return projects;
    }

    public async Task<ProjectEntity?> GetProjectEntityAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var project = await _projectRepository.GetAsync(expression);
        return project;
    }

    public async Task<ProjectModel?> UpdateProjectAsync(ProjectUpdateForm form)
    {
        try
        {
            var existingEntity = await GetProjectEntityAsync(x => x.Id == form.Id);

            if (existingEntity == null)
                return null!;

            //StartDate and EndDate, Code from Chat Gpt
            existingEntity.Title = string.IsNullOrWhiteSpace(form.Title) ? existingEntity.Title : form.Title;
            existingEntity.Description = string.IsNullOrWhiteSpace(form.Description) ? existingEntity.Description : form.Description;
            existingEntity.StartDate = form.StartDate != default ? form.StartDate : existingEntity.StartDate;
            existingEntity.EndDate = form.EndDate != default ? form.EndDate : existingEntity.EndDate;
            existingEntity.TotalPrice = form.TotalPrice ?? existingEntity.TotalPrice;


            var updatedEntity = await _projectRepository.UpdateAsync(x => x.Id == form.Id, existingEntity);
            return ProjectFactory.Create(existingEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var result = await _projectRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
