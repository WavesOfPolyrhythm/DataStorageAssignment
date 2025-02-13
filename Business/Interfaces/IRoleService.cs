using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IRoleService
{
    Task<RolesModel> CreateRolesAsync(RolesRegistrationForm form);
    Task<IEnumerable<RolesModel>> GetAllRolesAsync();
}