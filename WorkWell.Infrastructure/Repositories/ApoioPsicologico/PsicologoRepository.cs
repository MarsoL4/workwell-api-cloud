using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;
using WorkWell.Infrastructure.Persistence;
using System.Linq;

namespace WorkWell.Infrastructure.Repositories.ApoioPsicologico
{
    public class PsicologoRepository : IPsicologoRepository
    {
        private readonly WorkWellDbContext _context;

        public PsicologoRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<Psicologo?> GetByIdAsync(long id)
        {
            return await _context.Psicologos.FindAsync(id);
        }

        public async Task<IEnumerable<Psicologo>> GetAllAsync()
        {
            return await _context.Psicologos.ToListAsync();
        }

        public async Task<(IEnumerable<Psicologo> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize)
        {
            var query = _context.Psicologos.AsQueryable();
            var total = await query.CountAsync();
            var items = await query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return (items, total);
        }

        public async Task AddAsync(Psicologo psicologo)
        {
            await _context.Psicologos.AddAsync(psicologo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Psicologo psicologo)
        {
            _context.Psicologos.Update(psicologo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var psicologo = await _context.Psicologos.FindAsync(id);
            if (psicologo != null)
            {
                _context.Psicologos.Remove(psicologo);
                await _context.SaveChangesAsync();
            }
        }
    }
}