using API.Controllers;
using API.Entities;
using API.Profiles;
using API.Repository;
using API.Test.Fixtures;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace API.Test
{
    public class FornecedorControllerTests
    {
        private readonly Mock<IFornecedor> _mockFornecedor;
        private readonly FornecedorController _controller;
        private readonly IMapper _mapper;

        public FornecedorControllerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FornecedorProfile>();
            });
            _mapper = config.CreateMapper();

            _mockFornecedor = new Mock<IFornecedor>();        
            _controller = new FornecedorController(_mockFornecedor.Object, _mapper);
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
            var fornecedorDTO = _mapper.Map<Fornecedor>(fornecedor);
            _mockFornecedor.Setup(repo => repo.FindAsync(fornecedor.Id))
                .ReturnsAsync(fornecedorDTO);

            var result = await _controller.GetFornecedor(fornecedor.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<FornecedorDTO>(okResult.Value);
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
            var fornecedorDTO = _mapper.Map<FornecedorCreateDTO>(fornecedor);
    
            _mockFornecedor.Setup(repo => repo.GetByCnpjAsync(fornecedorDTO.Cnpj)).ReturnsAsync((Fornecedor)null);
    
            var result = await _controller.AddFornecedor(fornecedorDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Fornecedor>(okResult.Value);
            Assert.Equal(fornecedor.Cnpj, returnValue.Cnpj);
            _mockFornecedor.Verify(repo => repo.AddAsync(It.IsAny<Fornecedor>()), Times.Once);
            _mockFornecedor.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public async Task AddFornecedor_RetornaBadRequest_QuandoFornecedorNãoÉVálido()
        {
            FornecedorCreateDTO fornecedor = null;

            var result = await _controller.AddFornecedor(fornecedor);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutFornecedor_RetornaOk_QuandoFornecedorExiste_E_Válido()
        {
            var id = 1;
            var fornecedorExistente = FornecedorFixture.GetFornecedor(); // Fornecedor
            var fornecedorAtualizado = FornecedorFixture.GetFornecedorAtualizado(); // Fornecedor

            var fornecedorExistenteDTO = _mapper.Map<Fornecedor>(fornecedorExistente);

            _mockFornecedor.Setup(repo => repo.FindAsync(id))
                .ReturnsAsync(fornecedorExistenteDTO);

            var result = await _controller.PutFornecedor(id, fornecedorAtualizado);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Fornecedor>(okResult.Value);
            Assert.Equal(fornecedorAtualizado.Nome, returnValue.Nome);

            // Verificações
            _mockFornecedor.Verify(repo => repo.Update(id, It.IsAny<Fornecedor>()), Times.Once);
            _mockFornecedor.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public async Task PutFornecedor_RetornaNotFound_QuandoFornecedorNãoForEncontrado()
        {
            var id = 999;
            var fornecedorAtualizadoDTO = FornecedorFixture.GetFornecedorAtualizado();
            _mockFornecedor.Setup(repo => repo.FindAsync(id)).ReturnsAsync((Fornecedor)null);

            var result = await _controller.PutFornecedor(id, fornecedorAtualizadoDTO);

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
            var fornecedorDTO = _mapper.Map<Fornecedor>(fornecedor);
            _mockFornecedor.Setup(repo => repo.FindAsync(fornecedor.Id))
                .ReturnsAsync(fornecedorDTO);

            var entityEntryMock = new Mock<EntityEntry<Fornecedor>>(MockBehavior.Strict, null);
            _mockFornecedor.Setup(repo => repo.RemoveAsync(fornecedor.Id)).ReturnsAsync(entityEntryMock.Object);

            var result = await _controller.DeleteFornecedor(fornecedor.Id);

            Assert.IsType<OkObjectResult>(result);
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

