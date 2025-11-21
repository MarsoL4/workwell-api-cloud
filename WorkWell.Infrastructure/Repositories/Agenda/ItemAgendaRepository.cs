using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.Agenda;
using WorkWell.Domain.Interfaces.Agenda;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.Agenda
{
    public class ItemAgendaRepository : IItemAgendaRepository
    {
        private readonly WorkWellDbContext _context;

        public ItemAgendaRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<ItemAgenda?> GetByIdAsync(long id)
        {
            return await _context.ItensAgenda.FindAsync(id);
        }

        public async Task<IEnumerable<ItemAgenda>> GetAllByAgendaIdAsync(long agendaFuncionarioId)
        {
            return await _context.ItensAgenda
                .Where(i => i.AgendaFuncionarioId == agendaFuncionarioId)
                .ToListAsync();
        }

        public async Task AddAsync(ItemAgenda item)
        {
            await _context.ItensAgenda.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ItemAgenda item)
        {
            _context.ItensAgenda.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _context.ItensAgenda.FindAsync(id);
            if (item != null)
            {
                _context.ItensAgenda.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}