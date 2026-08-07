// =============================================================================
// SenacGames.Application - GameService
// =============================================================================
//  CONCEITO IMPORTANTE: Implementação do Serviço
// Esta classe IMPLEMENTA a interface IGameService.
// Ela usa o repositório (IGameRepository) para acessar o banco de dados
// e converte as entidades em DTOs antes de retornar para o controller.
//
// MAPEAMENTO MANUAL:
// Neste projeto didático, fazemos o mapeamento Entidade  DTO manualmente.
// Em projetos maiores, você pode usar bibliotecas como AutoMapper.
// =============================================================================

using ControleProdutos.Application.DTOs;
using ControleProdutos.Application.Interfaces;
using ControleProdutos.Domain.Entities;
using ControleProdutos.Domain.Interfaces;

namespace ControleProdutos.Application.Services
{
    /// <summary>
    /// Serviço de Games — contém a lógica de aplicação para operações com games.
    /// </summary>
    public class ProductService : IProductService
    {
        //  CONCEITO: Injeção de Dependência
        // O repositório é injetado via construtor. Isso permite que o .NET
        // forneça automaticamente a implementação correta em tempo de execução.
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Retorna todos os games convertidos em DTOs.
        /// </summary>
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var product = await _productRepository.GetAllAsync();
            return product.Select(MapToDto);
        }

        /// <summary>
        /// Busca um game pelo Id e retorna como DTO.
        /// </summary>
        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : MapToDto(product);
        }

      
       

        /// <summary>
        /// Retorna os games de uma categoria específica.
        /// </summary>
        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId)
        {
            var product = await _productRepository.GetByCategoryAsync(categoryId);
            return product.Select(MapToDto);
        }

        /// <summary>
        /// Cria um novo game a partir do DTO de criação.
        /// </summary>
        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            // Mapeia o DTO de criação para a entidade Game
            var product = new Product
            {
                Title = dto.Title,
                Description = dto.Description,
                CoverImageUrl = dto.CoverImageUrl,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.Now
            };

            await _productRepository.AddAsync(product);

            // Retorna o game criado como DTO
            return MapToDto(product);
        }

        /// <summary>
        /// Atualiza um game existente.
        /// </summary>
        public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            // Atualiza os campos do game com os dados do DTO
            product.Title = dto.Title;
            product.Description = dto.Description;
            product.CoverImageUrl = dto.CoverImageUrl;
            product.CategoryId = dto.CategoryId;
            

            await _productRepository.UpdateAsync(product);
            return MapToDto(product);
        }

        /// <summary>
        /// Remove um game pelo Id.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return false;

            await _productRepository.DeleteAsync(id);
            return true;
        }

        /// <summary>
        /// Retorna o total de games.
        /// </summary>
        public async Task<int> CountAsync()
        {
            return await _productRepository.CountAsync();
        }

        // =====================================================================
        // MÉTODO PRIVADO DE MAPEAMENTO
        // =====================================================================
        //  CONCEITO: Mapeamento Entidade  DTO
        // Este método converte uma entidade Game em um GameDto.
        // Ele é privado porque só é usado internamente pelo serviço.
        // =====================================================================
        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                CoverImageUrl = product.CoverImageUrl,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty,
                CreatedAt = product.CreatedAt
            };
        }
    }
}
