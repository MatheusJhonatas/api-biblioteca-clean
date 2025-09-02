using Azure.Core;
using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Application.UseCases.Leitores;
using Biblioteca.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeitoresController : ControllerBase
{
    private readonly CadastrarLeitorUseCase _cadastrarLeitor;
    private readonly ListarLeitoresUseCase _listarLeitores;

    public LeitoresController(CadastrarLeitorUseCase cadastrarLeitor, ListarLeitoresUseCase listarLeitores)
    {
        _cadastrarLeitor = cadastrarLeitor;
        _listarLeitores = listarLeitores;
    }

    // Métodos da API
    /// <summary>
    /// Cadastra um novo leitor.
    /// </summary>
    /// <param name="id">Identificador único do livro.</param>
    [HttpPost("v1/leitores")]
    public async Task<IActionResult> CriarLeitor([FromBody] CadastrarLeitorRequest _cadastrarLeitorRequest)
    {
        try
        {
            if (_cadastrarLeitorRequest == null)
                return BadRequest(ResultResponse<string>.Fail("Dados do leitor não podem ser nulos."));


            var resultado = await _cadastrarLeitor.Execute(_cadastrarLeitorRequest);

            if (!resultado.Success)
                return BadRequest(resultado);

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao criar leitor: {ex.Message}");
        }
    }
    /// <summary>
    /// Lista todos leitores disponiveis na biblioteca.
    /// </summary>
    /// <param name="id">Identificador único do livro.</param>
    [HttpGet("v1/leitores")]
    public async Task<IActionResult> ObterLeitores()
    {
        try
        {
            var resultado = await _listarLeitores.Execute();

            if (!resultado.Success)
                return BadRequest(resultado);

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao obter leitores: {ex.Message}");
        }
    }
}