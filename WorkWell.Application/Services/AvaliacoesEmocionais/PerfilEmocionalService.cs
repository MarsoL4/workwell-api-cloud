using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Extensions;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;

namespace WorkWell.Application.Services.AvaliacoesEmocionais
{
    public class PerfilEmocionalService : IPerfilEmocionalService
    {
        private readonly IPerfilEmocionalRepository _repo;

        public PerfilEmocionalService(IPerfilEmocionalRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PerfilEmocionalDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            var dtos = new List<PerfilEmocionalDto>();
            foreach (var e in list)
                dtos.Add(ToDto(e));
            return dtos;
        }

        public async Task<PerfilEmocionalDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(PerfilEmocionalDto dto)
        {
            var entity = FromDto(dto);
            entity.DataCriacao = DateTime.UtcNow.TruncateToMinutes();
            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(PerfilEmocionalDto dto)
        {
            // Para evitar tracking duplicado/erro, busque e atualize a instância existente:
            var existente = await _repo.GetByIdAsync(dto.Id);
            if (existente == null) throw new KeyNotFoundException("PerfilEmocional não encontrado.");
            existente.FuncionarioId = dto.FuncionarioId;
            existente.HumorInicial = dto.HumorInicial;
            existente.Rotina = dto.Rotina;
            existente.PrincipaisEstressores = dto.PrincipaisEstressores;
            // Não atualizar DataCriacao!
            await _repo.UpdateAsync(existente);
        }

        public async Task DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
        }

        private static PerfilEmocionalDto ToDto(PerfilEmocional e) => new()
        {
            Id = e.Id,
            FuncionarioId = e.FuncionarioId,
            HumorInicial = e.HumorInicial,
            Rotina = e.Rotina,
            PrincipaisEstressores = e.PrincipaisEstressores,
            DataCriacao = e.DataCriacao
        };

        private static PerfilEmocional FromDto(PerfilEmocionalDto d) => new()
        {
            Id = d.Id,
            FuncionarioId = d.FuncionarioId,
            HumorInicial = d.HumorInicial,
            Rotina = d.Rotina,
            PrincipaisEstressores = d.PrincipaisEstressores,
            DataCriacao = d.DataCriacao ?? DateTime.UtcNow.TruncateToMinutes()
        };
    }
}