using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class FornecedorContext : DbContext
{
    public FornecedorContext(DbContextOptions<FornecedorContext> options) : base(options) { }
    public DbSet<Fornecedor> FornecedorDB { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Fornecedor>().HasData(
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
    });
    }
}