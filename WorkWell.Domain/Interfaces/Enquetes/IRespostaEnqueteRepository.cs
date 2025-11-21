using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.Enquetes;

namespace WorkWell.Domain.Interfaces.Enquetes
{
    public interface IRespostaEnqueteRepository
    {
        Task<RespostaEnquete?> GetByIdAsync(long id);
        Task<IEnumerable<RespostaEnquete>> GetAllByEnqueteIdAsync(long enqueteId);
        Task AddAsync(RespostaEnquete resposta);
        Task UpdateAsync(RespostaEnquete resposta);
        Task DeleteAsync(long id);
    }
}