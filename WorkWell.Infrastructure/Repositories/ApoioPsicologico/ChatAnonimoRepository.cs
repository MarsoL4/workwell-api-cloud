using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.ApoioPsicologico
{
    public class ChatAnonimoRepository : IChatAnonimoRepository
    {
        private readonly WorkWellDbContext _context;

        public ChatAnonimoRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<ChatAnonimo?> GetByIdAsync(long id)
        {
            return await _context.ChatsAnonimos.FindAsync(id);
        }

        public async Task<IEnumerable<ChatAnonimo>> GetAllAsync()
        {
            return await _context.ChatsAnonimos.ToListAsync();
        }

        public async Task AddAsync(ChatAnonimo chat)
        {
            await _context.ChatsAnonimos.AddAsync(chat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ChatAnonimo chat)
        {
            _context.ChatsAnonimos.Update(chat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var chat = await _context.ChatsAnonimos.FindAsync(id);
            if (chat != null)
            {
                _context.ChatsAnonimos.Remove(chat);
                await _context.SaveChangesAsync();
            }
        }
    }
}