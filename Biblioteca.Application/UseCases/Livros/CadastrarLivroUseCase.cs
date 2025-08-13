using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;

namespace Biblioteca.Application.UseCases.Livros
{
    public class CadastrarLivroUseCase
    {
        private readonly ILivroRepository _livroRepo;
        private readonly BibliotecarioService _bibliotecarioService;

        public CadastrarLivroUseCase(ILivroRepository livroRepo, BibliotecarioService bibliotecarioService)
        {
            _livroRepo = livroRepo;
            _bibliotecarioService = bibliotecarioService;
        }

        public async Task<LivroResponse> Execute(CadastrarLivroRequest request)
        {
            var livro = _bibliotecarioService.CadastrarLivro(
                request.Titulo,
                request.Autor,
                request.ISBN,
                request.AnoPublicacao,
                request.Categorias
            );

            _livroRepo.Salvar(livro);

            return new LivroResponse(livro.Id, livro.Titulo, livro.Autor.NomeCompleto.ToString(), livro.AnoPublicacao, livro.Disponivel);
        }
    }
}