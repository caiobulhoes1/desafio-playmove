Desafio técnico PlayMove.

O teste de conhecimento consistia em desenvolver uma Web API .NET CORE para listar fornecedores relacionados a uma empresa.

O desenvolvimento foi feito utilizando .NET 8 e C# 12

Bibliotecas Utilizadas:

Microsoft.EntityFrameworkCore.InMemory. Versão: 8.0.10
Swashbuckle.AspNetCore. Versão: 6.4.0
Coverlet.Collector. Versão: 6.0.0
Microsoft.NET.Test.Sdk. Versão: 17.8.0
Moq. Versão: 4.20.72
xUnit Versão: 2.5.3
xUnit.Runner.VisualStudio Versão: 2.5.3

Execução da API:
Passo 1: Efetuar o clone do projeto.
Passo 2: Restaurar as bibliotecas utilizadas.
Passo 3: Acessar pasta desafio-playmove/API
Passo 3: Executar dotnet run

A documentação do projeto foi feita via Swagger, para acessar digite: http://localhost:5231/swagger

Os seguintes Endpoints foram implementados.

GET /api/fornecedores: Retorna todos os fornecedores.
GET /api/fornecedores/{id}: Retorna um fornecedor específico pelo ID.
POST /api/fornecedores: Adiciona um novo fornecedor.
PUT /api/fornecedores/{id}: Atualiza um fornecedor existente pelo ID.
DELETE /api/fornecedores/{id}: Remove um fornecedor pelo ID.

O banco utilizado foi em memória para facilitar a execução do projeto. Além disso, algumas informações estão sendo inseridas no momento em que o banco inicializa com a finalidade
de facilitar os testes.

A validação do modelo foi feita utilizando Regex, portanto, para inserir as informações alguns campos devem ser preenchidos corretamente, por exemplo:
Campo CNPJ: 00.000.000/0001-01 (OBS: Não foi utilizado API externa para validar se o CNPJ está ativo ou não).
Campo Telefone: (18) 99000-0000.

Além da implementação dos endpoints da API, também implementei testes unitários para validar as ações (Projeto API.Test). No total são 11 testes unitários.
