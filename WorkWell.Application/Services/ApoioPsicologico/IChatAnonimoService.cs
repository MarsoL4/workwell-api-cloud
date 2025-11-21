using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.ApoioPsicologico;

namespace WorkWell.Application.Services.ApoioPsicologico
{
    public interface IChatAnonimoService
    {
        Task<IEnumerable<ChatAnonimoDto>> GetAllAsync();
        Task<ChatAnonimoDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(ChatAnonimoDto dto);
        Task UpdateAsync(ChatAnonimoDto dto);
        Task DeleteAsync(long id);
    }
}