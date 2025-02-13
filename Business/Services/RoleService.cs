using Business.Interfaces;
using Data.Interfaces;

namespace Business.Services;

public class RoleService(IRolesRepository rolesRepository) : IRoleService
{
    private readonly IRolesRepository _rolesRepository = rolesRepository;
}
