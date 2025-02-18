using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerContactRepository(DataContext context) : BaseRepository<CustomerContactEntity>(context), ICustomerContactRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<CustomerContactEntity>> GetAllAsync()
    {
        return await _context.CustomerContacts
            .Include(c => c.Customer)
            .ToListAsync();
    }
}
