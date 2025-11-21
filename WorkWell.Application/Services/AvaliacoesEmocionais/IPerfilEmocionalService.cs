using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;

namespace WorkWell.Application.Services.AvaliacoesEmocionais
{
    public interface IPerfilEmocionalService
    {
        Task<IEnumerable<PerfilEmocionalDto>> GetAllAsync();
        Task<PerfilEmocionalDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(PerfilEmocionalDto dto);
        Task UpdateAsync(PerfilEmocionalDto dto);
        Task DeleteAsync(long id);
    }
}