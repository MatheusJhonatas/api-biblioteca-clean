using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;

namespace Biblioteca.Application.UseCases.Livros
{
    public class RemoverLivroUseCase
    {
        private readonly ILivroRepository _livroRepo;
        private readonly BibliotecarioService _bibliotecarioService;

        public RemoverLivroUseCase(ILivroRepository livroRepo, BibliotecarioService bibliotecarioService)
        {
            _livroRepo = livroRepo;
            _bibliotecarioService = bibliotecarioService;
        }

        public async Task ExecuteAsync(Guid livroId)
        {
            var livro = await _livroRepo.ObterPorIdAsync(livroId) ?? throw new ArgumentException("Livro não encontrado.");
            _bibliotecarioService.RemoverLivro(livro);
            await _livroRepo.RemoverAsync(livro);
        }
    }
}