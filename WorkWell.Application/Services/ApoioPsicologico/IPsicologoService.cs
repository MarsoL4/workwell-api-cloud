using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.Application.Services.ApoioPsicologico
{
    public interface IPsicologoService
    {
        Task<IEnumerable<PsicologoDto>> GetAllAsync();
        Task<PagedResultDto<PsicologoDto>> GetAllPagedAsync(int page, int pageSize); // NOVO
        Task<PsicologoDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(PsicologoDto dto);
        Task UpdateAsync(PsicologoDto dto);
        Task DeleteAsync(long id);
    }
}