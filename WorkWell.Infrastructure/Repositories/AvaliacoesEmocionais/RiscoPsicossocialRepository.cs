using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.AvaliacoesEmocionais
{
    public class RiscoPsicossocialRepository : IRiscoPsicossocialRepository
    {
        private readonly WorkWellDbContext _context;
        public RiscoPsicossocialRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<RiscoPsicossocial?> GetByIdAsync(long id)
            => await _context.RiscosPsicossociais.FindAsync(id);

        public async Task<IEnumerable<RiscoPsicossocial>> GetAllAsync()
            => await _context.RiscosPsicossociais.ToListAsync();

        public async Task AddAsync(RiscoPsicossocial risco)
        {
            await _context.RiscosPsicossociais.AddAsync(risco);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RiscoPsicossocial risco)
        {
            _context.RiscosPsicossociais.Update(risco);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _context.RiscosPsicossociais.FindAsync(id);
            if (entity != null)
            {
                _context.RiscosPsicossociais.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}