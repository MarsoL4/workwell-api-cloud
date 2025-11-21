using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Interfaces.EmpresaOrganizacao
{
    public interface IFuncionarioRepository
    {
        Task<Funcionario?> GetByIdAsync(long id);
        Task<IEnumerable<Funcionario>> GetAllAsync();
        Task<(IEnumerable<Funcionario> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize); // PAGINAÇÃO
        Task AddAsync(Funcionario funcionario);
        Task UpdateAsync(Funcionario funcionario);
        Task DeleteAsync(long id);
    }
}