using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.Indicadores;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.Application.Services.Indicadores
{
    public interface IIndicadoresEmpresaService
    {
        Task<IEnumerable<IndicadoresEmpresaDto>> GetAllAsync();
        Task<PagedResultDto<IndicadoresEmpresaDto>> GetAllPagedAsync(int page, int pageSize); // PAGINAÇÃO
        Task<IndicadoresEmpresaDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(IndicadoresEmpresaDto dto);
        Task UpdateAsync(IndicadoresEmpresaDto dto);
        Task DeleteAsync(long id);
    }
}