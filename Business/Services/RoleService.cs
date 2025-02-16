using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class RoleService(IRolesRepository rolesRepository) : IRoleService
{
    private readonly IRolesRepository _rolesRepository = rolesRepository;

    public async Task<RolesModel> CreateRolesAsync(RolesRegistrationForm form)
    {
        var existingRole = await _rolesRepository.GetAsync(x => x.RoleName == form.RoleName);
        if (existingRole != null)
            return null!;

        var entity = await _rolesRepository.CreateAsync(RolesFactory.Create(form));
        if (entity == null)
            return null!;

        return RolesFactory.Create(entity);
    }

    public async Task<IEnumerable<RolesModel>> GetAllRolesAsync()
    {
        var entities = await _rolesRepository.GetAllAsync();
        return entities.Select(RolesFactory.Create);
    }

    public async Task<RoleEntity?> GetRoleEntityAsync(Expression<Func<RoleEntity, bool>> expression)
    {
        var roles = await _rolesRepository.GetAsync(expression);
        return roles;
    }

    public async Task<RolesModel?> UpdateRolesAsync(RolesUpdateForm form)
    {
        try
        {
            var existingEntity = await GetRoleEntityAsync(x => x.Id == form.Id);

            if (existingEntity == null)
                return null!;

            existingEntity.RoleName = string.IsNullOrWhiteSpace(form.RoleName) ? existingEntity.RoleName : form.RoleName;

            var updatedEntity = await _rolesRepository.UpdateAsync(x => x.Id == form.Id, existingEntity);
            return RolesFactory.Create(existingEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<bool> DeleteRoleAsync(int id)
    {
        var result = await _rolesRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
