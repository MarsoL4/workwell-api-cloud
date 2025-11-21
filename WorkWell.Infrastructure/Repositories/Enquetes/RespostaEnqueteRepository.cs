using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.Enquetes;
using WorkWell.Domain.Interfaces.Enquetes;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.Enquetes
{
    public class RespostaEnqueteRepository : IRespostaEnqueteRepository
    {
        private readonly WorkWellDbContext _context;

        public RespostaEnqueteRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<RespostaEnquete?> GetByIdAsync(long id) =>
            await _context.RespostasEnquete.FindAsync(id);

        public async Task<IEnumerable<RespostaEnquete>> GetAllByEnqueteIdAsync(long enqueteId) =>
            await _context.RespostasEnquete
                .Where(r => r.EnqueteId == enqueteId)
                .ToListAsync();

        public async Task AddAsync(RespostaEnquete resposta)
        {
            await _context.RespostasEnquete.AddAsync(resposta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RespostaEnquete resposta)
        {
            _context.RespostasEnquete.Update(resposta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var resposta = await _context.RespostasEnquete.FindAsync(id);
            if (resposta != null)
            {
                _context.RespostasEnquete.Remove(resposta);
                await _context.SaveChangesAsync();
            }
        }
    }
}