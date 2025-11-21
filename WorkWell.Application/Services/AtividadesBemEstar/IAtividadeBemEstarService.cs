using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.AtividadesBemEstar;

namespace WorkWell.Application.Services.AtividadesBemEstar
{
    public interface IAtividadeBemEstarService
    {
        Task<IEnumerable<AtividadeBemEstarDto>> GetAllAsync();
        Task<AtividadeBemEstarDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(AtividadeBemEstarDto dto);
        Task UpdateAsync(AtividadeBemEstarDto dto);
        Task DeleteAsync(long id);

        // Participações
        Task<IEnumerable<ParticipacaoAtividadeDto>> GetParticipacoesAsync(long atividadeId);
        Task<long> AdicionarParticipacaoAsync(long atividadeId, ParticipacaoAtividadeDto dto);

        Task<bool> UpdateParticipacaoAsync(long atividadeId, ParticipacaoAtividadeDto dto);
        Task<bool> DeleteParticipacaoAsync(long atividadeId, long participacaoId);
    }
}