// =============================================================================
// SenacGames.Infrastructure - CategoryRepository
// =============================================================================
// Implementação do repositório de categorias.
// Segue o mesmo padrão do GameRepository.
// =============================================================================

using Microsoft.EntityFrameworkCore;
using ControleProdutos.Domain.Entities;
using ControleProdutos.Domain.Interfaces;
using ControleProdutos.Infrastructure.Context;

namespace ControleProdutos.Infrastructure.Repositories
{
    /// <summary>
    /// Implementação do repositório de Categorias usando Entity Framework Core.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ControleProdutosDbContext _context;

        public CategoryRepository(ControleProdutosDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Products) // Inclui os products para contar
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountAsync()
        {
            return await _context.Categories.CountAsync();
        }
    }
}
