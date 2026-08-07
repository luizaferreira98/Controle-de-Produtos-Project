// =============================================================================
// SenacGames.Domain - Entidade Category
// =============================================================================
// Esta classe representa uma categoria de Produtos no sistema.
// Exemplos: "Ação", "RPG", "Corrida", "Terror", etc.
//
//  CONCEITO IMPORTANTE:
// Uma Category possui MUITOS Produtos (relação 1:N - um para muitos).
// Isso significa que cada Category pode ter vários Produtos associados.
// =============================================================================

namespace ControleProdutos.Domain.Entities
{
    /// <summary>
    /// Representa uma categoria de Produtos.
    /// Uma categoria agrupa Produtos do mesmo tipo.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Identificador único da categoria (chave primária).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da categoria. Exemplo: "Esportes", "Eletrônicos", "".
        /// </summary>
        public string Name { get; set; } = string.Empty;

        // =====================================================================
        // NAVIGATION PROPERTY - Coleção de Products
        // =====================================================================
        //  CONCEITO:
        // Uma Category pode ter VÁRIOS Produtos associados (relação 1:N).
        // O ICollection<Product> representa essa coleção de produtos.
        // O Entity Framework usa essa propriedade para fazer JOINs automáticos.
        // =====================================================================

        /// <summary>
        /// Lista de Produtos que pertencem a esta categoria (propriedade de navegação).
        /// </summary>
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
