using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;

namespace WorkWell.Domain.Interfaces.AvaliacoesEmocionais
{
    public interface IRiscoPsicossocialRepository
    {
        Task<RiscoPsicossocial?> GetByIdAsync(long id);
        Task<IEnumerable<RiscoPsicossocial>> GetAllAsync();
        Task AddAsync(RiscoPsicossocial risco);
        Task UpdateAsync(RiscoPsicossocial risco);
        Task DeleteAsync(long id);
    }
}