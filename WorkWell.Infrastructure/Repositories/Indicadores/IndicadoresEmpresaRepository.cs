using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.Indicadores;
using WorkWell.Domain.Interfaces.Indicadores;
using WorkWell.Infrastructure.Persistence;
using System.Linq;

namespace WorkWell.Infrastructure.Repositories.Indicadores
{
    public class IndicadoresEmpresaRepository : IIndicadoresEmpresaRepository
    {
        private readonly WorkWellDbContext _context;

        public IndicadoresEmpresaRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<IndicadoresEmpresa?> GetByIdAsync(long id)
        {
            return await _context.IndicadoresEmpresa
                .Include(x => x.AdesaoPorSetor)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<IndicadoresEmpresa>> GetAllAsync()
        {
            return await _context.IndicadoresEmpresa
                .Include(x => x.AdesaoPorSetor)
                .ToListAsync();
        }

        public async Task<(IEnumerable<IndicadoresEmpresa> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize)
        {
            var query = _context.IndicadoresEmpresa.Include(x => x.AdesaoPorSetor).AsQueryable();
            var total = await query.CountAsync();
            var items = await query
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return (items, total);
        }

        public async Task AddAsync(IndicadoresEmpresa indicadores)
        {
            await _context.IndicadoresEmpresa.AddAsync(indicadores);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IndicadoresEmpresa indicadores)
        {
            _context.IndicadoresEmpresa.Update(indicadores);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _context.IndicadoresEmpresa.FindAsync(id);
            if (entity != null)
            {
                _context.IndicadoresEmpresa.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}