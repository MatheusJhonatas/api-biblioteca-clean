using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.UseCases.Livros
{
    public class ObterLivroPorIdUseCase
    {
        // Dependência do repositório de livros
        private readonly ILivroRepository _livroRepo;
        public ObterLivroPorIdUseCase(ILivroRepository livroRepo)
        {
            _livroRepo = livroRepo;
        }
        //metodo async para obter livro por id
        public async Task<ResultResponse<LivroResponse>> ExecuteAsync(Guid id)
        {
            try
            {
                var livro = await _livroRepo.ObterPorIdAsync(id);

                if (livro == null)
                    return ResultResponse<LivroResponse>.Fail("Livro não encontrado, verifique o id digitado..");

                // Cria o DTO de forma segura, tratando nulls nos ValueObjects
                var response = new LivroResponse(
                    livro.Id,
                    livro.Titulo ?? "Titulo Desconhecido",
                    livro.Autor?.NomeCompleto?.ToString() ?? "Autor desconhecido",
                    livro.AnoPublicacao,
                    livro.Disponivel
                );

                return ResultResponse<LivroResponse>.Ok(response);
            }
            catch (Exception ex)
            {
                return ResultResponse<LivroResponse>.Fail($"Erro interno ao obter livro: {ex.Message}");
            }
        }
    }
}
