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

        public async Task ExecuteAsync(EditarLivroRequest request)
        {
            var livro = await _livroRepo.ObterPorIdAsync(request.LivroId) ?? throw new ArgumentException("Livro não encontrado.");
            _bibliotecarioService.EditarLivro(livro, request.NovoTitulo, request.NovoAnoPublicacao);
            await _livroRepo.AtualizarAsync(livro);
        }
    }
}
