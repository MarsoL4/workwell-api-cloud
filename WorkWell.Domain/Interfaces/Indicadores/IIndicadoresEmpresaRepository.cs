using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.Indicadores;

namespace WorkWell.Domain.Interfaces.Indicadores
{
    public interface IIndicadoresEmpresaRepository
    {
        Task<IndicadoresEmpresa?> GetByIdAsync(long id);
        Task<IEnumerable<IndicadoresEmpresa>> GetAllAsync();
        Task<(IEnumerable<IndicadoresEmpresa> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize); // PAGINAÇÃO
        Task AddAsync(IndicadoresEmpresa indicadores);
        Task UpdateAsync(IndicadoresEmpresa indicadores);
        Task DeleteAsync(long id);
    }
}