using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.ApoioPsicologico;

namespace WorkWell.Domain.Interfaces.ApoioPsicologico
{
    public interface IPsicologoRepository
    {
        Task<Psicologo?> GetByIdAsync(long id);
        Task<IEnumerable<Psicologo>> GetAllAsync();
        Task<(IEnumerable<Psicologo> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize); // NOVO
        Task AddAsync(Psicologo psicologo);
        Task UpdateAsync(Psicologo psicologo);
        Task DeleteAsync(long id);
    }
}