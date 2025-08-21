using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;

namespace Biblioteca.Application.UseCases.Livros
{
    public class EditarLivroUseCase
    {
        private readonly ILivroRepository _livroRepo;
        //Por hora vou somente editar o livro via swagger, mas como melhoria vamos implementar a edição via serviço com o bibliotecario.
        // private readonly BibliotecarioService _bibliotecarioService;

        public EditarLivroUseCase(ILivroRepository livroRepo)
        {
            _livroRepo = livroRepo;
        }

        public async Task ExecuteAsync(EditarLivroRequest request)
        {
            var livro = await _livroRepo.ObterPorIdAsync(request.LivroId);
            if (livro == null)
                throw new ArgumentException("Livro não encontrado.");
            // Atualiza só se veio valor novo
            if (!string.IsNullOrWhiteSpace(request.NovoTitulo))
                livro.AtualizarTitulo(request.NovoTitulo);
            // Atualiza só se veio valor novo 
            if (request.NovoAnoPublicacao.HasValue)
                livro.AtualizarAnoPublicacao(request.NovoAnoPublicacao.Value);
            await _livroRepo.AtualizarAsync(livro);
        }
    }
}
