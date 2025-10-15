using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Requests.Livro;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;

namespace Biblioteca.Application.UseCases.Livros;

public class EditarLivroUseCase
{
    private readonly ILivroRepository _livroRepo;
    //Por hora vou somente editar o livro via swagger, mas como melhoria vamos implementar a edição via serviço com o bibliotecario.
    // private readonly BibliotecarioService _bibliotecarioService;
    public EditarLivroUseCase(ILivroRepository livroRepo)
    {
        _livroRepo = livroRepo;
    }
    public async Task<ResultResponse<LivroResponse>> ExecuteAsync(Guid livroId, EditarLivroRequest request)
    {
        var livro = await _livroRepo.ObterPorIdAsync(livroId);
        if (livro == null)
            throw new ArgumentException("Livro não encontrado.");
        // Atualiza só se veio valor novo
        if (!string.IsNullOrWhiteSpace(request.NovoTitulo))
            livro.AlterarTitulo(request.NovoTitulo);
        // Atualiza só se veio valor novo 
        if (request.NovoAnoPublicacao.HasValue)
            livro.AlterarAnoPublicacao(request.NovoAnoPublicacao.Value);
        // Atualiza só se veio valor novo
        if (request.NovoNumeroPaginas.HasValue)
            livro.AlterarNumeroPaginas(request.NovoNumeroPaginas.Value);
        await _livroRepo.AtualizarAsync(livro);
        // Retorna o livro atualizado
        return ResultResponse<LivroResponse>.Ok(new LivroResponse(
            livro.Id,
            livro.Titulo,
            livro.Autor.NomeCompleto.ToString(),
            livro.AnoPublicacao,
            livro.Disponivel,
            livro.NumeroPaginas,
            livro.Descricao
        ), "Livro atualizado com sucesso!");
    }
}

