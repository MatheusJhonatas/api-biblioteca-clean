namespace Biblioteca.Application.DTOs.Requests.Livro;

public class CategoriaRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int Tipo { get; set; }
}
