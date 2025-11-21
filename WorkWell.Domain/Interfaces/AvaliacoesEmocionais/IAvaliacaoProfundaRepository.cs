using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;

namespace WorkWell.Domain.Interfaces.AvaliacoesEmocionais
{
    public interface IAvaliacaoProfundaRepository
    {
        Task<AvaliacaoProfunda?> GetByIdAsync(long id);
        Task<IEnumerable<AvaliacaoProfunda>> GetAllAsync();
        Task AddAsync(AvaliacaoProfunda avaliacao);
        Task UpdateAsync(AvaliacaoProfunda avaliacao);
        Task DeleteAsync(long id);
    }
}