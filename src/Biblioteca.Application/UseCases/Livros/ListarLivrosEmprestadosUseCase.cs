using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;
namespace Biblioteca.Application.UseCases.Livros;

public class ListarLivrosEmprestadosUseCase
{
    private readonly ILivroRepository _livroRepo;
    public ListarLivrosEmprestadosUseCase(ILivroRepository livroRepo)
    {
        _livroRepo = livroRepo;
    }
    public async Task<IEnumerable<LivroResponse>> ExecuteAsync()
    {
        var livros = await _livroRepo.ListarEmprestadosAsync();
        return livros.Select(l => new LivroResponse(
            l.Id,
            l.Titulo,
            l.Autor.NomeCompleto.ToString(),
            l.AnoPublicacao,
            l.Disponivel,
            l.NumeroPaginas,
            l.Descricao));
    }
}
