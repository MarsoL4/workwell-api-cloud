using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.Agenda;

namespace WorkWell.Domain.Interfaces.Agenda
{
    public interface IAgendaFuncionarioRepository
    {
        Task<AgendaFuncionario?> GetByIdAsync(long id);
        Task<IEnumerable<AgendaFuncionario>> GetAllAsync();
        Task AddAsync(AgendaFuncionario agenda);
        Task UpdateAsync(AgendaFuncionario agenda);
        Task DeleteAsync(long id);
    }
}