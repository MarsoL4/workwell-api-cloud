using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;

namespace WorkWell.Domain.Interfaces.AvaliacoesEmocionais
{
    public interface IPerfilEmocionalRepository
    {
        Task<PerfilEmocional?> GetByIdAsync(long id);
        Task<IEnumerable<PerfilEmocional>> GetAllAsync();
        Task AddAsync(PerfilEmocional perfil);
        Task UpdateAsync(PerfilEmocional perfil);
        Task DeleteAsync(long id);
    }
}