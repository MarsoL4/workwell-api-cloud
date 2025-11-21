using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;
using System.Linq;

namespace WorkWell.Application.Services.EmpresaOrganizacao
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _setorRepository;

        public SetorService(ISetorRepository setorRepository)
        {
            _setorRepository = setorRepository;
        }

        public async Task<IEnumerable<SetorDto>> GetAllAsync()
        {
            var setores = await _setorRepository.GetAllAsync();
            var dtos = new List<SetorDto>();

            foreach (var setor in setores)
            {
                dtos.Add(ToDto(setor));
            }
            return dtos;
        }

        public async Task<PagedResultDto<SetorDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var (items, total) = await _setorRepository.GetAllPagedAsync(page, pageSize);
            return new PagedResultDto<SetorDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = items.Select(ToDto).ToList()
            };
        }

        public async Task<SetorDto?> GetByIdAsync(long id)
        {
            var setor = await _setorRepository.GetByIdAsync(id);
            return setor == null ? null : ToDto(setor);
        }

        public async Task<long> CreateAsync(SetorDto setorDto)
        {
            var setor = new Setor
            {
                Nome = setorDto.Nome,
                EmpresaId = setorDto.EmpresaId
            };
            await _setorRepository.AddAsync(setor);
            return setor.Id;
        }

        public async Task UpdateAsync(SetorDto setorDto)
        {
            var setor = new Setor
            {
                Id = setorDto.Id,
                Nome = setorDto.Nome,
                EmpresaId = setorDto.EmpresaId
            };
            await _setorRepository.UpdateAsync(setor);
        }

        public async Task DeleteAsync(long id)
        {
            await _setorRepository.DeleteAsync(id);
        }

        private static SetorDto ToDto(Setor setor)
        {
            return new SetorDto
            {
                Id = setor.Id,
                Nome = setor.Nome,
                EmpresaId = setor.EmpresaId
            };
        }
    }
}