using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Test.Fixtures
{
    public class FornecedorFixture
    {
        public static IEnumerable<Fornecedor> GetFornecedores()
        {
            return new[]
            {
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
            };
        }

        public static FornecedorDTO GetFornecedor()
        {
            return new FornecedorDTO
            {
                Id = 1,
                Nome = "Fornecedor A",
                Email = "fornecedorA@example.com",
                Telefone = "1234-5678",
                Endereco = "Rua Exemplo, 123",
                Cnpj = "00.000.000/0001-01",
                DataCadastro = DateTime.Now
            };
        }

        public static FornecedorUpdateDTO GetFornecedorAtualizado()
        {
            return new FornecedorUpdateDTO
            {
                Nome = "Fornecedor Atualizado",
                Email = "fornecedorA@example.com",
                Telefone = "1234-5678",
                Endereco = "Rua Exemplo, 123",
                Cnpj = "00.000.000/0001-01",
                DataCadastro = DateTime.Now
            };
        }
    }
}
