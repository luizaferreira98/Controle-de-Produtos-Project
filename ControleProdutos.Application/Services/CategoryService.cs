// =============================================================================
// SenacGames.Application - CategoryService
// =============================================================================
// Implementação do serviço de categorias.
// Segue o mesmo padrão do GameService.
// =============================================================================

using ControleProdutos.Application.DTOs;
using ControleProdutos.Application.Interfaces;
using ControleProdutos.Domain.Entities;
using ControleProdutos.Domain.Interfaces;



namespace ControleProdutos.Application.Services
{
    /// <summary>
    /// Serviço de Categorias — lógica de aplicação para operações com categorias.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(MapToDto);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category == null ? null : MapToDto(category);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category { Name = dto.Name };
            await _categoryRepository.AddAsync(category);
            return MapToDto(category);
        }

        public async Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;

            category.Name = dto.Name;
            await _categoryRepository.UpdateAsync(category);
            return MapToDto(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return false;

            await _categoryRepository.DeleteAsync(id);
            return true;
        }

        public async Task<int> CountAsync()
        {
            return await _categoryRepository.CountAsync();
        }

        /// <summary>
        /// Mapeia uma entidade Category para CategoryDto.
        /// </summary>
        private static CategoryDto MapToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                GameCount = category.Games?.Count ?? 0
            };
        }
    }
}
