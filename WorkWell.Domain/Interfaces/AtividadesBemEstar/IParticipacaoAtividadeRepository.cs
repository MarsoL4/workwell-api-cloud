using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.AtividadesBemEstar;

namespace WorkWell.Domain.Interfaces.AtividadesBemEstar
{
    public interface IParticipacaoAtividadeRepository
    {
        Task<ParticipacaoAtividade?> GetByIdAsync(long id);
        Task<IEnumerable<ParticipacaoAtividade>> GetAllByAtividadeIdAsync(long atividadeId);
        Task AddAsync(ParticipacaoAtividade participacao);
        Task UpdateAsync(ParticipacaoAtividade participacao);
        Task DeleteAsync(long id);
    }
}