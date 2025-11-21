using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;
using WorkWell.Domain.Extensions; 

namespace WorkWell.Application.Services.AvaliacoesEmocionais
{
    public class MoodCheckService : IMoodCheckService
    {
        private readonly IMoodCheckRepository _repo;

        public MoodCheckService(IMoodCheckRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<MoodCheckDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            var dtos = new List<MoodCheckDto>();
            foreach (var e in list)
                dtos.Add(ToDto(e));
            return dtos;
        }

        public async Task<MoodCheckDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(MoodCheckDto dto)
        {
            var entity = FromDto(dto);
            entity.DataRegistro = DateTime.UtcNow.TruncateToMinutes();
            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(MoodCheckDto dto)
        {
            var entity = FromDto(dto);
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
        }

        private static MoodCheckDto ToDto(MoodCheck e) => new()
        {
            Id = e.Id,
            FuncionarioId = e.FuncionarioId,
            Humor = e.Humor,
            Produtivo = e.Produtivo,
            Estressado = e.Estressado,
            DormiuBem = e.DormiuBem,
            DataRegistro = e.DataRegistro
        };

        private static MoodCheck FromDto(MoodCheckDto d) => new()
        {
            Id = d.Id,
            FuncionarioId = d.FuncionarioId,
            Humor = d.Humor,
            Produtivo = d.Produtivo,
            Estressado = d.Estressado,
            DormiuBem = d.DormiuBem,
            DataRegistro = d.DataRegistro ?? DateTime.UtcNow.TruncateToMinutes() // safe fallback
        };
    }
}