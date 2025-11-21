using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Application.DTOs.Paginacao;
using System.Linq;

namespace WorkWell.Application.Services.ApoioPsicologico
{
    public class PsicologoService : IPsicologoService
    {
        private readonly IPsicologoRepository _repo;

        public PsicologoService(IPsicologoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PsicologoDto>> GetAllAsync()
        {
            var models = await _repo.GetAllAsync();
            var dtos = new List<PsicologoDto>();
            foreach (var m in models) dtos.Add(ToDto(m));
            return dtos;
        }

        public async Task<PagedResultDto<PsicologoDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var (items, total) = await _repo.GetAllPagedAsync(page, pageSize);
            return new PagedResultDto<PsicologoDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = items.Select(ToDto).ToList()
            };
        }

        public async Task<PsicologoDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(PsicologoDto dto)
        {
            var entity = FromDto(dto);
            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(PsicologoDto dto)
        {
            // Busca a entidade existente (inheritance Funcionario) para evitar erro de tracking duplicado no EF Core
            var psicologoExistente = await _repo.GetByIdAsync(dto.Id);
            if (psicologoExistente == null)
                throw new KeyNotFoundException("Psicólogo não encontrado.");

            psicologoExistente.Nome = dto.Nome;
            psicologoExistente.Email = dto.Email;
            psicologoExistente.Senha = dto.Senha;
            psicologoExistente.TokenEmpresa = dto.TokenEmpresa;
            psicologoExistente.Crp = dto.Crp;
            psicologoExistente.Ativo = dto.Ativo;
            psicologoExistente.SetorId = dto.SetorId;

            await _repo.UpdateAsync(psicologoExistente);
        }

        public async Task DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
        }

        private static PsicologoDto ToDto(Psicologo e) => new()
        {
            Id = e.Id,
            Nome = e.Nome,
            Email = e.Email,
            Senha = e.Senha,
            TokenEmpresa = e.TokenEmpresa,
            Crp = e.Crp,
            Ativo = e.Ativo,
            SetorId = e.SetorId
        };

        private static Psicologo FromDto(PsicologoDto d) => new()
        {
            Id = d.Id,
            Nome = d.Nome,
            Email = d.Email,
            Senha = d.Senha,
            TokenEmpresa = d.TokenEmpresa,
            Crp = d.Crp,
            Ativo = d.Ativo,
            SetorId = d.SetorId
        };
    }
}