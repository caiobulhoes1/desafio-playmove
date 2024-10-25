using API.Controllers;
using API.Entities;
using API.Repository;
using API.Test.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace API.Test
{
    public class FornecedorControllerTests
    {
        private readonly Mock<IFornecedor> _mockFornecedor;
        private readonly FornecedorController _controller;

        public FornecedorControllerTests()
        {
            _mockFornecedor = new Mock<IFornecedor>();
            _controller = new FornecedorController(_mockFornecedor.Object);
        }

        [Fact]
        public async Task Get_Fornecedores_RetornaOkResult_QuandoExistemFornecedores()
        {
            var fornecedores = FornecedorFixture.GetFornecedores().ToArray();
            _mockFornecedor.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(fornecedores);

            var result = await _controller.GetFornecedores();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Fornecedor[]>(okResult.Value);
            Assert.Equal(2, returnValue.Length);
        }

        [Fact]
        public async Task GetFornecedores_RetornaNoContent_QuandoNãoExistemFornecedores()
        {
            _mockFornecedor.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new Fornecedor[0]);

            var result = await _controller.GetFornecedores();

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetFornecedores_RetornaOk_QuandoIDFornecedorExistir()
        {
            var fornecedor = FornecedorFixture.GetFornecedor();
            _mockFornecedor.Setup(repo => repo.FindAsync(fornecedor.Id))
                .ReturnsAsync(fornecedor);

            var result = await _controller.GetFornecedor(fornecedor.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Fornecedor>(okResult.Value);
            Assert.Equal(fornecedor.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetFornecedores_Retorna_NotFound_QuandoIDFornecedorNãoExistir()
        {
            int fornecedorId = 999;
            _mockFornecedor.Setup(repo => repo.FindAsync(fornecedorId))
                .ReturnsAsync((Fornecedor)null);

            var result = await _controller.GetFornecedor(fornecedorId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Fornecedor com o ID = {fornecedorId} não encontrado", notFoundResult.Value);
        }

        [Fact]
        public async Task AddFornecedor_RetornaOk_QuandoFornecedorÉAdicionadoComSucesso()
        {
            var fornecedor = FornecedorFixture.GetFornecedor();

            var result = await _controller.AddFornecedor(fornecedor);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Fornecedor>(okResult.Value);
            Assert.Equal(fornecedor.Id, returnValue.Id);
            _mockFornecedor.Verify(repo => repo.AddAsync(fornecedor), Times.Once);
            _mockFornecedor.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public async Task AddFornecedor_RetornaBadRequest_QuandoFornecedorNãoÉVálido()
        {
            Fornecedor fornecedor = null;

            var result = await _controller.AddFornecedor(fornecedor);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutFornecedor_RetornaOk_QuandoFornecedorExiste_E_Válido()
        {
            var id = 1;
            var fornecedorExistente = FornecedorFixture.GetFornecedor();
            var fornecedorAtualizado = FornecedorFixture.GetFornecedorAtualizado();
            _mockFornecedor.Setup(repo => repo.FindAsync(id))
                .ReturnsAsync(fornecedorExistente);

            var result = await _controller.PutFornecedor(id, fornecedorAtualizado);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Fornecedor>(okResult.Value);
            Assert.Equal(fornecedorAtualizado.Nome, returnValue.Nome);
            _mockFornecedor.Verify(repo => repo.Update(id, fornecedorExistente), Times.Once);
            _mockFornecedor.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public async Task PutFornecedor_RetornaNotFound_QuandoFornecedorNãoForEncontrado()
        {
            var id = 999;
            var fornecedorAtualizado = FornecedorFixture.GetFornecedor();
            _mockFornecedor.Setup(repo => repo.FindAsync(id)).ReturnsAsync((Fornecedor)null);

            var result = await _controller.PutFornecedor(id, fornecedorAtualizado);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Fornecedor com ID = {id} não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public async Task PutFornecedor_RetornaBadRequest_QuandoFornecedorÉNulo()
        {
            var id = 1;

            var result = await _controller.PutFornecedor(id, null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Dados não válidos", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteFornecedor_RetornaOK_QuandoFornecedorÉDeletado()
        {
            var fornecedor = FornecedorFixture.GetFornecedor();

            _mockFornecedor.Setup(repo => repo.FindAsync(fornecedor.Id)).ReturnsAsync(fornecedor);

            var entityEntryMock = new Mock<EntityEntry<Fornecedor>>(MockBehavior.Strict, null);
            _mockFornecedor.Setup(repo => repo.RemoveAsync(fornecedor.Id)).ReturnsAsync(entityEntryMock.Object);

            var result = await _controller.DeleteFornecedor(fornecedor.Id);

            Assert.IsType<OkResult>(result);
            _mockFornecedor.Verify(repo => repo.RemoveAsync(fornecedor.Id), Times.Once);
            _mockFornecedor.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteFornecedor_RetornaBadRequest_QuandoFornecedorNãoExiste()
        {
            var id = 999;

            var result = await _controller.DeleteFornecedor(id);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Fornecedor com ID = {id} não encontrado", notFoundResult.Value );
        }
    }
}

