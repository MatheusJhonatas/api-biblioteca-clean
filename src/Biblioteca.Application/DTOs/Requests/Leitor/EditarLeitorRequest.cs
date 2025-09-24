
namespace Biblioteca.Application.DTOs.Requests.Leitor;

public class EditarLeitorDto
{
    public Guid Id { get; set; }
    public string PrimeiroNome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Rua { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string CEP { get; set; }
}
