using API.Data;
using API.Entities;
using API.Repository;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IFornecedor, FornecedorRepository>();
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FornecedorContext>(opt =>
             opt.UseInMemoryDatabase("FornecedorDB"));

        SeedData(services.BuildServiceProvider());
    }

    private static void SeedData(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<FornecedorContext>();

            if (!context.FornecedorDB.Any())
            {
                context.FornecedorDB.AddRange
                (
                    new Fornecedor
                    {
                        Id = 1,
                        Nome = "Fornecedor A",
                        Email = "fornecedorA@example.com",
                        Telefone = "1234-5678",
                        Endereco = "Rua Exemplo, 123",
                        Cnpj = "00.000.000/0001-01",
                        DataCadastro = DateTime.Now
                    },
                    new Fornecedor
                    {
                        Id = 2,
                        Nome = "Fornecedor B",
                        Email = "fornecedorB@example.com",
                        Telefone = "8765-4321",
                        Endereco = "Avenida Exemplo, 456",
                        Cnpj = "11.111.111/0001-11",
                        DataCadastro = DateTime.Now
                    }
                );
                context.SaveChanges();
            }
        }
    }
}