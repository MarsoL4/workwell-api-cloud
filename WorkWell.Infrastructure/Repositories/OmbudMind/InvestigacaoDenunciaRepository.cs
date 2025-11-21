using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.OmbudMind;
using WorkWell.Domain.Interfaces.OmbudMind;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.OmbudMind
{
    public class InvestigacaoDenunciaRepository : IInvestigacaoDenunciaRepository
    {
        private readonly WorkWellDbContext _context;

        public InvestigacaoDenunciaRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<InvestigacaoDenuncia?> GetByIdAsync(long id)
        {
            return await _context.InvestigacoesDenuncia.FindAsync(id);
        }

        public async Task<IEnumerable<InvestigacaoDenuncia>> GetAllByDenunciaIdAsync(long denunciaId)
        {
            return await _context.InvestigacoesDenuncia
                .Where(i => i.DenunciaId == denunciaId)
                .ToListAsync();
        }

        public async Task AddAsync(InvestigacaoDenuncia investigacao)
        {
            await _context.InvestigacoesDenuncia.AddAsync(investigacao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InvestigacaoDenuncia investigacao)
        {
            _context.InvestigacoesDenuncia.Update(investigacao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var investigacao = await _context.InvestigacoesDenuncia.FindAsync(id);
            if (investigacao != null)
            {
                _context.InvestigacoesDenuncia.Remove(investigacao);
                await _context.SaveChangesAsync();
            }
        }
    }
}