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

        public IEnumerable<LivroResponse> Execute()
        {
            var livros = _livroRepo.ListarDisponiveis();
            return livros.Select(l => new LivroResponse(l.Id, l.Titulo, l.Autor.NomeCompleto.ToString(), l.AnoPublicacao, l.Disponivel));
        }
    }
}