using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, ICustomerRepository customerRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<ProjectModel> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var existingProject = await _projectRepository.GetAsync(x => x.Title == form.Title);
        if (existingProject != null)
            return null!;

        // Hämta befintlig kund från databasen istället för att skapa en ny
        var customer = await _customerRepository.GetAsync(x => x.Id == form.CustomerId);
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
}
