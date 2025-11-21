using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.OmbudMind;
using WorkWell.Domain.Interfaces.OmbudMind;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.OmbudMind
{
    public class DenunciaRepository : IDenunciaRepository
    {
        private readonly WorkWellDbContext _context;

        public DenunciaRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<Denuncia?> GetByIdAsync(long id)
        {
            return await _context.Denuncias.FindAsync(id);
        }

        public async Task<IEnumerable<Denuncia>> GetAllAsync()
        {
            return await _context.Denuncias.ToListAsync();
        }

        public async Task<Denuncia?> GetByCodigoRastreamentoAsync(string codigoRastreamento)
        {
            return await _context.Denuncias.FirstOrDefaultAsync(d => d.CodigoRastreamento == codigoRastreamento);
        }

        public async Task AddAsync(Denuncia denuncia)
        {
            await _context.Denuncias.AddAsync(denuncia);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Denuncia denuncia)
        {
            _context.Denuncias.Update(denuncia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var denuncia = await _context.Denuncias.FindAsync(id);
            if (denuncia != null)
            {
                _context.Denuncias.Remove(denuncia);
                await _context.SaveChangesAsync();
            }
        }
    }
}