using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

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
}
