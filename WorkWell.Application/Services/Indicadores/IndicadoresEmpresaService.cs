using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.Indicadores;
using WorkWell.Application.DTOs.Paginacao;
using WorkWell.Domain.Entities.Indicadores;
using WorkWell.Domain.Interfaces.Indicadores;

namespace WorkWell.Application.Services.Indicadores
{
    public class IndicadoresEmpresaService : IIndicadoresEmpresaService
    {
        private readonly IIndicadoresEmpresaRepository _repository;

        public IndicadoresEmpresaService(IIndicadoresEmpresaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<IndicadoresEmpresaDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(ToDto);
        }

        public async Task<PagedResultDto<IndicadoresEmpresaDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var (items, total) = await _repository.GetAllPagedAsync(page, pageSize);
            return new PagedResultDto<IndicadoresEmpresaDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = items.Select(ToDto).ToList()
            };
        }

        public async Task<IndicadoresEmpresaDto?> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(IndicadoresEmpresaDto dto)
        {
            var entity = FromDto(dto);
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(IndicadoresEmpresaDto dto)
        {
            var entity = FromDto(dto);
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }

        private static IndicadoresEmpresaDto ToDto(IndicadoresEmpresa e) => new()
        {
            Id = e.Id,
            EmpresaId = e.EmpresaId,
            HumorMedio = e.HumorMedio,
            AdesaoAtividadesGeral = e.AdesaoAtividadesGeral,
            FrequenciaConsultas = e.FrequenciaConsultas,
            AdesaoPorSetor = e.AdesaoPorSetor.Select(a => new AdesaoSetorDto
            {
                Id = a.Id,
                SetorId = a.SetorId,
                Adesao = a.Adesao
            }).ToList()
        };

        private static IndicadoresEmpresa FromDto(IndicadoresEmpresaDto dto) => new()
        {
            Id = dto.Id,
            EmpresaId = dto.EmpresaId,
            HumorMedio = dto.HumorMedio,
            AdesaoAtividadesGeral = dto.AdesaoAtividadesGeral,
            FrequenciaConsultas = dto.FrequenciaConsultas,
            AdesaoPorSetor = dto.AdesaoPorSetor.ConvertAll(a => new AdesaoSetor
            {
                Id = a.Id,
                SetorId = a.SetorId,
                Adesao = a.Adesao
            })
        };
    }
}