using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.OmbudMind;

namespace WorkWell.Domain.Interfaces.OmbudMind
{
    public interface IInvestigacaoDenunciaRepository
    {
        Task<InvestigacaoDenuncia?> GetByIdAsync(long id);
        Task<IEnumerable<InvestigacaoDenuncia>> GetAllByDenunciaIdAsync(long denunciaId);
        Task AddAsync(InvestigacaoDenuncia investigacao);
        Task UpdateAsync(InvestigacaoDenuncia investigacao);
        Task DeleteAsync(long id);
    }
}