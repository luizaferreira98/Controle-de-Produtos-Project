// =============================================================================
// SenacGames.Infrastructure - GameRepository
// =============================================================================
//  CONCEITO: Repositório (Repository Pattern)
// O repositório encapsula toda a lógica de acesso a dados.
// Ele usa o DbContext do Entity Framework para executar as operações.
//
// Benefícios do Repository Pattern:
// - Centraliza o acesso a dados em um único lugar
// - Facilita a manutenção e testes
// - A camada Application não precisa conhecer o EF Core
// =============================================================================

using Microsoft.EntityFrameworkCore;
using ControleProdutos.Domain.Entities;
using ControleProdutos.Domain.Interfaces;
using ControleProdutos.Infrastructure.Context;

namespace ControleProdutos.Infrastructure.Repositories
{
    /// <summary>
    /// Implementação do repositório de Products usando Entity Framework Core.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ControleProdutosDbContext _context;

        public ProductRepository(ControleProdutosDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os games incluindo a categoria relacionada.
        ///  CONCEITO: Include() — carrega dados de tabelas relacionadas (JOIN).
        /// </summary>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)  // Faz JOIN com a tabela Categories
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Busca um product pelo Id incluindo sua categoria.
        /// </summary>
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        /// <summary>
        /// Retorna todos os products de uma categoria específica.
        /// </summary>
        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        /// <summary>
        /// Adiciona um novo product  ao banco de dados.
        ///  CONCEITO: AddAsync() + SaveChangesAsync()
        /// AddAsync() marca a entidade para inserção.
        /// SaveChangesAsync() executa o INSERT no banco de dados.
        /// </summary>
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza um product existente.
        ///  CONCEITO: Update() marca a entidade como modificada.
        /// SaveChangesAsync() executa o UPDATE no banco.
        /// </summary>
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove um product do banco de dados.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retorna o total de products cadastrados.
        ///  CONCEITO: CountAsync() — executa COUNT(*) no banco.
        /// </summary>
        public async Task<int> CountAsync()
        {
            return await _context.Products.CountAsync();
        }
    }
}
