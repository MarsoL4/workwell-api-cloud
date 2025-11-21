using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.Enquetes;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.Application.Services.Enquetes
{
    public interface IEnqueteService
    {
        Task<IEnumerable<EnqueteDto>> GetAllAsync();
        Task<PagedResultDto<EnqueteDto>> GetAllPagedAsync(int page, int pageSize);
        Task<EnqueteDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(EnqueteDto dto);
        Task UpdateAsync(EnqueteDto dto);
        Task DeleteAsync(long id);

        Task<IEnumerable<RespostaEnqueteDto>> GetRespostasAsync(long enqueteId);
        Task<long> AdicionarRespostaAsync(long enqueteId, RespostaEnqueteDto dto);

        Task<bool> UpdateRespostaAsync(long enqueteId, RespostaEnqueteDto dto);
        Task<bool> DeleteRespostaAsync(long enqueteId, long respostaId);
    }
}