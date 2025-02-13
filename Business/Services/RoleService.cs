using Data.Interfaces;

namespace Business.Services;

public class RoleService(IRolesRepository rolesRepository)
{
    private readonly IRolesRepository _rolesRepository = rolesRepository;
}
