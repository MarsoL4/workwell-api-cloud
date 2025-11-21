using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Interfaces.EmpresaOrganizacao
{
    public interface ISetorRepository
    {
        Task<Setor?> GetByIdAsync(long id);
        Task<IEnumerable<Setor>> GetAllAsync();
        Task<(IEnumerable<Setor> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize); // NOVO
        Task AddAsync(Setor setor);
        Task UpdateAsync(Setor setor);
        Task DeleteAsync(long id);
    }
}