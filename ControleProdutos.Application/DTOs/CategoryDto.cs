// =============================================================================
// SenacGames.Application - DTOs de Category
// =============================================================================

namespace ControleProdutos.Application.DTOs
{
    /// <summary>
    /// DTO para transferência de dados de uma Categoria.
    /// </summary>
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Quantidade de games nesta categoria.
        /// Útil para mostrar no dashboard e na listagem.
        /// </summary>
        public int ProductCount { get; set; }
    }

    /// <summary>
    /// DTO para criação de uma nova Categoria.
    /// </summary>
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para atualização de uma Categoria existente.
    /// </summary>
    public class UpdateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
