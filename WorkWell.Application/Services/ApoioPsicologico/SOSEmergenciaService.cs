using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Extensions;
using WorkWell.Domain.Interfaces.ApoioPsicologico;

namespace WorkWell.Application.Services.ApoioPsicologico
{
    public class SOSemergenciaService : ISOSemergenciaService
    {
        private readonly ISOSemergenciaRepository _repo;

        public SOSemergenciaService(ISOSemergenciaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SOSemergenciaDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            var dtos = new List<SOSemergenciaDto>();
            foreach (var entity in list)
                dtos.Add(ToDto(entity));
            return dtos;
        }

        public async Task<SOSemergenciaDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(SOSemergenciaDto dto)
        {
            var entity = FromDto(dto);
            entity.DataAcionamento = DateTime.UtcNow.TruncateToMinutes();
            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(SOSemergenciaDto dto)
        {
            var entity = FromDto(dto);
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
        }

        private static SOSemergenciaDto ToDto(SOSemergencia e) => new()
        {
            Id = e.Id,
            FuncionarioId = e.FuncionarioId,
            DataAcionamento = e.DataAcionamento,
            Tipo = e.Tipo,
            PsicologoNotificado = e.PsicologoNotificado
        };

        private static SOSemergencia FromDto(SOSemergenciaDto d) => new()
        {
            Id = d.Id,
            FuncionarioId = d.FuncionarioId,
            DataAcionamento = d.DataAcionamento ?? DateTime.UtcNow.TruncateToMinutes(),
            Tipo = d.Tipo,
            PsicologoNotificado = d.PsicologoNotificado
        };
    }
}