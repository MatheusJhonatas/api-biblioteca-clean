using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Requests.Livro;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Application.UseCases.Emprestimos;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

[ApiController]
public class EmprestimosController : ControllerBase
{
    private readonly EmprestarLivroUseCase _emprestimoLivro;

    public EmprestimosController(EmprestarLivroUseCase emprestimoLivro)
    {
        _emprestimoLivro = emprestimoLivro;
    }
    /// <summary>
    /// Realiza o empréstimo de um livro para um leitor.
    /// </summary>
    [HttpPost("v1/emprestimos")]
    public async Task<IActionResult> RealizarEmprestimo([FromBody] EmprestarLivroRequest request)
    {
        try
        {
            if (request == null)
                return BadRequest("Dados do empréstimo não podem ser nulos.");

            var resultado = await _emprestimoLivro.ExecuteAsync(request);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ResultResponse<string>.Fail($"Erro interno ao emprestar um livro: {ex.Message}"));
        }
    }
}