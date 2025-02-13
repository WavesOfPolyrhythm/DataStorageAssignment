using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class RolesRepository(DataContext context) : BaseRepository<RoleEntity>(context), IRolesRepository
{
    private readonly DataContext _context = context;
}
