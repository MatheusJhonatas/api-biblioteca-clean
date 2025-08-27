using Biblioteca.Application.DTOs.Requests.Autor;

namespace Biblioteca.Application.DTOs.Requests.Livro;

public class CadastrarLivroRequest
{
    public string Titulo { get; set; }
    public AutorRequest Autor { get; set; }
    public string ISBN { get; set; }
    public int AnoPublicacao { get; set; }
    public int NumeroPaginas { get; set; }
    public string? Descricao { get; set; }
    public List<CategoriaRequest> Categorias { get; set; }
}


