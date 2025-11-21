using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.AvaliacoesEmocionais
{
    public class PerfilEmocionalRepository : IPerfilEmocionalRepository
    {
        private readonly WorkWellDbContext _context;
        public PerfilEmocionalRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<PerfilEmocional?> GetByIdAsync(long id)
            => await _context.PerfisEmocionais.FindAsync(id);

        public async Task<IEnumerable<PerfilEmocional>> GetAllAsync()
            => await _context.PerfisEmocionais.ToListAsync();

        public async Task AddAsync(PerfilEmocional perfil)
        {
            await _context.PerfisEmocionais.AddAsync(perfil);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PerfilEmocional perfil)
        {
            _context.PerfisEmocionais.Update(perfil);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var perfil = await _context.PerfisEmocionais.FindAsync(id);
            if (perfil != null)
            {
                _context.PerfisEmocionais.Remove(perfil);
                await _context.SaveChangesAsync();
            }
        }
    }
}