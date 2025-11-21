using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.ApoioPsicologico;

namespace WorkWell.Domain.Interfaces.ApoioPsicologico
{
    public interface IChatAnonimoRepository
    {
        Task<ChatAnonimo?> GetByIdAsync(long id);
        Task<IEnumerable<ChatAnonimo>> GetAllAsync();
        Task AddAsync(ChatAnonimo chat);
        Task UpdateAsync(ChatAnonimo chat);
        Task DeleteAsync(long id);
    }
}