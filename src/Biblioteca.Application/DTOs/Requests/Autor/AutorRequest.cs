namespace Biblioteca.Application.DTOs.Requests.Autor;

public class AutorRequest
{
    public Guid Id { get; set; }
    public NomeCompletoRequest NomeCompleto { get; set; }
    public EmailRequest Email { get; set; }
    public DateTime DataNascimento { get; set; }
}
