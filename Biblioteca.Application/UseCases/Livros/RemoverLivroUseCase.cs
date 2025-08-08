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

        public void Execute(Guid livroId)
        {
            var livro = _livroRepo.ObterPorId(livroId) ?? throw new ArgumentException("Livro não encontrado.");
            _bibliotecarioService.RemoverLivro(livro);
            _livroRepo.Remover(livro);
        }
    }
}