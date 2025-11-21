using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.Agenda;

namespace WorkWell.Application.Services.Agenda
{
    public interface IAgendaFuncionarioService
    {
        Task<IEnumerable<AgendaFuncionarioDto>> GetAllAsync();
        Task<AgendaFuncionarioDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(AgendaFuncionarioDto dto);
        Task UpdateAsync(AgendaFuncionarioDto dto);
        Task DeleteAsync(long id);

        // Itens da agenda
        Task<IEnumerable<ItemAgendaDto>> GetItensAsync(long agendaFuncionarioId);
        Task<long> AdicionarItemAsync(long agendaFuncionarioId, ItemAgendaDto dto);

        Task<bool> UpdateItemAsync(long agendaFuncionarioId, ItemAgendaDto dto);
        Task<bool> DeleteItemAsync(long agendaFuncionarioId, long itemId);
    }
}