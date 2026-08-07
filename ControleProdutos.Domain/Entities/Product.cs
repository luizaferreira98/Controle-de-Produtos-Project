// =============================================================================
// SenacGames.Domain - Entidade Game
// =============================================================================
// SenacGames.Domain - Entidade Produto
// =============================================================================
// Esta classe representa a entidade principal do sistema: um Produto.
// Ela pertence à camada de DOMÍNIO, que é responsável por definir as entidades
// e regras de negócio do sistema.
//
//  CONCEITO IMPORTANTE:
// A camada Domain NÃO depende de nenhuma outra camada.
// Ela é o "coração" da aplicação e define O QUE o sistema é.
// =============================================================================

namespace SenacGames.Domain.Entities
{
    /// <summary>
    /// Representa um Produto no catálogo do SenacGames.
    /// Cada Produto possui um título, descrição,
    /// imagem do Produto e pertence a uma categoria.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Identificador único do Produto (chave primária).
        /// O Entity Framework gera automaticamente esse valor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Título do Produto. Exemplo: "God of War Ragnarök"
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Descrição detalhada do Produto.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        /// <summary>
        /// URL da imagem de capa do Produto.
        /// </summary>
        public string CoverImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Chave estrangeira (FK) que relaciona o Produto com uma categoria.
        ///  CONCEITO: Foreign Key - conecta duas tabelas no banco de dados.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Data de criação do registro no banco de dados.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // =====================================================================
        // NAVIGATION PROPERTY (Propriedade de Navegação)
        // =====================================================================
        //  CONCEITO IMPORTANTE:
        // Navigation Properties permitem que o Entity Framework carregue
        // automaticamente os dados relacionados de outra tabela.
        // Aqui, cada Game "navega" até sua Category correspondente.
        // =====================================================================

        /// <summary>
        /// Categoria à qual este Produto pertence (propriedade de navegação).
        /// </summary>
        public virtual Category? Category { get; set; }
    }
}
