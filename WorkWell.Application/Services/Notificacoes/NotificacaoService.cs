using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.Notificacoes;
using WorkWell.Application.DTOs.Paginacao;
using WorkWell.Domain.Entities.Notificacoes;
using WorkWell.Domain.Extensions;
using WorkWell.Domain.Interfaces.Notificacoes;

namespace WorkWell.Application.Services.Notificacoes
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly INotificacaoRepository _repository;

        public NotificacaoService(INotificacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<NotificacaoDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(ToDto);
        }

        public async Task<PagedResultDto<NotificacaoDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var (items, total) = await _repository.GetAllPagedAsync(page, pageSize);
            return new PagedResultDto<NotificacaoDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = items.Select(ToDto).ToList()
            };
        }

        public async Task<NotificacaoDto?> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<IEnumerable<NotificacaoDto>> GetAllByFuncionarioIdAsync(long funcionarioId)
        {
            var list = await _repository.GetAllByFuncionarioIdAsync(funcionarioId);
            return list.Select(ToDto);
        }

        public async Task<long> CreateAsync(NotificacaoDto dto)
        {
            var entity = FromDto(dto);
            entity.DataEnvio = DateTime.UtcNow.TruncateToMinutes();
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(NotificacaoDto dto)
        {
            var entity = FromDto(dto);
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }

        private static NotificacaoDto ToDto(Notificacao e) => new()
        {
            Id = e.Id,
            FuncionarioId = e.FuncionarioId,
            Mensagem = e.Mensagem,
            Tipo = e.Tipo,
            Lida = e.Lida,
            DataEnvio = e.DataEnvio
        };

        private static Notificacao FromDto(NotificacaoDto dto) => new()
        {
            Id = dto.Id,
            FuncionarioId = dto.FuncionarioId,
            Mensagem = dto.Mensagem,
            Tipo = dto.Tipo,
            Lida = dto.Lida,
            DataEnvio = dto.DataEnvio ?? DateTime.UtcNow.TruncateToMinutes()
        };
    }
}