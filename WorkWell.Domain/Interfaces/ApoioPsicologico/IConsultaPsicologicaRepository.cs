using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.ApoioPsicologico;

namespace WorkWell.Domain.Interfaces.ApoioPsicologico
{
    public interface IConsultaPsicologicaRepository
    {
        Task<ConsultaPsicologica?> GetByIdAsync(long id);
        Task<IEnumerable<ConsultaPsicologica>> GetAllAsync();
        Task AddAsync(ConsultaPsicologica consulta);
        Task UpdateAsync(ConsultaPsicologica consulta);
        Task DeleteAsync(long id);
    }
}