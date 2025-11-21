using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.OmbudMind;

namespace WorkWell.Application.Services.OmbudMind
{
    public interface IDenunciaService
    {
        Task<IEnumerable<DenunciaDto>> GetAllAsync();
        Task<DenunciaDto?> GetByIdAsync(long id);
        Task<DenunciaDto?> GetByCodigoRastreamentoAsync(string codigo);
        Task<long> CreateAsync(DenunciaDto dto);
        Task UpdateAsync(DenunciaDto dto);
        Task DeleteAsync(long id);

        // Investigação
        Task<IEnumerable<InvestigacaoDenunciaDto>> GetInvestigacoesAsync(long denunciaId);
        Task<long> AdicionarInvestigacaoAsync(long denunciaId, InvestigacaoDenunciaDto dto);

        Task<bool> UpdateInvestigacaoAsync(long denunciaId, InvestigacaoDenunciaDto dto);
        Task<bool> DeleteInvestigacaoAsync(long denunciaId, long investigacaoId);
    }
}