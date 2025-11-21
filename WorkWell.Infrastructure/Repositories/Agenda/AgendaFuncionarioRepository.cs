using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.Agenda;
using WorkWell.Domain.Interfaces.Agenda;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.Agenda
{
    public class AgendaFuncionarioRepository : IAgendaFuncionarioRepository
    {
        private readonly WorkWellDbContext _context;

        public AgendaFuncionarioRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<AgendaFuncionario?> GetByIdAsync(long id)
        {
            return await _context.AgendasFuncionarios
                .Include(a => a.Itens)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<AgendaFuncionario>> GetAllAsync()
        {
            return await _context.AgendasFuncionarios
                .Include(a => a.Itens)
                .ToListAsync();
        }

        public async Task AddAsync(AgendaFuncionario agenda)
        {
            await _context.AgendasFuncionarios.AddAsync(agenda);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AgendaFuncionario agenda)
        {
            _context.AgendasFuncionarios.Update(agenda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var agenda = await _context.AgendasFuncionarios.FindAsync(id);
            if (agenda != null)
            {
                _context.AgendasFuncionarios.Remove(agenda);
                await _context.SaveChangesAsync();
            }
        }
    }
}