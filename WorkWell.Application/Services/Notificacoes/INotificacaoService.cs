using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.Notificacoes;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.Application.Services.Notificacoes
{
    public interface INotificacaoService
    {
        Task<IEnumerable<NotificacaoDto>> GetAllAsync();
        Task<PagedResultDto<NotificacaoDto>> GetAllPagedAsync(int page, int pageSize); // NOVO
        Task<NotificacaoDto?> GetByIdAsync(long id);
        Task<IEnumerable<NotificacaoDto>> GetAllByFuncionarioIdAsync(long funcionarioId);
        Task<long> CreateAsync(NotificacaoDto dto);
        Task UpdateAsync(NotificacaoDto dto);
        Task DeleteAsync(long id);
    }
}