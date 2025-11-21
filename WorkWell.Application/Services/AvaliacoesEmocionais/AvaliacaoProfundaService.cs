using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Extensions;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;

namespace WorkWell.Application.Services.AvaliacoesEmocionais
{
    public class AvaliacaoProfundaService : IAvaliacaoProfundaService
    {
        private readonly IAvaliacaoProfundaRepository _repo;

        public AvaliacaoProfundaService(IAvaliacaoProfundaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AvaliacaoProfundaDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            var dtos = new List<AvaliacaoProfundaDto>();
            foreach (var e in list)
                dtos.Add(ToDto(e));
            return dtos;
        }

        public async Task<AvaliacaoProfundaDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(AvaliacaoProfundaDto dto)
        {
            var entity = FromDto(dto);
            entity.DataRegistro = DateTime.UtcNow.TruncateToMinutes();
            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(AvaliacaoProfundaDto dto)
        {
            // Buscar existente antes de atualizar (evita tracked entity error)
            var existente = await _repo.GetByIdAsync(dto.Id);
            if (existente == null) throw new KeyNotFoundException("Avaliação não encontrada.");
            existente.FuncionarioId = dto.FuncionarioId;
            existente.Gad7Score = dto.Gad7Score;
            existente.Phq9Score = dto.Phq9Score;
            existente.Interpretacao = dto.Interpretacao;
            // NÃO atualiza DataRegistro
            await _repo.UpdateAsync(existente);
        }

        public async Task DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
        }

        private static AvaliacaoProfundaDto ToDto(AvaliacaoProfunda e) => new()
        {
            Id = e.Id,
            FuncionarioId = e.FuncionarioId,
            Gad7Score = e.Gad7Score,
            Phq9Score = e.Phq9Score,
            Interpretacao = e.Interpretacao,
            DataRegistro = e.DataRegistro
        };

        private static AvaliacaoProfunda FromDto(AvaliacaoProfundaDto d) => new()
        {
            Id = d.Id,
            FuncionarioId = d.FuncionarioId,
            Gad7Score = d.Gad7Score,
            Phq9Score = d.Phq9Score,
            Interpretacao = d.Interpretacao,
            DataRegistro = d.DataRegistro ?? DateTime.UtcNow.TruncateToMinutes()
        };
    }
}