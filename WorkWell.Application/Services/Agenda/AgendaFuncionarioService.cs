using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.Agenda;
using WorkWell.Domain.Entities.Agenda;
using WorkWell.Domain.Interfaces.Agenda;

namespace WorkWell.Application.Services.Agenda
{
    public class AgendaFuncionarioService : IAgendaFuncionarioService
    {
        private readonly IAgendaFuncionarioRepository _agendaRepository;
        private readonly IItemAgendaRepository _itemRepository;

        public AgendaFuncionarioService(
            IAgendaFuncionarioRepository agendaRepository,
            IItemAgendaRepository itemRepository)
        {
            _agendaRepository = agendaRepository;
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<AgendaFuncionarioDto>> GetAllAsync()
        {
            var agendas = await _agendaRepository.GetAllAsync();
            return agendas.Select(ToDto);
        }

        public async Task<AgendaFuncionarioDto?> GetByIdAsync(long id)
        {
            var agenda = await _agendaRepository.GetByIdAsync(id);
            return agenda == null ? null : ToDto(agenda);
        }

        public async Task<long> CreateAsync(AgendaFuncionarioDto dto)
        {
            var agenda = FromDto(dto);
            await _agendaRepository.AddAsync(agenda);
            return agenda.Id;
        }

        public async Task UpdateAsync(AgendaFuncionarioDto dto)
        {
            var agenda = FromDto(dto);
            await _agendaRepository.UpdateAsync(agenda);
        }

        public async Task DeleteAsync(long id)
        {
            await _agendaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ItemAgendaDto>> GetItensAsync(long agendaFuncionarioId)
        {
            var itens = await _itemRepository.GetAllByAgendaIdAsync(agendaFuncionarioId);
            return itens.Select(ToDto);
        }

        public async Task<long> AdicionarItemAsync(long agendaFuncionarioId, ItemAgendaDto dto)
        {
            var item = FromDto(dto);
            item.AgendaFuncionarioId = agendaFuncionarioId;
            await _itemRepository.AddAsync(item);
            return item.Id;
        }

        public async Task<bool> UpdateItemAsync(long agendaFuncionarioId, ItemAgendaDto dto)
        {
            var item = await _itemRepository.GetByIdAsync(dto.Id);
            if (item == null || item.AgendaFuncionarioId != agendaFuncionarioId)
                return false;
            // Atualiza apenas os campos possíveis
            item.Tipo = dto.Tipo;
            item.Titulo = dto.Titulo;
            item.Horario = dto.Horario;
            await _itemRepository.UpdateAsync(item);
            return true;
        }

        public async Task<bool> DeleteItemAsync(long agendaFuncionarioId, long itemId)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item == null || item.AgendaFuncionarioId != agendaFuncionarioId)
                return false;
            await _itemRepository.DeleteAsync(itemId);
            return true;
        }

        // Converters
        private static AgendaFuncionarioDto ToDto(AgendaFuncionario entidade) =>
            new()
            {
                Id = entidade.Id,
                FuncionarioId = entidade.FuncionarioId,
                Data = entidade.Data,
                Itens = entidade.Itens.Select(ToDto).ToList()
            };

        private static AgendaFuncionario FromDto(AgendaFuncionarioDto dto) =>
            new()
            {
                Id = dto.Id,
                FuncionarioId = dto.FuncionarioId,
                Data = dto.Data,
                Itens = dto.Itens.Select(FromDto).ToList()
            };

        private static ItemAgendaDto ToDto(ItemAgenda entidade) =>
            new()
            {
                Id = entidade.Id,
                AgendaFuncionarioId = entidade.AgendaFuncionarioId,
                Tipo = entidade.Tipo,
                Titulo = entidade.Titulo,
                Horario = entidade.Horario
            };

        private static ItemAgenda FromDto(ItemAgendaDto dto) =>
            new()
            {
                Id = dto.Id,
                AgendaFuncionarioId = dto.AgendaFuncionarioId,
                Tipo = dto.Tipo,
                Titulo = dto.Titulo,
                Horario = dto.Horario
            };
    }
}