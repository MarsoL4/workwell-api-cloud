using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;

namespace WorkWell.Application.Services.AvaliacoesEmocionais
{
    public interface IAvaliacaoProfundaService
    {
        Task<IEnumerable<AvaliacaoProfundaDto>> GetAllAsync();
        Task<AvaliacaoProfundaDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(AvaliacaoProfundaDto dto);
        Task UpdateAsync(AvaliacaoProfundaDto dto);
        Task DeleteAsync(long id);
    }
}