using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        return await _context.Projects
            .Include(p => p.Service)
                .ThenInclude(u => u.Unit)
            .Include(p => p.Status)
            .Include(p => p.Customer)
                .ThenInclude(c => c.CustomerContacts)
            .Include(p => p.Employee)
                .ThenInclude(e => e.Role)
            .ToListAsync();
    }
}
