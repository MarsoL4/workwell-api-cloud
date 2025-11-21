using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.ApoioPsicologico
{
    public class SOSemergenciaRepository : ISOSemergenciaRepository
    {
        private readonly WorkWellDbContext _context;

        public SOSemergenciaRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<SOSemergencia?> GetByIdAsync(long id)
        {
            return await _context.SOSemergencias.FindAsync(id);
        }

        public async Task<IEnumerable<SOSemergencia>> GetAllAsync()
        {
            return await _context.SOSemergencias.ToListAsync();
        }

        public async Task AddAsync(SOSemergencia sos)
        {
            await _context.SOSemergencias.AddAsync(sos);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SOSemergencia sos)
        {
            _context.SOSemergencias.Update(sos);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _context.SOSemergencias.FindAsync(id);
            if (entity != null)
            {
                _context.SOSemergencias.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}