using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;

namespace WorkWell.Application.Services.AvaliacoesEmocionais
{
    public interface IRiscoPsicossocialService
    {
        Task<IEnumerable<RiscoPsicossocialDto>> GetAllAsync();
        Task<RiscoPsicossocialDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(RiscoPsicossocialDto dto);
        Task UpdateAsync(RiscoPsicossocialDto dto);
        Task DeleteAsync(long id);
    }
}