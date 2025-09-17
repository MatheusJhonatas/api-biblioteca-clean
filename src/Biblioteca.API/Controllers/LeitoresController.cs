using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Application.UseCases.Leitores;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Prefixo base da rota: api/Leitores
    public class LeitoresController : ControllerBase
    {
        private readonly ObterLeitorPorIdUseCase _obterLeitorPorId;
        private readonly ListarLeitoresUseCase _listarLeitores;
        private readonly CadastrarLeitorUseCase _cadastrarLeitor;
        private readonly DeletarLeitorUseCase _deletarLeitor;

        public LeitoresController(
            ObterLeitorPorIdUseCase obterLeitorPorId,
            ListarLeitoresUseCase listarLeitores,
            CadastrarLeitorUseCase cadastrarLeitor,
            DeletarLeitorUseCase deletarLeitor)
        {
            _obterLeitorPorId = obterLeitorPorId;
            _listarLeitores = listarLeitores;
            _cadastrarLeitor = cadastrarLeitor;
            _deletarLeitor = deletarLeitor;
        }
        // POST api/Leitores/v1/leitores
        [HttpPost("v1/leitores")]
        public async Task<IActionResult> CriarLeitor([FromBody] CadastrarLeitorRequest cadastrarLeitorRequest)
        {
            try
            {
                if (cadastrarLeitorRequest == null)
                    return BadRequest(ResultResponse<string>.Fail("Dados do leitor não podem ser nulos."));

                var resultado = await _cadastrarLeitor.Execute(cadastrarLeitorRequest);

                if (!resultado.Success)
                    return BadRequest(resultado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao criar leitor: {ex.Message}");
            }
        }
        // GET api/Leitores/v1/leitores/{id}
        [HttpGet("v1/leitores/{id:guid}")]
        public async Task<IActionResult> ObterLeitorPorIdAsync([FromRoute] Guid id)
        {
            try
            {
                var resultado = await _obterLeitorPorId.ExecuteAsync(id);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                var innerEx = ex.InnerException != null ? $" Inner Exception: {ex.InnerException.Message}" : string.Empty;
                return StatusCode(500, ResultResponse<string>.Fail(
                    $"Erro interno ao obter leitor: {ex.Message}{innerEx}"
                ));
            }
        }

        // GET api/Leitores/v1/leitores
        [HttpGet("v1/leitores")]
        public async Task<IActionResult> ListarLeitoresAsync()
        {
            try
            {
                var resultado = await _listarLeitores.Execute();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultResponse<string>.Fail(
                    $"Erro ao listar leitores: {ex.Message}"
                ));
            }
        }

        [HttpDelete("v1/leitores/{id:guid}")]
        public async Task<IActionResult> DeletarLeitorAsync(Guid id)
        {
            try
            {
                var resultado = await _deletarLeitor.ExecuteAsync(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultResponse<string>.Fail(
                    $"Erro ao deletar leitores: {ex.Message}"
                ));
            }
        }
    }
}