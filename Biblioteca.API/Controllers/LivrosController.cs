using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Application.UseCases.Livros;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

[ApiController]
public class LivrosController : ControllerBase
{
    private readonly CadastrarLivroUseCase _cadastrarLivro;
    private readonly ListarLivrosDisponiveisUseCase _listarLivros;
    private readonly ObterLivroPorIdUseCase _obterLivroPorId;
    private readonly RemoverLivroUseCase _removerLivro;

    public LivrosController(CadastrarLivroUseCase cadastrarLivro, ListarLivrosDisponiveisUseCase listarLivros, ObterLivroPorIdUseCase obterLivroPorId, RemoverLivroUseCase removerLivro)
    {
        _cadastrarLivro = cadastrarLivro;
        _listarLivros = listarLivros;
        _obterLivroPorId = obterLivroPorId;
        _removerLivro = removerLivro;
    }

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
    [HttpGet("v1/livros")]
    public async Task<IActionResult> ListarLivrosAsync()
    {
        try
        {
            var resultado = await _listarLivros.ExecuteAsync();
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ResultResponse<string>.Fail($"Erro interno ao listar livros: {ex.Message}"));
        }
    }
    [HttpGet("v1/livros/{id:guid}")]
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
}
