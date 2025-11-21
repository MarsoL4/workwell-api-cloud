using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Extensions;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;

namespace WorkWell.Application.Services.AvaliacoesEmocionais
{
    public class RiscoPsicossocialService : IRiscoPsicossocialService
    {
        private readonly IRiscoPsicossocialRepository _repo;

        public RiscoPsicossocialService(IRiscoPsicossocialRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RiscoPsicossocialDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            var dtos = new List<RiscoPsicossocialDto>();
            foreach (var e in list)
                dtos.Add(ToDto(e));
            return dtos;
        }

        public async Task<RiscoPsicossocialDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(RiscoPsicossocialDto dto)
        {
            var entity = FromDto(dto);
            entity.DataRegistro = DateTime.UtcNow.TruncateToMinutes();
            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(RiscoPsicossocialDto dto)
        {
            var entity = FromDto(dto);
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
        }

        private static RiscoPsicossocialDto ToDto(RiscoPsicossocial e) => new()
        {
            Id = e.Id,
            FuncionarioId = e.FuncionarioId,
            Categoria = e.Categoria,
            NivelRisco = e.NivelRisco,
            DataRegistro = e.DataRegistro
        };

        private static RiscoPsicossocial FromDto(RiscoPsicossocialDto d) => new()
        {
            Id = d.Id,
            FuncionarioId = d.FuncionarioId,
            Categoria = d.Categoria,
            NivelRisco = d.NivelRisco,
            DataRegistro = d.DataRegistro ?? DateTime.UtcNow.TruncateToMinutes()
        };
    }
}