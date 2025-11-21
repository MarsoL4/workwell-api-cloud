using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.Application.Services.EmpresaOrganizacao
{
    public interface IEmpresaService
    {
        Task<IEnumerable<EmpresaDto>> GetAllAsync();
        Task<PagedResultDto<EmpresaDto>> GetAllPagedAsync(int page, int pageSize); // NOVO
        Task<EmpresaDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(EmpresaDto empresaDto);
        Task UpdateAsync(EmpresaDto empresaDto);
        Task DeleteAsync(long id);
    }
}