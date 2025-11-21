using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Interfaces.EmpresaOrganizacao
{
    public interface IEmpresaRepository
    {
        Task<Empresa?> GetByIdAsync(long id);
        Task<IEnumerable<Empresa>> GetAllAsync();
        Task<(IEnumerable<Empresa> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize); // NOVO
        Task AddAsync(Empresa empresa);
        Task UpdateAsync(Empresa empresa);
        Task DeleteAsync(long id);
    }
}