using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ServiceRepository(DataContext context) : BaseRepository<ServiceEntity>(context), IServiceRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<ServiceEntity>> GetAllAsync()
    {
        return await _context.Services
            .Include(s => s.Unit)
            .ToListAsync();
    }
}
