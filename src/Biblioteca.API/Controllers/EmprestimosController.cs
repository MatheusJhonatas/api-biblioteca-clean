using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Requests.Livro;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Application.UseCases.Emprestimos;
using Biblioteca.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

[ApiController]
public class EmprestimosController : ControllerBase
{
    private readonly EmprestarLivroUseCase _emprestimoLivro;
    private readonly ListarEmprestimoUseCase _listarEmprestimo;


    public EmprestimosController(EmprestarLivroUseCase emprestimoLivro, ListarEmprestimoUseCase listarEmprestimo)
    {
        _emprestimoLivro = emprestimoLivro;
        _listarEmprestimo = listarEmprestimo;
    }
    /// <summary>
    /// Realiza o empréstimo de um livro para um leitor.
    /// </summary>
    [HttpPost("v1/emprestimos")]
    public async Task<IActionResult> RealizarEmprestimo([FromBody] EmprestarLivroRequest request)
    {
        try
        {
            if (!@ModelState.IsValid)
                return BadRequest(ResultResponse<string>.Fail("Dados do empréstimo inválidos."));

            var resultado = await _emprestimoLivro.ExecuteAsync(request);
            if (!resultado.Success)
                return BadRequest(resultado);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ResultResponse<string>.Fail($"Erro interno ao emprestar um livro: {ex.Message}"));
        }
    }
    /// <summary>
    /// Lista todos os empréstimos.
    /// </summary>
    [HttpGet("v1/emprestimos")]
    public async Task<IActionResult> ObterEmprestimos()
    {
        try
        {
            var resultado = await _listarEmprestimo.ExecuteAsync();

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ResultResponse<string>.Fail($"Erro interno ao obter empréstimos: {ex.Message}"));
        }
    }
}