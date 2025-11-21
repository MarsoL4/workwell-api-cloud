using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.ApoioPsicologico;

namespace WorkWell.Application.Services.ApoioPsicologico
{
    public interface IConsultaPsicologicaService
    {
        Task<IEnumerable<ConsultaPsicologicaDto>> GetAllAsync();
        Task<ConsultaPsicologicaDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(ConsultaPsicologicaDto dto);
        Task UpdateAsync(ConsultaPsicologicaDto dto);
        Task DeleteAsync(long id);
    }
}