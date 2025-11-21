using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.Enquetes;

namespace WorkWell.Domain.Interfaces.Enquetes
{
    public interface IEnqueteRepository
    {
        Task<Enquete?> GetByIdAsync(long id);
        Task<IEnumerable<Enquete>> GetAllAsync();
        Task<(IEnumerable<Enquete> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize); // NOVO
        Task AddAsync(Enquete enquete);
        Task UpdateAsync(Enquete enquete);
        Task DeleteAsync(long id);
    }
}