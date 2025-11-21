using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.OmbudMind;

namespace WorkWell.Domain.Interfaces.OmbudMind
{
    public interface IDenunciaRepository
    {
        Task<Denuncia?> GetByIdAsync(long id);
        Task<IEnumerable<Denuncia>> GetAllAsync();
        Task<Denuncia?> GetByCodigoRastreamentoAsync(string codigoRastreamento);
        Task AddAsync(Denuncia denuncia);
        Task UpdateAsync(Denuncia denuncia);
        Task DeleteAsync(long id);
    }
}