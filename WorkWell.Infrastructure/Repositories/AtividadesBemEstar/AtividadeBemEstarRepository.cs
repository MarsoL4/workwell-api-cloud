using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.AtividadesBemEstar;
using WorkWell.Domain.Interfaces.AtividadesBemEstar;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.AtividadesBemEstar
{
    public class AtividadeBemEstarRepository : IAtividadeBemEstarRepository
    {
        private readonly WorkWellDbContext _context;
        public AtividadeBemEstarRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<AtividadeBemEstar?> GetByIdAsync(long id)
        {
            return await _context.AtividadesBemEstar.FindAsync(id);
        }

        public async Task<IEnumerable<AtividadeBemEstar>> GetAllAsync()
        {
            return await _context.AtividadesBemEstar.ToListAsync();
        }

        public async Task AddAsync(AtividadeBemEstar atividade)
        {
            await _context.AtividadesBemEstar.AddAsync(atividade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AtividadeBemEstar atividade)
        {
            _context.AtividadesBemEstar.Update(atividade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var atividade = await _context.AtividadesBemEstar.FindAsync(id);
            if (atividade != null)
            {
                _context.AtividadesBemEstar.Remove(atividade);
                await _context.SaveChangesAsync();
            }
        }
    }
}