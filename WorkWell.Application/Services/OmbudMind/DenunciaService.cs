using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.OmbudMind;
using WorkWell.Domain.Entities.OmbudMind;
using WorkWell.Domain.Extensions;
using WorkWell.Domain.Interfaces.OmbudMind;

namespace WorkWell.Application.Services.OmbudMind
{
    public class DenunciaService : IDenunciaService
    {
        private readonly IDenunciaRepository _denunciaRepo;
        private readonly IInvestigacaoDenunciaRepository _investigacaoRepo;

        public DenunciaService(IDenunciaRepository denunciaRepo, IInvestigacaoDenunciaRepository investigacaoRepo)
        {
            _denunciaRepo = denunciaRepo;
            _investigacaoRepo = investigacaoRepo;
        }

        public async Task<IEnumerable<DenunciaDto>> GetAllAsync()
            => (await _denunciaRepo.GetAllAsync()).Select(ToDto);

        public async Task<DenunciaDto?> GetByIdAsync(long id)
        {
            var entity = await _denunciaRepo.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<DenunciaDto?> GetByCodigoRastreamentoAsync(string codigo)
        {
            var entity = await _denunciaRepo.GetByCodigoRastreamentoAsync(codigo);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(DenunciaDto dto)
        {
            var entity = FromDto(dto);
            entity.DataCriacao = DateTime.UtcNow.TruncateToMinutes();
            await _denunciaRepo.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(DenunciaDto dto)
        {
            // buscar a entidade já rastreada e atualizar nela
            var entity = await _denunciaRepo.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Denúncia não encontrada.");

            entity.FuncionarioDenuncianteId = dto.FuncionarioDenuncianteId;
            entity.EmpresaId = dto.EmpresaId;
            entity.Tipo = dto.Tipo;
            entity.Descricao = dto.Descricao;
            // MANTÉM DataCriacao existente
            entity.Status = dto.Status;
            entity.CodigoRastreamento = dto.CodigoRastreamento;

            await _denunciaRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _denunciaRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<InvestigacaoDenunciaDto>> GetInvestigacoesAsync(long denunciaId)
            => (await _investigacaoRepo.GetAllByDenunciaIdAsync(denunciaId)).Select(ToDto);

        public async Task<long> AdicionarInvestigacaoAsync(long denunciaId, InvestigacaoDenunciaDto dto)
        {
            var entity = FromDto(dto);
            entity.DenunciaId = denunciaId;
            await _investigacaoRepo.AddAsync(entity);
            return entity.Id;
        }

        // Helpers
        private static DenunciaDto ToDto(Denuncia d) => new()
        {
            Id = d.Id,
            FuncionarioDenuncianteId = d.FuncionarioDenuncianteId,
            EmpresaId = d.EmpresaId,
            Tipo = d.Tipo,
            Descricao = d.Descricao,
            DataCriacao = d.DataCriacao,
            Status = d.Status,
            CodigoRastreamento = d.CodigoRastreamento
        };

        private static Denuncia FromDto(DenunciaDto dto) => new()
        {
            Id = dto.Id,
            FuncionarioDenuncianteId = dto.FuncionarioDenuncianteId,
            EmpresaId = dto.EmpresaId,
            Tipo = dto.Tipo,
            Descricao = dto.Descricao,
            DataCriacao = dto.DataCriacao ?? DateTime.UtcNow.TruncateToMinutes(),
            Status = dto.Status,
            CodigoRastreamento = dto.CodigoRastreamento
        };

        private static InvestigacaoDenunciaDto ToDto(InvestigacaoDenuncia i) => new()
        {
            Id = i.Id,
            DenunciaId = i.DenunciaId,
            EquipeResponsavel = i.EquipeResponsavel,
            DataInicio = i.DataInicio,
            DataFim = i.DataFim,
            MedidasAdotadas = i.MedidasAdotadas,
            Concluida = i.Concluida
        };

        private static InvestigacaoDenuncia FromDto(InvestigacaoDenunciaDto dto) => new()
        {
            Id = dto.Id,
            DenunciaId = dto.DenunciaId,
            EquipeResponsavel = dto.EquipeResponsavel,
            DataInicio = dto.DataInicio,
            DataFim = dto.DataFim,
            MedidasAdotadas = dto.MedidasAdotadas,
            Concluida = dto.Concluida
        };

        public async Task<bool> UpdateInvestigacaoAsync(long denunciaId, InvestigacaoDenunciaDto dto)
        {
            var entity = await _investigacaoRepo.GetByIdAsync(dto.Id);
            if (entity == null || entity.DenunciaId != denunciaId)
                return false;
            entity.EquipeResponsavel = dto.EquipeResponsavel;
            entity.DataInicio = dto.DataInicio;
            entity.DataFim = dto.DataFim;
            entity.MedidasAdotadas = dto.MedidasAdotadas;
            entity.Concluida = dto.Concluida;
            await _investigacaoRepo.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteInvestigacaoAsync(long denunciaId, long investigacaoId)
        {
            var entity = await _investigacaoRepo.GetByIdAsync(investigacaoId);
            if (entity == null || entity.DenunciaId != denunciaId)
                return false;
            await _investigacaoRepo.DeleteAsync(investigacaoId);
            return true;
        }
    }
}