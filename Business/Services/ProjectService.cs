using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<ProjectModel> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var entity = await _projectRepository.GetAsync(x => x.Title == form.Title);
        entity ??= await _projectRepository.CreateAsync(ProjectFactory.Create(form));

        return ProjectFactory.Create(entity);
    }

    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(ProjectFactory.Create);
        return projects;
    }
}
