using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;

namespace WorkWell.Application.Services.AvaliacoesEmocionais
{
    public interface IMoodCheckService
    {
        Task<IEnumerable<MoodCheckDto>> GetAllAsync();
        Task<MoodCheckDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(MoodCheckDto dto);
        Task UpdateAsync(MoodCheckDto dto);
        Task DeleteAsync(long id);
    }
}