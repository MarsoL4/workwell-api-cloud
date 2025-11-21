using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Extensions;
using WorkWell.Domain.Interfaces.ApoioPsicologico;

namespace WorkWell.Application.Services.ApoioPsicologico
{
    public class ChatAnonimoService : IChatAnonimoService
    {
        private readonly IChatAnonimoRepository _repo;

        public ChatAnonimoService(IChatAnonimoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ChatAnonimoDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            var dtos = new List<ChatAnonimoDto>();
            foreach (var entity in list)
                dtos.Add(ToDto(entity));
            return dtos;
        }

        public async Task<ChatAnonimoDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(ChatAnonimoDto dto)
        {
            var entity = FromDto(dto);
            entity.DataEnvio = DateTime.UtcNow.TruncateToMinutes();
            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(ChatAnonimoDto dto)
        {
            // Buscar entidade existente para evitar conflito de tracking
            var existente = await _repo.GetByIdAsync(dto.Id);
            if (existente == null) throw new KeyNotFoundException("Chat não encontrado.");
            existente.RemetenteId = dto.RemetenteId;
            existente.PsicologoId = dto.PsicologoId;
            existente.Mensagem = dto.Mensagem;
            existente.Anonimo = dto.Anonimo;
            // Não atualizar DataEnvio!
            await _repo.UpdateAsync(existente);
        }

        public async Task DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
        }

        private static ChatAnonimoDto ToDto(ChatAnonimo e) => new()
        {
            Id = e.Id,
            RemetenteId = e.RemetenteId,
            PsicologoId = e.PsicologoId,
            Mensagem = e.Mensagem,
            DataEnvio = e.DataEnvio,
            Anonimo = e.Anonimo
        };

        private static ChatAnonimo FromDto(ChatAnonimoDto d) => new()
        {
            Id = d.Id,
            RemetenteId = d.RemetenteId,
            PsicologoId = d.PsicologoId,
            Mensagem = d.Mensagem,
            DataEnvio = d.DataEnvio ?? DateTime.UtcNow.TruncateToMinutes(),
            Anonimo = d.Anonimo
        };
    }
}