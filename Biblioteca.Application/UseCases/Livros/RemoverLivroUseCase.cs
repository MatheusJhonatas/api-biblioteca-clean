using Biblioteca.Application.DTOs.Responses;
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
        public async Task<ResultResponse<string>> ExecuteAsync(Guid livroId)
        {
            try
            {
                var livro = await _livroRepo.ObterPorIdAsync(livroId);

                if (livro == null)
                    return ResultResponse<string>.Fail("Livro não encontrado.");

                _bibliotecarioService.RemoverLivro(livro);

                await _livroRepo.RemoverAsync(livro);

                return ResultResponse<string>.Ok($"Livro {livro.Titulo} removido com sucesso!");
            }
            catch (Exception ex)
            {
                return ResultResponse<string>.Fail($"Erro ao remover livro: {ex.Message}");
            }
        }
    }
}