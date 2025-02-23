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
        await _rolesRepository.BeginTransactionAsync();
        try
        {
            var existingRole = await _rolesRepository.GetAsync(x => x.RoleName == form.RoleName);
            if (existingRole != null)
            {
                await _rolesRepository.RollbackTransactionAsync();
                return null!;
            }

            var entity = await _rolesRepository.CreateAsync(RolesFactory.Create(form));

            if (entity == null)
            {
                await _rolesRepository.RollbackTransactionAsync();
                return null!;
            }
            var role = RolesFactory.Create(entity);
            if (role == null)
            {
                await _rolesRepository.RollbackTransactionAsync();
                return null!;
            }

            await _rolesRepository.CommitTransactionAsync();
            return role;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _rolesRepository.RollbackTransactionAsync();
            return null!;
        }
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

            var updatedEntity = RolesFactory.Update(form, existingEntity);
            updatedEntity = await _rolesRepository.UpdateAsync(x => x.Id == form.Id, updatedEntity);
            if (updatedEntity == null) 
                return null!;

            return RolesFactory.Create(updatedEntity);
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
