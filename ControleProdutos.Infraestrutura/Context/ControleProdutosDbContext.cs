// =============================================================================
// SenacGames.Infrastructure - DbContext
// =============================================================================
//  CONCEITO IMPORTANTE: DbContext (Entity Framework Core)
// O DbContext é a classe PRINCIPAL do Entity Framework Core.
// Ele representa uma "sessão" com o banco de dados e permite:
// - Consultar dados (SELECT)
// - Inserir dados (INSERT)
// - Atualizar dados (UPDATE)
// - Deletar dados (DELETE)
//
// Ele herda de IdentityDbContext porque também gerencia as tabelas
// do ASP.NET Core Identity (usuários, roles, claims, etc.).
// =============================================================================

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ControleProdutos.Domain.Entities;
using ControleProdutos.Infrastructure.Configurations;

namespace ControleProdutos.Infrastructure.Context
{
    /// <summary>
    /// Contexto do banco de dados do ControleProdutos.
    /// Herda de IdentityDbContext para incluir as tabelas do Identity.
    /// </summary>
    public class ControleProdutosDbContext : IdentityDbContext
    {
        // =====================================================================
        //  CONCEITO: Construtor com DbContextOptions
        // O ASP.NET Core injeta as opções de configuração (connection string,
        // provider, etc.) automaticamente via Dependency Injection.
        // =====================================================================
        public ControleProdutosDbContext(DbContextOptions<ControleProdutosDbContext> options)
            : base(options)
        {
        }

        // =====================================================================
        // DbSets — Representam as tabelas no banco de dados
        // =====================================================================
        //  CONCEITO: DbSet<T>
        // Cada DbSet<T> representa uma tabela no banco de dados.
        // O Entity Framework cria automaticamente as tabelas correspondentes.
        // =====================================================================

        /// <summary>
        /// Tabela de Products no banco de dados.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Tabela de Categorias no banco de dados.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        // =====================================================================
        //  CONCEITO: OnModelCreating (Fluent API)
        // Este método permite configurar o modelo do banco de dados usando
        // a Fluent API do Entity Framework Core.
        // Aqui aplicamos as configurações definidas em classes separadas.
        // =====================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // IMPORTANTE: Sempre chamar base.OnModelCreating() quando herdar
            // de IdentityDbContext, para que as tabelas do Identity sejam criadas.
            base.OnModelCreating(modelBuilder);

            // Aplica as configurações de cada entidade (definidas em classes separadas)
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
