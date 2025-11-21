using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.Notificacoes;
using WorkWell.Domain.Interfaces.Notificacoes;
using WorkWell.Infrastructure.Persistence;
using System.Linq;

namespace WorkWell.Infrastructure.Repositories.Notificacoes
{
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly WorkWellDbContext _context;

        public NotificacaoRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<Notificacao?> GetByIdAsync(long id)
        {
            return await _context.Notificacoes.FindAsync(id);
        }

        public async Task<IEnumerable<Notificacao>> GetAllAsync()
        {
            return await _context.Notificacoes.ToListAsync();
        }

        public async Task<(IEnumerable<Notificacao> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize)
        {
            var query = _context.Notificacoes.AsQueryable();
            var total = await query.CountAsync();
            var items = await query
                .OrderBy(n => n.Id)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return (items, total);
        }

        public async Task<IEnumerable<Notificacao>> GetAllByFuncionarioIdAsync(long funcionarioId)
        {
            return await _context.Notificacoes
                .Where(n => n.FuncionarioId == funcionarioId)
                .ToListAsync();
        }

        public async Task AddAsync(Notificacao notificacao)
        {
            await _context.Notificacoes.AddAsync(notificacao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Notificacao notificacao)
        {
            _context.Notificacoes.Update(notificacao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var notificacao = await _context.Notificacoes.FindAsync(id);
            if (notificacao != null)
            {
                _context.Notificacoes.Remove(notificacao);
                await _context.SaveChangesAsync();
            }
        }
    }
}