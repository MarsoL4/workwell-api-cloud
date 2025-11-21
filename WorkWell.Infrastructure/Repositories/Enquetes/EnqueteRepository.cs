using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.Enquetes;
using WorkWell.Domain.Interfaces.Enquetes;
using WorkWell.Infrastructure.Persistence;
using System.Linq;

namespace WorkWell.Infrastructure.Repositories.Enquetes
{
    public class EnqueteRepository : IEnqueteRepository
    {
        private readonly WorkWellDbContext _context;

        public EnqueteRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<Enquete?> GetByIdAsync(long id) =>
            await _context.Enquetes.FindAsync(id);

        public async Task<IEnumerable<Enquete>> GetAllAsync() =>
            await _context.Enquetes.ToListAsync();

        public async Task<(IEnumerable<Enquete> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize)
        {
            var query = _context.Enquetes.AsQueryable();
            var total = await query.CountAsync();
            var items = await query
                .OrderBy(e => e.Id)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return (items, total);
        }

        public async Task AddAsync(Enquete enquete)
        {
            await _context.Enquetes.AddAsync(enquete);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Enquete enquete)
        {
            _context.Enquetes.Update(enquete);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var enquete = await _context.Enquetes.FindAsync(id);
            if (enquete != null)
            {
                _context.Enquetes.Remove(enquete);
                await _context.SaveChangesAsync();
            }
        }
    }
}