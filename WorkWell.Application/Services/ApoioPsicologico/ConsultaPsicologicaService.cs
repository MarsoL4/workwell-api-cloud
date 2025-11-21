using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;

namespace WorkWell.Application.Services.ApoioPsicologico
{
    public class ConsultaPsicologicaService : IConsultaPsicologicaService
    {
        private readonly IConsultaPsicologicaRepository _repo;

        public ConsultaPsicologicaService(IConsultaPsicologicaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ConsultaPsicologicaDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            var dtos = new List<ConsultaPsicologicaDto>();
            foreach (var e in list) dtos.Add(ToDto(e));
            return dtos;
        }

        public async Task<ConsultaPsicologicaDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(ConsultaPsicologicaDto dto)
        {
            var entity = FromDto(dto);
            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(ConsultaPsicologicaDto dto)
        {
            // Buscar entidade existente para evitar conflito de tracking
            var existente = await _repo.GetByIdAsync(dto.Id);
            if (existente == null) throw new KeyNotFoundException("Consulta não encontrada.");
            existente.FuncionarioId = dto.FuncionarioId;
            existente.PsicologoId = dto.PsicologoId;
            existente.DataConsulta = dto.DataConsulta;
            existente.Tipo = dto.Tipo;
            existente.Status = dto.Status;
            existente.AnotacoesSigilosas = dto.AnotacoesSigilosas;
            await _repo.UpdateAsync(existente);
        }

        public async Task DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
        }

        private static ConsultaPsicologicaDto ToDto(ConsultaPsicologica e) => new()
        {
            Id = e.Id,
            FuncionarioId = e.FuncionarioId,
            PsicologoId = e.PsicologoId,
            DataConsulta = e.DataConsulta,
            Tipo = e.Tipo,
            Status = e.Status,
            AnotacoesSigilosas = e.AnotacoesSigilosas
        };

        private static ConsultaPsicologica FromDto(ConsultaPsicologicaDto d) => new()
        {
            Id = d.Id,
            FuncionarioId = d.FuncionarioId,
            PsicologoId = d.PsicologoId,
            DataConsulta = d.DataConsulta,
            Tipo = d.Tipo,
            Status = d.Status,
            AnotacoesSigilosas = d.AnotacoesSigilosas
        };
    }
}