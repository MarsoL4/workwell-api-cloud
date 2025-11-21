using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.Enquetes;
using WorkWell.Application.DTOs.Paginacao;
using WorkWell.Domain.Entities.Enquetes;
using WorkWell.Domain.Extensions;
using WorkWell.Domain.Interfaces.Enquetes;

namespace WorkWell.Application.Services.Enquetes
{
    public class EnqueteService : IEnqueteService
    {
        private readonly IEnqueteRepository _enqueteRepository;
        private readonly IRespostaEnqueteRepository _respostaRepository;

        public EnqueteService(IEnqueteRepository enqueteRepository, IRespostaEnqueteRepository respostaRepository)
        {
            _enqueteRepository = enqueteRepository;
            _respostaRepository = respostaRepository;
        }

        public async Task<IEnumerable<EnqueteDto>> GetAllAsync()
            => (await _enqueteRepository.GetAllAsync()).Select(ToDto);

        public async Task<PagedResultDto<EnqueteDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var (items, total) = await _enqueteRepository.GetAllPagedAsync(page, pageSize);
            return new PagedResultDto<EnqueteDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = items.Select(ToDto).ToList()
            };
        }

        public async Task<EnqueteDto?> GetByIdAsync(long id)
        {
            var entity = await _enqueteRepository.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(EnqueteDto dto)
        {
            var entity = FromDto(dto);
            entity.DataCriacao = DateTime.UtcNow.TruncateToMinutes();
            await _enqueteRepository.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(EnqueteDto dto)
        {
            var entity = FromDto(dto);
            await _enqueteRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
            => await _enqueteRepository.DeleteAsync(id);

        public async Task<IEnumerable<RespostaEnqueteDto>> GetRespostasAsync(long enqueteId)
            => (await _respostaRepository.GetAllByEnqueteIdAsync(enqueteId)).Select(ToDto);

        public async Task<long> AdicionarRespostaAsync(long enqueteId, RespostaEnqueteDto dto)
        {
            var entity = FromDto(dto);
            entity.EnqueteId = enqueteId;
            await _respostaRepository.AddAsync(entity);
            return entity.Id;
        }

        // Helpers DTO/Entity
        private static EnqueteDto ToDto(Enquete e) => new()
        {
            Id = e.Id,
            EmpresaId = e.EmpresaId,
            Pergunta = e.Pergunta,
            DataCriacao = e.DataCriacao,
            Ativa = e.Ativa
        };

        private static Enquete FromDto(EnqueteDto dto) => new()
        {
            Id = dto.Id,
            EmpresaId = dto.EmpresaId,
            Pergunta = dto.Pergunta,
            DataCriacao = dto.DataCriacao ?? DateTime.UtcNow.TruncateToMinutes(),
            Ativa = dto.Ativa
        };

        private static RespostaEnqueteDto ToDto(RespostaEnquete e) => new()
        {
            Id = e.Id,
            EnqueteId = e.EnqueteId,
            FuncionarioId = e.FuncionarioId,
            Resposta = e.Resposta
        };

        private static RespostaEnquete FromDto(RespostaEnqueteDto dto) => new()
        {
            Id = dto.Id,
            EnqueteId = dto.EnqueteId,
            FuncionarioId = dto.FuncionarioId,
            Resposta = dto.Resposta
        };

        public async Task<bool> UpdateRespostaAsync(long enqueteId, RespostaEnqueteDto dto)
        {
            var entity = await _respostaRepository.GetByIdAsync(dto.Id);
            if (entity == null || entity.EnqueteId != enqueteId)
                return false;
            entity.FuncionarioId = dto.FuncionarioId;
            entity.Resposta = dto.Resposta;
            await _respostaRepository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteRespostaAsync(long enqueteId, long respostaId)
        {
            var entity = await _respostaRepository.GetByIdAsync(respostaId);
            if (entity == null || entity.EnqueteId != enqueteId)
                return false;

            await _respostaRepository.DeleteAsync(respostaId);
            return true;
        }
    }
}