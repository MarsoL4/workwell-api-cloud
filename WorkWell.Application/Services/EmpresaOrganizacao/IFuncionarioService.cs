using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.Application.Services.EmpresaOrganizacao
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<FuncionarioDto>> GetAllAsync();
        Task<PagedResultDto<FuncionarioDto>> GetAllPagedAsync(int page, int pageSize); // PAGINAÇÃO
        Task<FuncionarioDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(FuncionarioDto funcionarioDto);
        Task UpdateAsync(FuncionarioDto funcionarioDto);
        Task DeleteAsync(long id);
    }
}