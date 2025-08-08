using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;

namespace Biblioteca.Application.UseCases.Livros
{
    public class EditarLivroUseCase
    {
        private readonly ILivroRepository _livroRepo;
        private readonly BibliotecarioService _bibliotecarioService;

        public EditarLivroUseCase(ILivroRepository livroRepo, BibliotecarioService bibliotecarioService)
        {
            _livroRepo = livroRepo;
            _bibliotecarioService = bibliotecarioService;
        }

        public void Execute(EditarLivroRequest request)
        {
            var livro = _livroRepo.ObterPorId(request.LivroId) ?? throw new ArgumentException("Livro não encontrado.");
            _bibliotecarioService.EditarLivro(livro, request.NovoTitulo, request.NovoAnoPublicacao);
            _livroRepo.Atualizar(livro);
        }
    }
}
