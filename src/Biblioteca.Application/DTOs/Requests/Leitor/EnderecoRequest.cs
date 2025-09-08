namespace Biblioteca.Application.DTOs.Requests.Leitor;

public class EnderecoRequest
{
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Cep { get; set; }
    public string Complemento { get; set; }
}