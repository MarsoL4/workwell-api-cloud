using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Domain.Entities.Notificacoes;

namespace WorkWell.Domain.Interfaces.Notificacoes
{
    public interface INotificacaoRepository
    {
        Task<Notificacao?> GetByIdAsync(long id);
        Task<IEnumerable<Notificacao>> GetAllAsync();
        Task<(IEnumerable<Notificacao> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize); // NOVO
        Task<IEnumerable<Notificacao>> GetAllByFuncionarioIdAsync(long funcionarioId);
        Task AddAsync(Notificacao notificacao);
        Task UpdateAsync(Notificacao notificacao);
        Task DeleteAsync(long id);
    }
}