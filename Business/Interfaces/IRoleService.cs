using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IRoleService
{
    Task<RolesModel> CreateRolesAsync(RolesRegistrationForm form);
    Task<IEnumerable<RolesModel>> GetAllRolesAsync();
    Task<RoleEntity?> GetRoleEntityAsync(Expression<Func<RoleEntity, bool>> expression);
    Task<RolesModel?> UpdateRolesAsync(RolesUpdateForm form);
    Task<bool> DeleteRoleAsync(int id);
}