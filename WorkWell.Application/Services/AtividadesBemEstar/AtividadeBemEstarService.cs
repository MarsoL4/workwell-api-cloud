using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.AtividadesBemEstar;
using WorkWell.Domain.Entities.AtividadesBemEstar;
using WorkWell.Domain.Interfaces.AtividadesBemEstar;

namespace WorkWell.Application.Services.AtividadesBemEstar
{
    public class AtividadeBemEstarService : IAtividadeBemEstarService
    {
        private readonly IAtividadeBemEstarRepository _atividadeRepo;
        private readonly IParticipacaoAtividadeRepository _participacaoRepo;

        public AtividadeBemEstarService(
            IAtividadeBemEstarRepository atividadeRepo,
            IParticipacaoAtividadeRepository participacaoRepo)
        {
            _atividadeRepo = atividadeRepo;
            _participacaoRepo = participacaoRepo;
        }

        public async Task<IEnumerable<AtividadeBemEstarDto>> GetAllAsync()
            => (await _atividadeRepo.GetAllAsync()).Select(ToDto);

        public async Task<AtividadeBemEstarDto?> GetByIdAsync(long id)
        {
            var entity = await _atividadeRepo.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(AtividadeBemEstarDto dto)
        {
            var entity = FromDto(dto);
            await _atividadeRepo.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(AtividadeBemEstarDto dto)
        {
            var existente = await _atividadeRepo.GetByIdAsync(dto.Id);
            if (existente == null) throw new KeyNotFoundException("Atividade não encontrada.");

            existente.EmpresaId = dto.EmpresaId;
            existente.Tipo = dto.Tipo;
            existente.Titulo = dto.Titulo;
            existente.Descricao = dto.Descricao;
            existente.DataInicio = dto.DataInicio;
            existente.DataFim = dto.DataFim;
            existente.SetorAlvoId = dto.SetorAlvoId;

            await _atividadeRepo.UpdateAsync(existente);
        }

        public async Task DeleteAsync(long id)
            => await _atividadeRepo.DeleteAsync(id);

        public async Task<IEnumerable<ParticipacaoAtividadeDto>> GetParticipacoesAsync(long atividadeId)
            => (await _participacaoRepo.GetAllByAtividadeIdAsync(atividadeId)).Select(ToDto);

        public async Task<long> AdicionarParticipacaoAsync(long atividadeId, ParticipacaoAtividadeDto dto)
        {
            var entity = FromDto(dto);
            entity.AtividadeId = atividadeId;
            entity.DataParticipacao = dto.DataParticipacao ?? DateTime.UtcNow;
            await _participacaoRepo.AddAsync(entity);
            return entity.Id;
        }

        public async Task<bool> UpdateParticipacaoAsync(long atividadeId, ParticipacaoAtividadeDto dto)
        {
            var entidade = await _participacaoRepo.GetByIdAsync(dto.Id);
            if (entidade == null || entidade.AtividadeId != atividadeId)
                return false;
            entidade.FuncionarioId = dto.FuncionarioId;
            entidade.Participou = dto.Participou;
            entidade.DataParticipacao = dto.DataParticipacao ?? entidade.DataParticipacao;
            await _participacaoRepo.UpdateAsync(entidade);
            return true;
        }

        public async Task<bool> DeleteParticipacaoAsync(long atividadeId, long participacaoId)
        {
            var entidade = await _participacaoRepo.GetByIdAsync(participacaoId);
            if (entidade == null || entidade.AtividadeId != atividadeId)
                return false;

            await _participacaoRepo.DeleteAsync(participacaoId);
            return true;
        }

        // Helpers DTO/Entity
        private static AtividadeBemEstarDto ToDto(AtividadeBemEstar e) => new()
        {
            Id = e.Id,
            EmpresaId = e.EmpresaId,
            Tipo = e.Tipo,
            Titulo = e.Titulo,
            Descricao = e.Descricao,
            DataInicio = e.DataInicio,
            DataFim = e.DataFim,
            SetorAlvoId = e.SetorAlvoId
        };

        private static AtividadeBemEstar FromDto(AtividadeBemEstarDto dto) => new()
        {
            Id = dto.Id,
            EmpresaId = dto.EmpresaId,
            Tipo = dto.Tipo,
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            DataInicio = dto.DataInicio,
            DataFim = dto.DataFim,
            SetorAlvoId = dto.SetorAlvoId
        };

        private static ParticipacaoAtividadeDto ToDto(ParticipacaoAtividade e) => new()
        {
            Id = e.Id,
            FuncionarioId = e.FuncionarioId,
            AtividadeId = e.AtividadeId,
            Participou = e.Participou,
            DataParticipacao = e.DataParticipacao
        };

        private static ParticipacaoAtividade FromDto(ParticipacaoAtividadeDto dto) => new()
        {
            Id = dto.Id,
            FuncionarioId = dto.FuncionarioId,
            AtividadeId = dto.AtividadeId,
            Participou = dto.Participou,
            DataParticipacao = dto.DataParticipacao ?? DateTime.UtcNow
        };
    }
}