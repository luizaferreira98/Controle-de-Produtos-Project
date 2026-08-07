// =============================================================================
// SenacGames.Infrastructure - Configuração da entidade Category (Fluent API)
// =============================================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ControleProdutos.Domain.Entities;

namespace ControleProdutos.Infrastructure.Configurations
{
    /// <summary>
    /// Configuração Fluent API da entidade Category.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
