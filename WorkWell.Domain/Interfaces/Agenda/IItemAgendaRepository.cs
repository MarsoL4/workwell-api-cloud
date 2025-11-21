using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.Agenda;

namespace WorkWell.Domain.Interfaces.Agenda
{
    public interface IItemAgendaRepository
    {
        Task<ItemAgenda?> GetByIdAsync(long id);
        Task<IEnumerable<ItemAgenda>> GetAllByAgendaIdAsync(long agendaFuncionarioId);
        Task AddAsync(ItemAgenda item);
        Task UpdateAsync(ItemAgenda item);
        Task DeleteAsync(long id);
    }
}