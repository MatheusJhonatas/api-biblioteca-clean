using Biblioteca.Application.DTOs.Requests.Leitor;
using Biblioteca.Application.DTOs.Requests.Livro;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Application.UseCases.Livros;
using Microsoft.AspNetCore.Mvc;
namespace Biblioteca.API.Controllers;

[ApiController]
public class LivrosController : ControllerBase
{
    private readonly CadastrarLivroUseCase _cadastrarLivro;
    private readonly ListarLivrosDisponiveisUseCase _listarLivrosDisponiveis;
    private readonly ObterLivroPorIdUseCase _obterLivroPorId;
    private readonly RemoverLivroUseCase _removerLivro;
    private readonly EditarLivroUseCase _editarLivro;
    private readonly ListarLivrosEmprestadosUseCase _listarLivrosEmprestados;

    public LivrosController(CadastrarLivroUseCase cadastrarLivro, ListarLivrosDisponiveisUseCase listarLivrosDisponiveis, ObterLivroPorIdUseCase obterLivroPorId, RemoverLivroUseCase removerLivro, EditarLivroUseCase editarLivro, ListarLivrosEmprestadosUseCase listarLivrosEmprestados)
    {
        _cadastrarLivro = cadastrarLivro;
        _listarLivrosDisponiveis = listarLivrosDisponiveis;
        _obterLivroPorId = obterLivroPorId;
        _removerLivro = removerLivro;
        _editarLivro = editarLivro;
        _listarLivrosEmprestados = listarLivrosEmprestados;
    }
    /// <summary>
    /// Cadastra um novo livro na biblioteca.
    /// </summary>
    /// <param name="request">Dados necessários para cadastrar o livro.</param>
    /// <returns>Livro cadastrado com sucesso ou mensagem de erro.</returns>
    [HttpPost("v1/livros")]
    public async Task<IActionResult> CadastrarLivro([FromBody] CadastrarLivroRequest request)
    {
        try
        {
            if (request == null)
                return BadRequest(ResultResponse<string>.Fail("Dados do livro não podem ser nulos."));

            var resultado = await _cadastrarLivro.Execute(request);

            if (!resultado.Success)
                return BadRequest(resultado);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ResultResponse<string>.Fail($"Erro interno ao cadastrar livro: {ex.Message}"));
        }
    }
    /// <summary>
    /// Lista todos os livros **disponíveis** na biblioteca.
    /// </summary>
    [HttpGet("v1/livros")]
    public async Task<IActionResult> ListarLivrosAsync()
    {
        try
        {
            var resultado = await _listarLivrosDisponiveis.ExecuteAsync();
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ResultResponse<string>.Fail($"Erro interno ao listar livros: {ex.Message}"));
        }
    }
    ///<summary>
    /// Lista todos os livros empréstados na biblioteca.
    /// <</summary>
    [HttpGet("v1/livros/emprestados")]
    public async Task<IActionResult> ListarLivrosEmprestadosAsync()
    {
        try
        {
            var resultado = await _listarLivrosEmprestados.ExecuteAsync();
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ResultResponse<string>.Fail($"Erro interno ao listar livros: {ex.Message}"));
        }
    }
    /// <summary>
    /// Obtém os detalhes de um livro a partir do seu ID.
    /// </summary>
    /// <param name="id">Identificador único do livro.</param>
    [HttpGet("v1/livro/{id:guid}")]
    public async Task<IActionResult> ObterLivroPorIdAsync(Guid id)
    {
        try
        {
            var resultado = await _obterLivroPorId.ExecuteAsync(id);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ResultResponse<string>.Fail($"Erro interno ao obter livro: {ex.Message}"));
        }
    }
    /// <summary>
    /// Remove um livro do sistema com base no seu ID.
    /// </summary>
    /// <param name="id">Identificador único do livro.</param>
    [HttpDelete("v1/livros/{id:guid}")]
    public async Task<IActionResult> DeletarLivroPorIdAsync(Guid id)
    {
        try
        {
            var resultado = await _removerLivro.ExecuteAsync(id);
            if (!resultado.Success)
                return NotFound(resultado);
            return Ok(resultado);
        }
        catch
        {
            return StatusCode(500, ResultResponse<string>.Fail("Erro interno ao remover livro."));
        }
    }
    /// <summary>
    /// Atualiza parcialmente um livro (título, ano de publicação ou número de páginas).
    /// </summary>
    [HttpPatch("v1/livros/{id}")]
    public async Task<IActionResult> AtualizarLivro(Guid id, [FromBody] EditarLivroRequest request)
    {
        try
        {
            if (request == null)
                return BadRequest(ResultResponse<string>.Fail("Dados do livro não podem ser nulos."));

            if (string.IsNullOrWhiteSpace(request.NovoTitulo) && !request.NovoAnoPublicacao.HasValue && !request.NovoNumeroPaginas.HasValue)
                return BadRequest(ResultResponse<string>.Fail("Pelo menos um campo deve ser atualizado."));

            var resultado = await _editarLivro.ExecuteAsync(id, request);

            if (!resultado.Success)
                return BadRequest(resultado);

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ResultResponse<string>.Fail($"Erro interno ao atualizar livro: {ex.Message}"));
        }
    }
}


