// =============================================================================
// ControleProdutos.Infrastructure - Seed Data (Dados Iniciais)
// =============================================================================
//  CONCEITO IMPORTANTE: Seed Data
// Seed Data são dados iniciais que são inseridos no banco de dados
// quando a aplicação é executada pela primeira vez.
// Isso é útil para:
// - Ter dados de demonstração
// - Criar o usuário administrador inicial
// - Popular categorias padrão
//
// Este método é chamado no Program.cs durante a inicialização da aplicação.
// =============================================================================

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ControleProdutos.Domain.Entities;
using ControleProdutos.Infrastructure.Context;

namespace ControleProdutos.Infrastructure.Identity
{
    /// <summary>
    /// Classe responsável por popular o banco de dados com dados iniciais.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Popula o banco de dados com categorias, produtos e o usuário admin.
        /// Este método é idempotente — pode ser chamado várias vezes sem duplicar dados.
        /// </summary>
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            // Obtém o DbContext do container de Dependency Injection
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ControleProdutosDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Aplica migrations pendentes automaticamente
            await context.Database.MigrateAsync();

            // =====================================================================
            // 1. SEED DE CATEGORIAS
            // =====================================================================
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Eletrônicos e Tecnologia" },
                    new Category { Name = "Moda e Vestuário" },
                    new Category { Name = "Beleza e Cuidados Pessoais" },
                    new Category { Name = "Esporte e Lazer" },
                    new Category { Name = "Alimentos e Bebidas" },
                    new Category { Name = "Casa e Decoração" }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // =====================================================================
            // 2. SEED DE PRODUTOS
            // =====================================================================
            if (!context.Products.Any())
            {
                // Busca as categorias recém-criadas para obter os IDs
                var eletronicos = await context.Categories.FirstAsync(c => c.Name == "Eletrônicos e Tecnologia");
                var moda = await context.Categories.FirstAsync(c => c.Name == "Moda e Vestuário");
                var beleza = await context.Categories.FirstAsync(c => c.Name == "Beleza e Cuidados Pessoais");
                var esporte = await context.Categories.FirstAsync(c => c.Name == "Esporte e Lazer");
                var alimentos = await context.Categories.FirstAsync(c => c.Name == "Alimentos e Bebidas");
                var casa = await context.Categories.FirstAsync(c => c.Name == "Casa e Decoração");

                var products = new List<Product>
                {
                    new Product
                    {
                        Title = "Iphone 13 Pro Max",
                        Description = "O iPhone 13 Pro Max oferece desempenho excepcional, câmera avançada e uma experiência de usuário incomparável.",
                        CoverImageUrl = "",
                        Price = 9999.99,
                        CategoryId = eletronicos.Id,
                        CreatedAt = DateTime.Now
                    }
    
                };
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();

                // =====================================================================
                // 3. SEED DE ROLES (Papéis de Usuário)
                // =====================================================================
                //  CONCEITO: Roles no Identity
                // Roles são papéis que definem o nível de acesso do usuário.
                // Exemplo: "Admin" pode gerenciar games, "User" só pode visualizar.
                // =====================================================================
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // =====================================================================
                // 4. SEED DO USUÁRIO ADMINISTRADOR
                // =====================================================================
                //  CONCEITO: UserManager
                // O UserManager é o serviço do Identity para gerenciar usuários.
                // Ele permite criar, buscar, atualizar e deletar usuários.
                // =====================================================================
                var adminEmail = "admin@controleprodutos.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true // Confirma o email automaticamente
                    };

                    // Cria o usuário com a senha padrão
                    var result = await userManager.CreateAsync(adminUser, "Admin@123");

                    if (result.Succeeded)
                    {
                        // Atribui a role "Admin" ao usuário
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            } } } 
    }
