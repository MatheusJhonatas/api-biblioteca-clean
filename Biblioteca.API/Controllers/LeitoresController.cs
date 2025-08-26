using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeitorController : ControllerBase
    {
        private readonly LeitorService _leitorService;

        public LeitorController(LeitorService leitorService)
        {
            _leitorService = leitorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var leitor = await _leitorService.ObterPorIdAsync(id);
            if (leitor == null)
                return NotFound();

            return Ok(leitor);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var leitores = await _leitorService.ObterTodosAsync();
            return Ok(leitores);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Leitor leitor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _leitorService.AdicionarLeitorAsync(leitor);
            return CreatedAtAction(nameof(ObterPorId), new { id = leitor.Id }, leitor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Leitor leitor)
        {
            if (id != leitor.Id)
                return BadRequest("ID inválido");

            await _leitorService.AtualizarLeitorAsync(leitor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            await _leitorService.RemoverLeitorAsync(id);
            return NoContent();
        }
    }
}
