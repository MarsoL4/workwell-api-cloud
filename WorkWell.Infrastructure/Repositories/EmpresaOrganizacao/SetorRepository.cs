using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;
using WorkWell.Infrastructure.Persistence;
using System.Linq;

namespace WorkWell.Infrastructure.Repositories.EmpresaOrganizacao
{
    public class SetorRepository : ISetorRepository
    {
        private readonly WorkWellDbContext _context;

        public SetorRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<Setor?> GetByIdAsync(long id)
        {
            return await _context.Setores.FindAsync(id);
        }

        public async Task<IEnumerable<Setor>> GetAllAsync()
        {
            return await _context.Setores.ToListAsync();
        }

        public async Task<(IEnumerable<Setor> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize)
        {
            var query = _context.Setores.AsQueryable();
            var total = await query.CountAsync();
            var items = await query
                .OrderBy(s => s.Id)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return (items, total);
        }

        public async Task AddAsync(Setor setor)
        {
            await _context.Setores.AddAsync(setor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Setor setor)
        {
            _context.Setores.Update(setor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var setor = await _context.Setores.FindAsync(id);
            if (setor != null)
            {
                _context.Setores.Remove(setor);
                await _context.SaveChangesAsync();
            }
        }
    }
}