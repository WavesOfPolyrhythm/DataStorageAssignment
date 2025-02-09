using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<ProjectModel> CreateProjectAsync(ProjectRegistrationForm form);
    Task<IEnumerable<ProjectModel>> GetAllProjectsAsync();
}