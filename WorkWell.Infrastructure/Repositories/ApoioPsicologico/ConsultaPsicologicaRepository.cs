using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.ApoioPsicologico
{
    public class ConsultaPsicologicaRepository : IConsultaPsicologicaRepository
    {
        private readonly WorkWellDbContext _context;

        public ConsultaPsicologicaRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<ConsultaPsicologica?> GetByIdAsync(long id) =>
            await _context.ConsultasPsicologicas.FindAsync(id);

        public async Task<IEnumerable<ConsultaPsicologica>> GetAllAsync() =>
            await _context.ConsultasPsicologicas.ToListAsync();

        public async Task AddAsync(ConsultaPsicologica consulta)
        {
            await _context.ConsultasPsicologicas.AddAsync(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ConsultaPsicologica consulta)
        {
            _context.ConsultasPsicologicas.Update(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var consulta = await _context.ConsultasPsicologicas.FindAsync(id);
            if (consulta != null)
            {
                _context.ConsultasPsicologicas.Remove(consulta);
                await _context.SaveChangesAsync();
            }
        }
    }
}