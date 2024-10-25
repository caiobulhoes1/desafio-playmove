using API.Entities;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class FornecedorController : ControllerBase
{
    private readonly IFornecedor _fornecedor;

    public FornecedorController(IFornecedor fornecedor)
    {
        _fornecedor = fornecedor;
    }

    [HttpGet]
    [Route("api/fornecedores")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetFornecedores()
    {
        var listFornecedores = await _fornecedor.GetAllAsync();

        if (listFornecedores.Any())
        {
            return Ok(listFornecedores);
        }
        return NoContent();
    }

    [HttpGet]
    [Route("api/fornecedores/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFornecedor(int id)
    {
        var fornecedor = await _fornecedor.FindAsync(id);

        if (fornecedor == null)
        {
            return NotFound($"Fornecedor com o ID = {id} não encontrado");
        }
        return Ok(fornecedor);
    }

    [HttpPost]
    [Route("api/fornecedores")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddFornecedor(Fornecedor fornecedor)
    {
        if (fornecedor != null)
        {
            await _fornecedor.AddAsync(fornecedor);
            await _fornecedor.Save();
            return Ok(fornecedor);
        }
        return BadRequest();

    }

    [HttpPut]
    [Route("api/fornecedores/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutFornecedor(int id, Fornecedor fornecedor)
    {
        if (fornecedor == null)
        {
            return BadRequest("Dados não válidos");
        }

        var fornecedorExistente = await _fornecedor.FindAsync(id);
        if (fornecedorExistente == null)
        {
            return NotFound($"Fornecedor com ID = {id} não encontrado.");
        }

        fornecedorExistente.Nome = fornecedor.Nome;
        fornecedorExistente.Email = fornecedor.Email;
        fornecedorExistente.Endereco = fornecedor.Endereco;
        fornecedorExistente.Cnpj = fornecedor.Cnpj;
        fornecedorExistente.Telefone = fornecedor.Telefone;
        fornecedorExistente.DataCadastro = fornecedor.DataCadastro;

        _fornecedor.Update(id, fornecedorExistente);
        await _fornecedor.Save();

        return Ok(fornecedorExistente);
    }

    [HttpDelete]
    [Route("api/fornecedores/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFornecedor(int id)
    {
        var fornecedorExistente = await _fornecedor.FindAsync(id);

        if (fornecedorExistente == null)
        {
            return NotFound($"Fornecedor com ID = {id} não encontrado");
        }

        await _fornecedor.RemoveAsync(id);
        await _fornecedor.Save();

        return Ok();
    }
}
