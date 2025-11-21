using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.ApoioPsicologico;

namespace WorkWell.Domain.Interfaces.ApoioPsicologico
{
    public interface ISOSemergenciaRepository
    {
        Task<SOSemergencia?> GetByIdAsync(long id);
        Task<IEnumerable<SOSemergencia>> GetAllAsync();
        Task AddAsync(SOSemergencia sos);
        Task UpdateAsync(SOSemergencia sos);
        Task DeleteAsync(long id);
    }
}