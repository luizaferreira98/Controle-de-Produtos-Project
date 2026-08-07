// =============================================================================
// SenacGames.Infrastructure - Configuração da entidade Game (Fluent API)
// =============================================================================
//  CONCEITO: IEntityTypeConfiguration<T>
// Esta classe define as regras de mapeamento da entidade Game para o banco.
// Usando Fluent API, podemos definir:
// - Tamanho máximo de campos (MaxLength)
// - Campos obrigatórios (IsRequired)
// - Relacionamentos entre tabelas
// - Nomes de tabelas e colunas
// =============================================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ControleProdutos.Domain.Entities;

namespace ControleProdutos.Infrastructure.Configurations
{
    /// <summary>
    /// Configuração Fluent API da entidade Product.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Define a chave primária
            builder.HasKey(p => p.Id);

            // Configurações dos campos
            builder.Property(p => p .Title)
                .IsRequired()           // Campo obrigatório
                .HasMaxLength(200);     // Máximo de 200 caracteres

            builder.Property(p => p.Description)
                .HasMaxLength(2000);    // Máximo de 2000 caracteres

            builder.Property(p => p.CoverImageUrl)
                .HasMaxLength(500);

            // =====================================================================
            //  CONCEITO: Configuração de Relacionamento (Fluent API)
            // Um Game pertence a UMA Category (relação N:1).
            // Uma Category possui MUITOS Games (relação 1:N).
            // HasOne  WithMany  HasForeignKey
            // =====================================================================
            builder.HasOne(p => p.Category)       // Um Product tem UMA Category
                .WithMany(c => c.Products)            // Uma Category tem MUITOS Products
                .HasForeignKey(p => p.CategoryId)  // A FK é CategoryId
                .OnDelete(DeleteBehavior.Restrict); // Não permite deletar categoria com products
        }
    }
}
