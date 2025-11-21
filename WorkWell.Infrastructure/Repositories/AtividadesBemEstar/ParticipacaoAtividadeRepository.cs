using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.AtividadesBemEstar;
using WorkWell.Domain.Interfaces.AtividadesBemEstar;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.AtividadesBemEstar
{
    public class ParticipacaoAtividadeRepository : IParticipacaoAtividadeRepository
    {
        private readonly WorkWellDbContext _context;
        public ParticipacaoAtividadeRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<ParticipacaoAtividade?> GetByIdAsync(long id)
        {
            return await _context.ParticipacoesAtividade.FindAsync(id);
        }

        public async Task<IEnumerable<ParticipacaoAtividade>> GetAllByAtividadeIdAsync(long atividadeId)
        {
            return await _context.ParticipacoesAtividade
                .Where(p => p.AtividadeId == atividadeId).ToListAsync();
        }

        public async Task AddAsync(ParticipacaoAtividade participacao)
        {
            await _context.ParticipacoesAtividade.AddAsync(participacao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ParticipacaoAtividade participacao)
        {
            _context.ParticipacoesAtividade.Update(participacao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var part = await _context.ParticipacoesAtividade.FindAsync(id);
            if (part != null)
            {
                _context.ParticipacoesAtividade.Remove(part);
                await _context.SaveChangesAsync();
            }
        }
    }
}