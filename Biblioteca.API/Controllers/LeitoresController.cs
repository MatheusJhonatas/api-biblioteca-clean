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

    public LeitoresController(CadastrarLeitorUseCase cadastrarLeitor)
    {
        _cadastrarLeitor = cadastrarLeitor;
    }

    // Métodos da API
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
}