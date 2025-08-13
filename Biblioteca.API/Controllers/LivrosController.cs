using AuthApi.ViewModels;
using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Application.UseCases.Livros;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

[ApiController]
public class LivrosController : ControllerBase
{
    //Uso da injeção de dependência para o caso de uso de cadastrar livro
    private readonly CadastrarLivroUseCase _cadastrarLivro;
    public LivrosController(CadastrarLivroUseCase cadastrarLivro)
    {
        _cadastrarLivro = cadastrarLivro;
    }
    [HttpPost]
    public async Task<IActionResult> CadastrarLivro(
        [FromBody] CadastrarLivroRequest request)
    {
        try
        {
            // Validação de entrada, SE request for nulo retorna BadRequest
            if (request == null)
            {
                return BadRequest(new ResultResponse<string>("Dados do livro não podem ser nulos."));
            }
            // Chamada ao caso de uso, metodo Execute para cadastrar livro.
            var resultado = _cadastrarLivro.Execute(request);
            return Ok(new ResultResponse<LivroResponse>(resultado));
        }
        catch
        {
            return StatusCode(500, new ResultResponse<string>("Erro(01) interno do servidor."));
        }
    }
}
