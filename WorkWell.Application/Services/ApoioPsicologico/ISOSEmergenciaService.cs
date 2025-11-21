using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.ApoioPsicologico;

namespace WorkWell.Application.Services.ApoioPsicologico
{
    public interface ISOSemergenciaService
    {
        Task<IEnumerable<SOSemergenciaDto>> GetAllAsync();
        Task<SOSemergenciaDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(SOSemergenciaDto dto);
        Task UpdateAsync(SOSemergenciaDto dto);
        Task DeleteAsync(long id);
    }
}