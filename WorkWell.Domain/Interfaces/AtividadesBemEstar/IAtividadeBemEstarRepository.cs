using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.AtividadesBemEstar;

namespace WorkWell.Domain.Interfaces.AtividadesBemEstar
{
    public interface IAtividadeBemEstarRepository
    {
        Task<AtividadeBemEstar?> GetByIdAsync(long id);
        Task<IEnumerable<AtividadeBemEstar>> GetAllAsync();
        Task AddAsync(AtividadeBemEstar atividade);
        Task UpdateAsync(AtividadeBemEstar atividade);
        Task DeleteAsync(long id);
    }
}