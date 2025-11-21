using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.AvaliacoesEmocionais
{
    public class MoodCheckRepository : IMoodCheckRepository
    {
        private readonly WorkWellDbContext _context;
        public MoodCheckRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<MoodCheck?> GetByIdAsync(long id)
            => await _context.MoodChecks.FindAsync(id);

        public async Task<IEnumerable<MoodCheck>> GetAllAsync()
            => await _context.MoodChecks.ToListAsync();

        public async Task AddAsync(MoodCheck moodCheck)
        {
            await _context.MoodChecks.AddAsync(moodCheck);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MoodCheck moodCheck)
        {
            _context.MoodChecks.Update(moodCheck);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _context.MoodChecks.FindAsync(id);
            if (entity != null)
            {
                _context.MoodChecks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}