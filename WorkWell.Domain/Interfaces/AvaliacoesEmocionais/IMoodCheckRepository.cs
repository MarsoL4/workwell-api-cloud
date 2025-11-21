using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;

namespace WorkWell.Domain.Interfaces.AvaliacoesEmocionais
{
    public interface IMoodCheckRepository
    {
        Task<MoodCheck?> GetByIdAsync(long id);
        Task<IEnumerable<MoodCheck>> GetAllAsync();
        Task AddAsync(MoodCheck moodCheck);
        Task UpdateAsync(MoodCheck moodCheck);
        Task DeleteAsync(long id);
    }
}