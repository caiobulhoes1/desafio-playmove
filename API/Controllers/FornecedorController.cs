using API.Entities;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class FornecedorController : ControllerBase
{
    private readonly IFornecedor _fornecedor;
    private readonly IMapper _mapper;

    public FornecedorController(IFornecedor fornecedor, IMapper mapper)
    {
        _fornecedor = fornecedor;
        _mapper = mapper;
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

        var fornecedorDTO = _mapper.Map<FornecedorDTO>(fornecedor);

        return Ok(fornecedorDTO);
    }

    [HttpPost]
    [Route("api/fornecedores")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddFornecedor(FornecedorCreateDTO fornecedorDTO)
    {      
        if (fornecedorDTO == null)
        {
            return BadRequest();
        }

        var fornecedorExistente = await _fornecedor.GetByCnpjAsync(fornecedorDTO.Cnpj);
        if (fornecedorExistente != null)
        {
            return Conflict("Fornecedor já cadastrado.");
        }

        var fornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);

        await _fornecedor.AddAsync(fornecedor);
        await _fornecedor.Save();
        return Ok(fornecedor);
    }

    [HttpPut]
    [Route("api/fornecedores/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PutFornecedor(int id, FornecedorUpdateDTO fornecedorDTO)
    {
        if (fornecedorDTO == null)
        {
            return BadRequest("Dados não válidos");
        }

        var cnpjExistente = await _fornecedor.GetByCnpjAsync(fornecedorDTO.Cnpj);
        if (cnpjExistente != null)
        {
            return Conflict("Fornecedor já cadastrado.");
        }

        var fornecedorExistente = await _fornecedor.FindAsync(id);
        if (fornecedorExistente == null)
        {
            return NotFound($"Fornecedor com ID = {id} não encontrado.");
        }

        _mapper.Map(fornecedorDTO, fornecedorExistente);

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

        return Ok($"Fornecedor com ID = {id} foi removido com sucesso.");
    }
}
