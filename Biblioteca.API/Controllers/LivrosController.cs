using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Application.UseCases.Livros;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly CadastrarLivroUseCase _cadastrarLivro;

    public LivrosController(CadastrarLivroUseCase cadastrarLivro)
    {
        _cadastrarLivro = cadastrarLivro;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarLivro([FromBody] CadastrarLivroRequest request)
    {
        if (request == null)
            return BadRequest(ResultResponse<string>.Fail("Dados do livro não podem ser nulos."));

        var resultado = await _cadastrarLivro.Execute(request);

        if (!resultado.Success)
            return BadRequest(resultado); // já retorna no padrão do ResultResponse

        return Ok(resultado); // sucesso já vem formatado pelo Use Case
    }
}
