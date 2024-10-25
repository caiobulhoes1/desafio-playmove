using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace API.Entities;

public class Fornecedor
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;

    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Telefone]
    public string Telefone { get; set; } = string.Empty;

    [Required]
    public string Endereco { get; set; } = string.Empty;

    [Required]
    [Cnpj]
    public string Cnpj { get; set; } = string.Empty;

    [Required]
    public DateTime DataCadastro { get; set; }
}

public class CnpjAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string? cnpj = value as string;

        if (string.IsNullOrEmpty(cnpj))
        {
            return ValidationResult.Success;
        }

        var regex = new Regex(@"^\d{2}\.?\d{3}\.?\d{3}/?\d{4}-?\d{2}$");

        if (!regex.IsMatch(cnpj))
        {
            return new ValidationResult("O CNPJ informado está em um formato inválido.");
        }

        return ValidationResult.Success;

    }
}

public class TelefoneAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string? telefone = value as string;

        if (string.IsNullOrEmpty(telefone))
        {
            return ValidationResult.Success;
        }

        var regex = new Regex(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$");

        if (!regex.IsMatch(telefone))
        {
            return new ValidationResult("O telefone informado está em um formato inválido.");
        }

        return ValidationResult.Success;
    }
}