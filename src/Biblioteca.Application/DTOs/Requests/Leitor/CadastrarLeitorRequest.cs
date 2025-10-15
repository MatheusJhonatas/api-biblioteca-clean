using Biblioteca.Application.DTOs.Requests.Leitor;
namespace Biblioteca.Application.DTOs.Requests;

public class CadastrarLeitorRequest
{
    public NomeCompletoRequest NomeCompleto { get; set; }
    public EmailRequest Email { get; set; }
    public EnderecoRequest Endereco { get; set; }
    public CpfRequest Cpf { get; set; }
    public DateTime DataCadastro { get; set; }
}
