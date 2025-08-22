using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.UseCases.Livros
{
    public class ListarLivrosDisponiveisUseCase
    {
        private readonly ILivroRepository _livroRepo;
        public ListarLivrosDisponiveisUseCase(ILivroRepository livroRepo)
        {
            _livroRepo = livroRepo;
        }

        public async Task<IEnumerable<LivroResponse>> ExecuteAsync()
        {
            var livros = await _livroRepo.ListarDisponiveisAsync();
            return livros.Select(l => new LivroResponse(l.Id, l.Titulo, l.Autor.NomeCompleto.ToString(), l.AnoPublicacao, l.Disponivel, l.NumeroPaginas));
        }
    }
}