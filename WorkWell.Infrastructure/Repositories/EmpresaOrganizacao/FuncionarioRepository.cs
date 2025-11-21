using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;
using WorkWell.Infrastructure.Persistence;
using WorkWell.Infrastructure.Extensions;
using System.Linq;

namespace WorkWell.Infrastructure.Repositories.EmpresaOrganizacao
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly WorkWellDbContext _context;

        public FuncionarioRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<Funcionario?> GetByIdAsync(long id)
        {
            return await _context.Funcionarios.FindAsync(id);
        }

        public async Task<IEnumerable<Funcionario>> GetAllAsync()
        {
            return await _context.Funcionarios.ToListAsync();
        }

        public async Task<(IEnumerable<Funcionario> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize)
        {
            var query = _context.Funcionarios.AsQueryable();
            var total = await query.CountAsync();
            var items = await query
                .OrderBy(f => f.Id)
                .Paginate(page, pageSize)
                .ToListAsync();
            return (items, total);
        }

        public async Task AddAsync(Funcionario funcionario)
        {
            await _context.Funcionarios.AddAsync(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Funcionario funcionario)
        {
            _context.Funcionarios.Update(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();
            }
        }
    }
}