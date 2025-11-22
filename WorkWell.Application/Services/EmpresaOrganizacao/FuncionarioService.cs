using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;
using System.Linq;

namespace WorkWell.Application.Services.EmpresaOrganizacao
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<IEnumerable<FuncionarioDto>> GetAllAsync()
        {
            var funcionarios = await _funcionarioRepository.GetAllAsync();
            var dtos = new List<FuncionarioDto>();
            foreach (var funcionario in funcionarios)
            {
                dtos.Add(ToDto(funcionario));
            }
            return dtos;
        }

        public async Task<PagedResultDto<FuncionarioDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var (items, total) = await _funcionarioRepository.GetAllPagedAsync(page, pageSize);
            return new PagedResultDto<FuncionarioDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = items.Select(ToDto).ToList()
            };
        }

        public async Task<FuncionarioDto?> GetByIdAsync(long id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            return funcionario == null ? null : ToDto(funcionario);
        }

        public async Task<long> CreateAsync(FuncionarioDto funcionarioDto)
        {
            var funcionario = FromDto(funcionarioDto);
            await _funcionarioRepository.AddAsync(funcionario);
            return funcionario.Id;
        }

        public async Task UpdateAsync(FuncionarioDto funcionarioDto)
        {
            // Busca a entidade existente para evitar erro de tracking duplicado no EF Core
            var funcionarioExistente = await _funcionarioRepository.GetByIdAsync(funcionarioDto.Id);
            if (funcionarioExistente == null)
                throw new KeyNotFoundException("Funcionário não encontrado.");

            funcionarioExistente.Nome = funcionarioDto.Nome;
            funcionarioExistente.Email = funcionarioDto.Email;
            funcionarioExistente.Senha = funcionarioDto.Senha;
            funcionarioExistente.TokenEmpresa = funcionarioDto.TokenEmpresa;
            funcionarioExistente.Cargo = funcionarioDto.Cargo;
            funcionarioExistente.Ativo = funcionarioDto.Ativo;
            funcionarioExistente.SetorId = funcionarioDto.SetorId;
            funcionarioExistente.EmpresaId = funcionarioDto.EmpresaId; // <-- ADICIONADO!

            await _funcionarioRepository.UpdateAsync(funcionarioExistente);
        }

        public async Task DeleteAsync(long id)
        {
            await _funcionarioRepository.DeleteAsync(id);
        }

        private static FuncionarioDto ToDto(Funcionario funcionario)
        {
            return new FuncionarioDto
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Senha = funcionario.Senha,
                TokenEmpresa = funcionario.TokenEmpresa,
                Cargo = funcionario.Cargo,
                Ativo = funcionario.Ativo,
                SetorId = funcionario.SetorId,
                EmpresaId = funcionario.EmpresaId // <-- ADICIONADO!
            };
        }

        private static Funcionario FromDto(FuncionarioDto dto)
        {
            return new Funcionario
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                TokenEmpresa = dto.TokenEmpresa,
                Cargo = dto.Cargo,
                Ativo = dto.Ativo,
                SetorId = dto.SetorId,
                EmpresaId = dto.EmpresaId // <-- ADICIONADO!
            };
        }
    }
}