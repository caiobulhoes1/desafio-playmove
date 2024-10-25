using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace API.Entities;

public class FornecedorCreateDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
}