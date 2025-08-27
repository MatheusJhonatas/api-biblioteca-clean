namespace Biblioteca.Application.DTOs.Responses;

public class LeitorResponse
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Endereco { get; set; }
    public DateTime DataCadastro { get; set; }

    public LeitorResponse(Guid id, string nomeCompleto, string email, string cpf, string endereco, DateTime dataCadastro)
    {
        Id = id;
        NomeCompleto = nomeCompleto;
        Email = email;
        CPF = cpf;
        Endereco = endereco;
        DataCadastro = dataCadastro;
    }
}