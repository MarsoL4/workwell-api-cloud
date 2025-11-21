using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.AvaliacoesEmocionais
{
    public class AvaliacaoProfundaRepository : IAvaliacaoProfundaRepository
    {
        private readonly WorkWellDbContext _context;
        public AvaliacaoProfundaRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<AvaliacaoProfunda?> GetByIdAsync(long id)
            => await _context.AvaliacoesProfundas.FindAsync(id);

        public async Task<IEnumerable<AvaliacaoProfunda>> GetAllAsync()
            => await _context.AvaliacoesProfundas.ToListAsync();

        public async Task AddAsync(AvaliacaoProfunda avaliacao)
        {
            await _context.AvaliacoesProfundas.AddAsync(avaliacao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AvaliacaoProfunda avaliacao)
        {
            _context.AvaliacoesProfundas.Update(avaliacao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _context.AvaliacoesProfundas.FindAsync(id);
            if (entity != null)
            {
                _context.AvaliacoesProfundas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}